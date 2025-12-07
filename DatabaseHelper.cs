using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace CourseWork_5sem;

public class DatabaseHelper
{
    // Храним текущее активное соединение
    public static NpgsqlConnection CurrentConnection { get;  set; }
    
    // Храним роль (имя пользователя) для настройки интерфейса в будущем
    public static string CurrentUserRole { get; set; }
    // ДОБАВЛЕНИЕ: Хранение текущего пароля для переподключения
    private static string _currentPassword = "";

    private const string Host = "localhost"; // Или ваш IP адрес
    private const string Database = "cars_parts_db"; // Имя вашей БД
    private const int Port = 5432;
    
    public static string CurrentUsername => CurrentUserRole;
    /// <summary>
    /// Попытка подключения к БД с указанными учетными данными
    /// </summary>
    public static bool TryLogin(string username, string password)
    {
        try
        {
            // Используем Builder для безопасного создания строки подключения
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Host,
                Port = Port,
                Database = Database,
                Username = username,
                Password = password,
                // Полезно для отладки, но в продакшене лучше убрать
                IncludeErrorDetail = true 
            };

            // Создаем новое соединение
            var tempConnection = new NpgsqlConnection(builder.ConnectionString);
            
            // Пробуем открыть. Если пароль неверный - Npgsql выбросит исключение
            tempConnection.Open();

            // Если мы здесь, значит логин успешен. Сохраняем соединение.
            if (CurrentConnection != null && CurrentConnection.State == System.Data.ConnectionState.Open)
            {
                CurrentConnection.Close();
            }

            CurrentConnection = tempConnection;
            CurrentUserRole = username;
            _currentPassword = password;
            return true;
        }
        catch (PostgresException ex)
        {
            // Ошибка 28P01 - неверный пароль или пользователь
            if (ex.SqlState == "28P01")
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show($"Ошибка БД: {ex.MessageText}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка соединения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    public static void CloseConnection()
    {
        if (CurrentConnection != null)
        {
            CurrentConnection.Close();
            CurrentConnection.Dispose();
        }
    }
    
    // =========================================================================
    // ДОБАВЛЕНИЕ ДЛЯ СМЕНЫ ПАРОЛЯ
    // =========================================================================

    /// <summary>
    /// Пытается установить соединение с БД для проверки учетных данных (используется для проверки старого пароля).
    /// </summary>
    public static bool TestConnection(string username, string password)
    {
        // Строка подключения для проверки
        string testConnectionString = new NpgsqlConnectionStringBuilder
        {
            Host = Host,
            Port = Port,
            Database = Database,
            Username = username,
            Password = password
        }.ConnectionString;
        
        using (var conn = new NpgsqlConnection(testConnectionString))
        {
            try
            {
                conn.Open();
                return true; // Соединение удалось -> Пароль верен
            }
            catch (NpgsqlException)
            {
                return false; // Соединение не удалось -> Пароль не верен
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    
    /// <summary>
    /// Обновляет хранимый пароль и переподключает активное соединение с новыми учетными данными.
    /// </summary>
    public static void UpdateConnectionCredentials(string username, string newPassword)
    {
        // 1. Обновляем хранимый пароль
        _currentPassword = newPassword;
        
        // 2. Закрываем старое соединение
        CloseConnection(); 
        
        // 3. Создаем новое соединение с новым паролем
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = Host,
            Port = Port,
            Database = Database,
            Username = username,
            Password = newPassword,
            IncludeErrorDetail = true 
        };
        
        try
        {
            CurrentConnection = new NpgsqlConnection(builder.ConnectionString);
            CurrentConnection.Open();
        }
        catch (Exception ex)
        {
            // Если переподключение не удалось, нужно сообщить пользователю
            MessageBox.Show($"ВНИМАНИЕ! Пароль в БД изменен, но не удалось переподключиться: {ex.Message}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            // В этом случае лучше закрыть приложение или предложить перелогиниться.
            // В рамках данной задачи просто оповестим:
            CurrentConnection = null;
            // Необходим выход, чтобы пользователь снова вошел с новым паролем.
        }
    }
    /// <summary>
    /// Возвращает список всех таблиц, доступных текущему пользователю в схеме public.
    /// </summary>
    public static List<string> GetAllAccessibleTables()
    {
        var tableNames = new List<string>();
    
        string sql = @"
        SELECT table_name 
        FROM information_schema.tables 
        WHERE table_schema = 'public' 
          AND table_type = 'BASE TABLE'
        ORDER BY table_name;";

        // Внимание: Здесь используется DatabaseHelper.CurrentConnection. 
        // Убедитесь, что объект NpgsqlConnection доступен и открыт.
        using (var cmd = new NpgsqlCommand(sql, CurrentConnection)) 
        {
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок БД
                MessageBox.Show($"Ошибка при получении списка таблиц: {ex.Message}", "Ошибка БД");
            }
        }
        return tableNames;
    }
    
    
    
    
    /// <summary>
    /// Выполняет SQL-запрос и возвращает результат в виде DataTable.
    /// Использует параметризованные запросы.
    /// </summary>
    public static DataTable ExecuteDataTable(string sql, Dictionary<string, object> parameters = null)
    {
        if (CurrentConnection == null || CurrentConnection.State != System.Data.ConnectionState.Open)
        {
            throw new InvalidOperationException("Соединение с базой данных не установлено.");
        }

        var dataTable = new DataTable();
        using (var cmd = new NpgsqlCommand(sql, CurrentConnection))
        {
            // Добавление параметров для защиты от SQL-инъекций
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            try
            {
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                // Выбрасываем исключение, чтобы его поймал вызывающий метод (TableBrowserForm)
                throw new Exception($"Ошибка при выполнении запроса: {ex.Message}", ex);
            }
        }
        return dataTable;
    }

    /// <summary>
    /// Возвращает SQL-запрос SELECT с заменой внешних ключей на имена.
    /// </summary>
    public static string GetDisplayQuery(string tableName)
    {
        // Проверяем, есть ли кастомные метаданные для этой таблицы
        if (AccessControl.CustomTableViews.TryGetValue(tableName, out var metadata))
        {
            return metadata.DisplaySql;
        }

        // Запрос по умолчанию для всех остальных таблиц (например, справочников)
        return $"SELECT * FROM {tableName} ORDER BY 1";
    }
    
    /// <summary>
    /// Получает имена колонок для указанной таблицы (используется для поиска).
    /// </summary>
    public static List<string> GetColumnNames(string tableName)
    {
        // Эту информацию лучше хранить в AccessControl или получать из information_schema
        // Для примера, возвращаем список для employees и parts.
        if (AccessControl.CustomTableViews.TryGetValue(tableName, out var metadata))
        {
            return metadata.ColumnNames;
        }
            // Запрос для получения имен колонок из БД для других таблиц
        var columns = new List<string>();
        string sql = $@"
            SELECT column_name
            FROM information_schema.columns
            WHERE table_name = @tableName 
              AND table_schema = 'public'
            ORDER BY ordinal_position";
        
        using (var cmd = new NpgsqlCommand(sql, CurrentConnection)) 
        {
            cmd.Parameters.AddWithValue("@tableName", tableName);
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении списка колонок: {ex.Message}", "Ошибка БД");
            }
        }
        return columns;
    }
    
    public static int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
    {
        using (var cmd = new NpgsqlCommand(sql, CurrentConnection))
        {
            if (parameters != null)
            {
                foreach (var pair in parameters)
                {
                    // Убедитесь, что типы обрабатываются корректно, 
                    // хотя для DELETE достаточно object.
                    cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                }
            }

            try
            {
                // ExecuteNonQuery возвращает количество затронутых строк
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Возвращаем -1 в случае ошибки
            }
        }
    }
    
    public static DataRow ExecuteDataRow(string sql, Dictionary<string, object> parameters = null)
    {
        DataTable dt = ExecuteDataTable(sql, parameters);

        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        return null;
    }
    
        
    public static List<ReferenceItem> LoadReferenceTable(string referenceTableName)
    {
        List<ReferenceItem> items = new List<ReferenceItem>();
        
        if (CurrentConnection == null || CurrentConnection.State != ConnectionState.Open)
        {
            MessageBox.Show("Соединение с БД не установлено для загрузки справочника.", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return items;
        }
        string sql;
        if (referenceTableName.Equals("employees", StringComparison.OrdinalIgnoreCase))
        {
            // Для employees: конкатенируем Имя и Фамилию
            sql = "SELECT id, first_name || ' ' || last_name AS name FROM employees ORDER BY last_name";
        }
        else if (referenceTableName.Equals("buyers", StringComparison.OrdinalIgnoreCase))
        {
            // Для buyers: конкатенируем Имя и Фамилию
            sql = "SELECT id, first_name || ' ' || last_name AS name FROM buyers ORDER BY last_name";
        }
        else if (referenceTableName.Equals("cells", StringComparison.OrdinalIgnoreCase))
        {
            // ДОБАВЛЕНИЕ: Для ячеек склада: используем cell_number
            sql = "SELECT id, cell_number AS name FROM cells ORDER BY cell_number";
        }
        else if (referenceTableName.Equals("customer_orders", StringComparison.OrdinalIgnoreCase))
        {
            // Для заказов: используем ID и дату заказа
            sql = "SELECT id, 'Заказ №' || id || ' от ' || TO_CHAR(order_date, 'YYYY-MM-DD') AS name FROM customer_orders ORDER BY id DESC";
        }
        else if (referenceTableName.Equals("product_requests", StringComparison.OrdinalIgnoreCase))
        {
            // Ошибка: 'product_requests': 42703
            sql = "SELECT id, 'Заявка №' || id || ' от ' || TO_CHAR(creation_date, 'YYYY-MM-DD') AS name FROM product_requests ORDER BY id DESC";
        }
        else
        {
            sql = $"SELECT id, name FROM \"{referenceTableName}\" ORDER BY name";
        }
        
        try
        {
            using (var cmd = new NpgsqlCommand(sql, CurrentConnection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    items.Add(new ReferenceItem
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            } 
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки справочника '{referenceTableName}': {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        return items;
    }

    // --- Метод для загрузки значений ENUM ---
    public static List<string> GetEnumValues(string enumTypeName)
    {
        var values = new List<string>();

        if (CurrentConnection == null || CurrentConnection.State != ConnectionState.Open)
        {
            MessageBox.Show("Соединение с БД не установлено для загрузки ENUM.", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return values;
        }
        
        // SQL-запрос для получения значений ENUM из pg_enum
        string sql = @"
            SELECT e.enumlabel
            FROM pg_enum e
            JOIN pg_type t ON t.oid = e.enumtypid
            WHERE t.typname = @enumTypeName
            ORDER BY e.enumsortorder";

        try
        {
            using (var cmd = new NpgsqlCommand(sql, CurrentConnection))
            {
                cmd.Parameters.AddWithValue("enumTypeName", enumTypeName);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        values.Add(reader.GetString(0));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении значений ENUM '{enumTypeName}': {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        return values;
    }
}