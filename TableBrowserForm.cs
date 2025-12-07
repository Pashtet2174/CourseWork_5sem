using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork_5sem
{
    // Форма для отображения и взаимодействия с одной таблицей БД
    public partial class TableBrowserForm : Form
    {
        private readonly string _tableName;
        private DataTable _dataTable;

        /// <summary>
        /// Конструктор для формы просмотра таблицы.
        /// </summary>
        /// <param name="tableName">Имя таблицы в базе данных.</param>
        public TableBrowserForm(string tableName)
        {
            _tableName = tableName;
            InitializeComponent();
            this.Load += TableBrowserForm_Load;
            
            // Отключаем границы, чтобы форма выглядела как панель в MainMenuForm
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill; // Заполняем родительский контейнер
        }
        
        private void TableBrowserForm_Load(object sender, EventArgs e)
        {
            // Первоначальная загрузка данных
            LoadData(_tableName, null);
        }

        // Главный метод для загрузки/обновления данных
        private void LoadData(string tableName, string searchTerm)
        {
            try
            {
                // Вызываем метод, который будет собирать SELECT с подстановкой FK
                string displaySql = DatabaseHelper.GetDisplayQuery(tableName);
                
                // Добавляем условие поиска, если оно есть
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    // Получаем список колонок для поиска
                    var columns = DatabaseHelper.GetColumnNames(tableName);
                    
                    // Строим условие WHERE для поиска по всем колонкам
                    string searchCondition = string.Join(" OR ", 
                        columns.Select(col => $"CAST({col} AS TEXT) ILIKE @searchTerm")); 
                        // ILIKE - регистронезависимый поиск в PostgreSQL

                    displaySql = $"SELECT * FROM ({displaySql}) AS base_query WHERE {searchCondition}";
                }
                
                // Параметры для запроса (если есть поиск)
                var parameters = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    parameters.Add("@searchTerm", $"%{searchTerm}%");
                }
                
                // Загружаем данные в DataTable
                _dataTable = DatabaseHelper.ExecuteDataTable(displaySql, parameters);

                // Присваиваем DataTable в DataGridView
                _dataGridView.DataSource = _dataTable;
                
                // Настраиваем ширину столбцов (опционально)
                _dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // --- Обработчики событий ---

        private void RefreshData(object sender, EventArgs e)
        {
            // Сбрасываем поле поиска и перезагружаем данные
            _searchTextBox.Clear();
            LoadData(_tableName, null);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            // Загружаем данные с учетом строки поиска
            LoadData(_tableName, _searchTextBox.Text);
        }

        private void AddRecord_Click(object sender, EventArgs e)
        {
            // Создаем форму, используя конструктор для ДОБАВЛЕНИЯ (только имя таблицы)
            using (var addForm = new RecordEditForm(_tableName))
            {
                // Открываем как диалоговое окно
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Если пользователь нажал "Сохранить", обновляем таблицу
                    RefreshData(null, null);
            
                    // Опционально: можно выделить последнюю запись (сложнее, так как ID генерируется БД)
                }
            }
        }

        private void EditRecord_Click(object sender, EventArgs e)
        {
            // 1. Проверка выбора
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись для изменения.", "Изменение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            DataGridViewRow selectedRow = _dataGridView.SelectedRows[0];
            
            // Получение ID (первая колонка DataGridView, которая является ID)
            if (selectedRow.Cells[0].Value == DBNull.Value || selectedRow.Cells[0].Value == null)
            {
                MessageBox.Show("Невозможно получить ID выбранной записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Прямое преобразование к int
                int recordId = Convert.ToInt32(selectedRow.Cells[0].Value);

                // 2. Получение полных данных записи из БД
                // Используем универсальный запрос для получения данных, 
                // используя DisplaySql, который уже делает все JOIN'ы.
                string displaySql = DatabaseHelper.GetDisplayQuery(_tableName);
                string selectSql = $"SELECT * FROM ({displaySql}) AS base_query WHERE id = @id";
                
                var parameters = new Dictionary<string, object>
                {
                    { "@id", recordId }
                };

                DataRow originalData = DatabaseHelper.ExecuteDataRow(selectSql, parameters);

                if (originalData == null)
                {
                    MessageBox.Show("Не удалось загрузить данные для редактирования.", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. Открытие формы редактирования (заглушка)
                // В реальном приложении здесь будет открываться RecordEditForm
                
                // ---- ВАЖНО: ЗДЕСЬ БУДЕТ ВЫЗОВ ФОРМЫ ----
                // Пример (КОД НУЖНО БУДЕТ ИЗМЕНИТЬ ПОСЛЕ СОЗДАНИЯ RecordEditForm):
                
                using (var editForm = new RecordEditForm(_tableName, recordId, originalData))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Если редактирование завершено, обновляем таблицу
                        RefreshData(null, null);
                    }
                }
                

            }
            catch (Npgsql.PostgresException ex)
            {
                // Ошибки БД (например, проблема с запросом)
                MessageBox.Show($"Ошибка БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                // Ошибка преобразования ID
                MessageBox.Show("Ошибка: ID выбранной записи имеет неверный формат (не число).", "Ошибка типа данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Общие ошибки
                MessageBox.Show($"Критическая ошибка при изменении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteRecord_Click(object sender, EventArgs e)
        {
            // 1. Проверка выбора
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Получаем выбранную строку. Берем только первую, если выбрано несколько.
            DataGridViewRow selectedRow = _dataGridView.SelectedRows[0];
            
            // 2. Получение ID записи
            // ID всегда находится в первой колонке (индекс 0) нашего SELECT * FROM (...)
            if (selectedRow.Cells[0].Value == DBNull.Value || selectedRow.Cells[0].Value == null)
            {
                MessageBox.Show("Невозможно получить ID выбранной записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ID может быть int, long, decimal, поэтому используем универсальный ToString()
            object idValue = selectedRow.Cells[0].Value;
            
            int recordId = Convert.ToInt32(idValue);
            
            // 3. Подтверждение действия
            DialogResult result = MessageBox.Show(
                $"Вы действительно хотите удалить запись (ID: {recordId}) из таблицы '{_tableName}'?",
                "Подтверждение удаления", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 4. Формирование запроса DELETE
                    // Используем параметризацию для безопасности
                    string deleteSql = $"DELETE FROM \"{_tableName}\" WHERE id = @id;";
                    
                    var parameters = new Dictionary<string, object>
                    {
                        { "@id", recordId }
                    };

                    // 5. Выполнение запроса
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(deleteSql, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // 6. Обновление DataGridView
                        RefreshData(null, null); 
                    }
                    else if (rowsAffected == 0)
                    {
                        MessageBox.Show("Запись не найдена или не удалена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    // (Обработка ошибки уже есть внутри ExecuteNonQuery)
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show($"Критическая ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}