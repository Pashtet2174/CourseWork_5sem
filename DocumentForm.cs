using System.Data;
using System.Text;

namespace CourseWork_5sem;

public partial class DocumentForm : Form
{
    // Словарь для хранения шаблонов запросов. 
    // Заполним их позже, как вы просили.
    private readonly SaveFileDialog saveFileDialogCsv;
    private readonly Dictionary<string, string> _queryTemplates = new Dictionary<string, string>
    {
        {"Заявки с деталями", "SELECT * FROM public.product_requests; -- Ваш готовый запрос 1"},
        {"Продажи по сотрудникам", "SELECT * FROM public.employees; -- Ваш готовый запрос 2"},
        // Добавляйте сюда другие готовые запросы
    };
    
    public DocumentForm()
    {
        InitializeComponent();
        this.Text = "Подготовка выходных документов и отчетов";

        // =======================================================
        // ДОБАВЛЕНИЕ: Настройка формы для встраивания (MDI-стиль)
        // =======================================================
        saveFileDialogCsv = new SaveFileDialog();
        this.TopLevel = false;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Dock = DockStyle.Fill; // Заполняем родительский контейнер
        // =======================================================

        // Настройка DataGridView
        dataGridViewResults.AllowUserToAddRows = false;
    
        // Инициализация кнопок шаблонов
        InitializeQueryTemplates();
    }

    private void InitializeQueryTemplates()
    {
        foreach (var template in _queryTemplates)
        {
            Button btn = new Button
            {
                Text = template.Key,
                Tag = template.Value, // SQL-запрос хранится в Tag
                AutoSize = true,
                Margin = new Padding(5)
            };
            btn.Click += TemplateButton_Click;
            flowLayoutPanelTemplates.Controls.Add(btn);
        }
    }
    
    // Обработчик нажатия на кнопки готовых запросов
    private void TemplateButton_Click(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.Tag is string query)
        {
            textBoxQuery.Text = query;
        }
    }
    
    // -------------------------------------------------------------------------
    // ЛОГИКА ЗАПУСКА ЗАПРОСА
    // -------------------------------------------------------------------------

    private void BtnExecuteQuery_Click(object sender, EventArgs e)
    {
        string sql = textBoxQuery.Text.Trim();
        
        if (string.IsNullOrEmpty(sql))
        {
            MessageBox.Show("Введите SQL-запрос для выполнения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Убеждаемся, что запрос безопасен (только SELECT)
        if (!sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
             MessageBox.Show("Разрешены только запросы, начинающиеся с SELECT.", "Ошибка безопасности", MessageBoxButtons.OK, MessageBoxIcon.Error);
             return;
        }

        try
        {
            // Используем универсальный метод из DatabaseHelper для получения DataTable
            DataTable dt = DatabaseHelper.ExecuteDataTable(sql); 
            
            if (dt != null)
            {
                dataGridViewResults.DataSource = dt;
                MessageBox.Show($"Запрос выполнен успешно. Получено {dt.Rows.Count} записей.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dataGridViewResults.DataSource = null;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            dataGridViewResults.DataSource = null;
        }
    }

    // -------------------------------------------------------------------------
    // ЛОГИКА ЭКСПОРТА В CSV
    // -------------------------------------------------------------------------
    
    private void BtnExportToCsv_Click(object sender, EventArgs e)
    {
        if (dataGridViewResults.DataSource == null || !(dataGridViewResults.DataSource is DataTable dt) || dt.Rows.Count == 0)
        {
            MessageBox.Show("Нет данных для экспорта. Выполните запрос.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        // Настройка диалога сохранения
        saveFileDialogCsv.Filter = "CSV files (*.csv)|*.csv";
        saveFileDialogCsv.Title = "Экспорт данных в CSV";
        saveFileDialogCsv.FileName = "Report_" + DateTime.Now.ToString("yyyyMMdd_HHmm");

        if (saveFileDialogCsv.ShowDialog() == DialogResult.OK)
        {
            ExportDataTableToCsv(dt, saveFileDialogCsv.FileName);
        }
    }

    /// <summary>
    /// Экспортирует DataTable в CSV с разделителем ';' и кодировкой UTF-8.
    /// </summary>
    private void ExportDataTableToCsv(DataTable dt, string filePath)
    {
        try
        {
            // Используем UTF8 для корректного отображения кириллицы
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Заголовки (Header)
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName);
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.WriteLine();

                // Данные (Rows)
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // Заменяем переносы строк и двойные кавычки и заключаем в кавычки для безопасности
                        string value = dr[i].ToString()
                            .Replace("\"", "\"\"") // Экранирование кавычек
                            .Replace("\n", " ")
                            .Replace("\r", " "); 
                        
                        sw.Write($"\"{value}\""); // Заключаем в двойные кавычки
                        
                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(";");
                        }
                    }
                    sw.WriteLine();
                }
            }
            MessageBox.Show($"Данные успешно экспортированы в файл:\n{filePath}", "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при экспорте в CSV: {ex.Message}", "Ошибка экспорта", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private void button6_Click(object sender, EventArgs e)
    {
            throw new System.NotImplementedException();
    }
    
}