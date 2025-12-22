using System.Data;
using System.Text;

namespace CourseWork_5sem;

public partial class DocumentForm : Form
{
    private readonly SaveFileDialog saveFileDialogCsv;
    
    public DocumentForm()
    {
        InitializeComponent();
        this.Text = "Подготовка выходных документов и отчетов";
        FontManager.FontSizeChanged += FontManager_FontSizeChanged;
        ApplyNewFontSize(FontManager.CurrentFontSize); 
        saveFileDialogCsv = new SaveFileDialog();
        this.TopLevel = false;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Dock = DockStyle.Fill; 

        dataGridViewResults.AllowUserToAddRows = false;
    }
    private void BtnExecuteQuery_Click(object sender, EventArgs e)
    {
        string sql = textBoxQuery.Text.Trim();
        
        if (string.IsNullOrEmpty(sql))
        {
            MessageBox.Show("Введите SQL-запрос для выполнения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
             MessageBox.Show("Разрешены только запросы, начинающиеся с SELECT.", "Ошибка безопасности", MessageBoxButtons.OK, MessageBoxIcon.Error);
             return;
        }

        try
        {
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

    
    private void BtnExportToCsv_Click(object sender, EventArgs e)
    {
        if (dataGridViewResults.DataSource == null || !(dataGridViewResults.DataSource is DataTable dt) || dt.Rows.Count == 0)
        {
            MessageBox.Show("Нет данных для экспорта. Выполните запрос.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        saveFileDialogCsv.Filter = "CSV files (*.csv)|*.csv";
        saveFileDialogCsv.Title = "Экспорт данных в CSV";
        saveFileDialogCsv.FileName = "Report_" + DateTime.Now.ToString("yyyyMMdd_HHmm");

        if (saveFileDialogCsv.ShowDialog() == DialogResult.OK)
        {
            ExportDataTableToCsv(dt, saveFileDialogCsv.FileName);
        }
    }

    private void ExportDataTableToCsv(DataTable dt, string filePath)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName);
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.WriteLine();

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string value = dr[i].ToString()
                            .Replace("\"", "\"\"") 
                            .Replace("\n", " ")
                            .Replace("\r", " "); 
                        
                        sw.Write($"\"{value}\""); 
                        
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

    private string GetPartsQuery()
    {
        return @"
    SELECT 
      name AS ""Название детали"", 
      article AS ""Артикул""
    FROM parts
    ORDER BY name;";
    }
    private void button3_Click(object sender, EventArgs e)
    {
        textBoxQuery.Text = GetPartsQuery().Trim();
    }
    private string GetSupplierQuery()
    {
        return @"
    SELECT 
        s.id AS ""ID поставщика"",
        s.name AS ""Название поставщика"",
        CASE 
            WHEN s.category = 'Firms' THEN 'Фирмы'
            WHEN s.category = 'Manufacturer' THEN 'Производитель'
            WHEN s.category = 'dealer' THEN 'Дилер'
            WHEN s.category = 'Small_production' THEN 'Мелкое производство'
            WHEN s.category = 'Small_supplier' THEN 'Мелкий поставщик'
            WHEN s.category = 'Store' THEN 'Магазин'
        END AS ""Категория поставщика"",
        s.phone_number AS ""Телефон поставщика"",
        sup.part_id AS ""ID детали"",
        sup.part_quantity AS ""Количество деталей""
    FROM suppliers s
    LEFT JOIN supplies sup ON s.id = sup.supplier_id
    ORDER BY s.id;";
    }

    private void button4_Click(object sender, EventArgs e)
    {
        textBoxQuery.Text = GetSupplierQuery().Trim();
    }
    
    private string GetDetailedEmployeeQuery()
    {
        return @"
        SELECT 
            e.id AS ""ID сотрудника"",
            e.last_name AS ""Фамилия"",
            e.first_name AS ""Имя"",
            e.middle_name AS ""Отчество"",
            CASE 
                WHEN e.gender = 'M' THEN 'Мужской'
                WHEN e.gender = 'F' THEN 'Женский'
            END AS ""Пол"",
            e.birth_date AS ""Дата рождения"",
            e.house_number AS ""Номер дома"",
            e.work_experience AS ""Стаж работы"",
            c.name AS ""Страна"",
            ci.name AS ""Город"",
            s.name AS ""Улица"",
            p.name AS ""Должность"",
            sp.name AS ""Специальность"",
            w.name AS ""Место работы"",
            d.name AS ""Отдел"",
            q.name AS ""Квалификация"",
            pr.name AS ""Профессия"",
            wr.record_date AS ""Дата записи"",
            CASE 
                WHEN wr.event_type = 'Hiring' THEN 'Прием на работу'
                WHEN wr.event_type = 'Dismissal' THEN 'Увольнение'
                WHEN wr.event_type = 'Transfer' THEN 'Перевод'
            END AS ""Тип события"",
            wr.termination_reason AS ""Причина увольнения""
        FROM employees e
        LEFT JOIN countries c ON e.country_id = c.id
        LEFT JOIN cities ci ON e.city_id = ci.id
        LEFT JOIN streets s ON e.street_id = s.id
        LEFT JOIN work_records wr ON e.id = wr.employee_id
        LEFT JOIN positions p ON wr.position_id = p.id
        LEFT JOIN specialties sp ON wr.specialty_id = sp.id
        LEFT JOIN workplaces w ON wr.workplace_id = w.id
        LEFT JOIN departments d ON wr.department_id = d.id
        LEFT JOIN qualifications q ON wr.qualification_id = q.id
        LEFT JOIN professions pr ON wr.profession_id = pr.id
        ORDER BY e.id, wr.record_date DESC;";
    }
    
    private void button6_Click(object sender, EventArgs e)
    {
        textBoxQuery.Text = GetDetailedEmployeeQuery().Trim();
    }
    
    private string GetDetailedRequestQuery()
    {
        return @"
    SELECT 
        pr.id AS ""ID Заявки"",
        pr.creation_date AS ""Дата создания"",
        pr.status AS ""Статус"",
        pr.request_amount AS ""Сумма запроса"",
        pr.operation_amount AS ""Сумма операции"",
        pr.operation_date AS ""Дата операции"",
        b.last_name AS ""Фамилия покупателя"",
        b.first_name AS ""Имя покупателя"",
        b.middle_name AS ""Отчество покупателя"",
        p.name AS ""Название детали"",
        p.article AS ""Артикул"",
        rp.part_quantity AS ""Количество детали"",
        m.name AS ""Производитель"",
        c.name AS ""Страна""
    FROM 
        product_requests pr 
    INNER JOIN 
        buyers b ON pr.buyer_id = b.id
    INNER JOIN 
        request_parts rp ON pr.id = rp.product_request_id
    INNER JOIN 
        parts p ON rp.part_id = p.id
    INNER JOIN 
        manufacturers m ON p.manufacturer_id = m.id
    INNER JOIN 
        countries c ON p.country_id = c.id
    ORDER BY 
        pr.creation_date DESC;";
    }

    private void button5_Click(object sender, EventArgs e)
    {
        textBoxQuery.Text = GetDetailedRequestQuery().Trim();
    }
    
    private void FontManager_FontSizeChanged(object sender, EventArgs e)
    {
        ApplyNewFontSize(FontManager.CurrentFontSize);
    }
    
    private void ApplyNewFontSize(float newSize)
    {
        this.Font = new Font(this.Font.FontFamily, newSize, this.Font.Style);
        foreach (Control control in this.Controls)
        {
            UpdateControlFont(control, newSize);
        }
    }
    
    private void UpdateControlFont(Control parent, float newSize)
    {
        if (parent.Font != null)
        {
            parent.Font = new Font(parent.Font.FontFamily, newSize, parent.Font.Style);
        }
        foreach (Control child in parent.Controls)
        {
            UpdateControlFont(child, newSize);
        }
    }
    
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        FontManager.FontSizeChanged -= FontManager_FontSizeChanged;
        base.OnFormClosed(e);
    }
    
}