using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace CourseWork_5sem;

public partial class AnalyticsForm : Form
{
    private System.Windows.Forms.Timer refreshTimer;
    public AnalyticsForm()
    {
        InitializeComponent();
        this.Text = "Аналитика и Отчеты";
        this.TopLevel = false;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Dock = DockStyle.Fill; 
        FontManager.FontSizeChanged += FontManager_FontSizeChanged;
        ApplyNewFontSize(FontManager.CurrentFontSize); 
        this.Load += (s, e) => 
        {    // Загружаем данные сразу
            InitRealTimeUpdate();  // Настраиваем управление таймером
            InitChartContextMenu(); // Настраиваем правую кнопку мыши
        };
        // Вызов метода загрузки аналитики при старте
        this.Load += (s, e) => LoadMetricsData();
        this.Load += (s, e) => LoadAnalyticsData();
        this.Load += (s, e) => LoadHiringAnalytics();
        this.Load += (s, e) => LoadSuppliersCategoryAnalytics();
        this.Load += (s, e) => LoadTopPartsAnalytics();
        InitChartContextMenu();
    }
    
    private void LoadAnalyticsData()
{
    try
    {
        // SQL запрос (используем order_date согласно вашей схеме БД)
        string sqlQuery = @"
            SELECT 
                TO_CHAR(order_date, 'YYYY-MM') AS month_year, 
                COUNT(id) AS orders_count
            FROM 
                customer_orders
            GROUP BY 
                month_year, DATE_TRUNC('month', order_date)
            ORDER BY 
                DATE_TRUNC('month', order_date);";

        DataTable dt = DatabaseHelper.ExecuteDataTable(sqlQuery);

        if (dt != null)
        {
            // 1. Очистка старых данных и заголовков
            analyticsChart.Series.Clear();
            analyticsChart.Titles.Clear();

            // 2. Добавление названия графика
            Title chartTitle = new Title();
            chartTitle.Text = "Динамика заказов по месяцам"; // Текст заголовка
            chartTitle.Font = new Font("Arial", 14, FontStyle.Bold); // Шрифт и размер
            chartTitle.ForeColor = Color.FromArgb(45, 45, 45); // Цвет текста
            chartTitle.Alignment = ContentAlignment.TopCenter; // Выравнивание
            analyticsChart.Titles.Add(chartTitle);

            // 3. Создание и настройка серии данных
            Series series = new Series("Заказы")
            {
                ChartType = SeriesChartType.Column, // Столбчатая диаграмма
                Palette = ChartColorPalette.SeaGreen, // Приятный цвет
                IsValueShownAsLabel = true // Показывать числа прямо над столбиками
            };

            // 4. Заполнение данными
            foreach (DataRow row in dt.Rows)
            {
                string month = row["month_year"].ToString();
                int count = Convert.ToInt32(row["orders_count"]);
                series.Points.AddXY(month, count);
            }

            analyticsChart.Series.Add(series);
            
            // 5. Настройка осей (чтобы подписи не слипались)
            analyticsChart.ChartAreas[0].AxisX.Title = "Период";
            analyticsChart.ChartAreas[0].AxisY.Title = "Кол-во заказов";
            analyticsChart.ChartAreas[0].AxisX.Interval = 1;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка загрузки аналитики: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
    //получение и отображение метрик в таблице
    private void LoadMetricsData()
{
    try
    {
        // SQL запрос остается прежним
        string sqlQuery = @"
            SELECT 'total_parts_count' AS stat, COUNT(*)::TEXT AS val FROM parts
            UNION
            SELECT 'avg_order_amount', ROUND(AVG(order_amount), 2)::TEXT FROM customer_orders
            UNION  
            SELECT 'max_supply_cost', MAX(part_quantity)::TEXT FROM supplies
            UNION
            SELECT 'min_supply_cost', MIN(part_quantity)::TEXT FROM supplies;";

        DataTable dt = DatabaseHelper.ExecuteDataTable(sqlQuery);

        if (dt != null && dt.Rows.Count > 0)
        {
            // 1. Обновление текстовых меток (ваша "таблица")
            foreach (DataRow row in dt.Rows)
            {
                string statName = row["stat"].ToString();
                string value = row["val"].ToString();

                // Присваиваем значение конкретному Label в зависимости от имени метрики
                switch (statName)
                {
                    case "total_parts_count":
                        lblTotalPartsCount.Text = value;
                        break;
                    case "avg_order_amount":
                        lblAvgOrderAmount.Text = $"{value} руб.";
                        break;
                    case "max_supply_cost":
                        lblMaxSupplyCost.Text = value;
                        break;
                    case "min_supply_cost":
                        lblMinSupplyCost.Text = value;
                        break;
                }
            }

        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка загрузки метрик: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
private void LoadHiringAnalytics()
{
    try
    {
        // Запрос считает количество записей о найме (Hiring) по месяцам
        string sqlQuery = @"
            SELECT 
                TO_CHAR(record_date, 'YYYY-MM') AS month_year, 
                COUNT(id) AS hire_count
            FROM 
                work_records
            WHERE 
                event_type = 'Hiring'
            GROUP BY 
                month_year, DATE_TRUNC('month', record_date)
            ORDER BY 
                DATE_TRUNC('month', record_date);";

        DataTable dt = DatabaseHelper.ExecuteDataTable(sqlQuery);

        if (dt != null)
        {
            // Предположим, вы добавили на форму Chart с именем staffChart
            staffChart.Series.Clear();
            staffChart.Titles.Clear();

            // Настройка заголовка
            Title title = new Title("Динамика найма сотрудников", Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black);
            staffChart.Titles.Add(title);

            // Линейный график лучше подходит для динамики персонала
            Series series = new Series("Новые сотрудники")
            {
                ChartType = SeriesChartType.Line, 
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8,
                BorderWidth = 3,
                Color = Color.RoyalBlue,
                IsValueShownAsLabel = true
            };

            foreach (DataRow row in dt.Rows)
            {
                series.Points.AddXY(row["month_year"].ToString(), Convert.ToInt32(row["hire_count"]));
            }

            staffChart.Series.Add(series);
            
            // Настройка сетки для красоты
            staffChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            staffChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка статистики персонала: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
    
private void LoadSuppliersCategoryAnalytics()
{
    try
    {
        string sqlQuery = @"
            SELECT category::TEXT, COUNT(id) AS supplier_count
            FROM suppliers
            GROUP BY category;";

        DataTable dt = DatabaseHelper.ExecuteDataTable(sqlQuery);

        if (dt != null)
        {
            suppliersCategoryChart.Series.Clear();
            suppliersCategoryChart.Titles.Clear();

            // Заголовок
            Title title = new Title("Поставщики по категориям");
            title.Font = new Font("Arial", 12, FontStyle.Bold);
            suppliersCategoryChart.Titles.Add(title);

            // Настройка круговой диаграммы
            Series series = new Series("Categories")
            {
                ChartType = SeriesChartType.Pie, // Круговая диаграмма
                IsValueShownAsLabel = true,        // Показать в процентах (опционально)
            };

            // Кастомные настройки отображения
            series["PieLabelStyle"] = "Outside"; // Вынести подписи за пределы круга
            series.BorderColor = Color.White;
            series.BorderWidth = 2;
            suppliersCategoryChart.ChartAreas[0].Position.Auto = true; 
            suppliersCategoryChart.ChartAreas[0].InnerPlotPosition = new ElementPosition(5, 5, 90, 90);

            foreach (DataRow row in dt.Rows)
            {
                string category = row["category"].ToString();
                int count = Convert.ToInt32(row["supplier_count"]);
                
                // Добавляем точку и переводим техническое имя на русский
                int index = series.Points.AddXY(TranslateCategory(category), count);
                series.Points[index].LegendText = TranslateCategory(category);
            }

            suppliersCategoryChart.Series.Add(series);
            
            // Настройка легенды
            if (suppliersCategoryChart.Legends.Count > 0)
            {
                suppliersCategoryChart.Legends[0].Enabled = true;
                suppliersCategoryChart.Legends[0].Docking = Docking.Bottom;
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка статистики поставщиков: {ex.Message}");
    }
}

// Вспомогательный метод для перевода категорий
private string TranslateCategory(string category)
{
    return category switch
    {
        "Firms" => "Фирмы",
        "Manufacturer" => "Производители",
        "dealer" => "Дилеры",
        "Small_production" => "Малое производство",
        "Small_supplier" => "Мелкие поставщики",
        "Store" => "Магазины",
        _ => category
    };
}
private void LoadTopPartsAnalytics()
{
    try
    {
        string sqlQuery = @"
            SELECT p.name AS part_name, SUM(rp.part_quantity) AS total_sold
            FROM parts p
            JOIN request_parts rp ON p.id = rp.part_id
            JOIN product_requests pr ON rp.product_request_id = pr.id
            WHERE pr.status = 'Sale'
            GROUP BY p.name
            ORDER BY total_sold DESC
            LIMIT 5;";

        DataTable dt = DatabaseHelper.ExecuteDataTable(sqlQuery);

        if (dt != null)
        {
            topPartsChart.Series.Clear();
            topPartsChart.Titles.Clear();

            // Заголовок графика
            Title title = new Title("Топ-5 продаваемых деталей");
            title.Font = new Font("Arial", 12, FontStyle.Bold);
            topPartsChart.Titles.Add(title);

            // Настройка Horizontal Bar Chart
            Series series = new Series("Количество")
            {
                ChartType = SeriesChartType.Bar, // Горизонтальные столбцы
                Color = Color.MediumSlateBlue,
                IsValueShownAsLabel = true // Показывает количество прямо на графике
            };

            foreach (DataRow row in dt.Rows)
            {
                // Добавляем данные: Имя детали и количество
                series.Points.AddXY(row["part_name"].ToString(), Convert.ToInt32(row["total_sold"]));
            }

            topPartsChart.Series.Add(series);

            // Настройка осей для красоты
            topPartsChart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            topPartsChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // Убираем вертикальные линии
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка загрузки статистики продаж: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
    
    // МЕТОДЫ УПРАВЛЕНИЯ ШРИФТОМ

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
        
        // Применяем шрифт к элементам чарта, если это необходимо
        if (parent is Chart chart)
        {
            // Пример: изменить шрифт заголовка
            if (chart.Titles.Count > 0)
            {
                chart.Titles[0].Font = new Font(chart.Titles[0].Font.FontFamily, newSize + 2, FontStyle.Bold);
            }
        }

        foreach (Control child in parent.Controls)
        {
            UpdateControlFont(child, newSize);
        }
    }
    
    private void InitRealTimeUpdate()
    {
        refreshTimer = new System.Windows.Forms.Timer();
        refreshTimer.Interval = 30000; // 30 секунд
        refreshTimer.Tick += (s, e) => RefreshAllData();

        // Привязываем включение/выключение таймера к CheckBox
        cbAutoRefresh.CheckedChanged += (s, e) => 
        {
            if (cbAutoRefresh.Checked)
            {
                refreshTimer.Start();
                cbAutoRefresh.ForeColor = Color.Green; // Визуальная индикация включения
            }
            else
            {
                refreshTimer.Stop();
                cbAutoRefresh.ForeColor = Color.Black; // Обычный цвет при выключении
            }
        };

        // По умолчанию выключен (или включен, если поставить cbAutoRefresh.Checked = true)
        refreshTimer.Enabled = cbAutoRefresh.Checked;
    }

    private void RefreshAllData()
    {
        LoadMetricsData();
        LoadAnalyticsData();
        LoadHiringAnalytics();
        LoadSuppliersCategoryAnalytics();
        LoadTopPartsAnalytics();
    }
    
    private void InitChartContextMenu()
    {
        // Создаем контекстное меню
        ContextMenuStrip chartMenu = new ContextMenuStrip();

        // Добавляем пункт "Сохранить как изображение"
        ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить как изображение...");
        saveItem.Click += (s, e) => 
        {
            // Определяем, на каком графике было вызвано меню
            if (chartMenu.SourceControl is Chart targetChart)
            {
                ExportChartToPng(targetChart, targetChart.Name);
            }
        };

        // Добавляем пункт "Обновить данные"
        ToolStripMenuItem refreshItem = new ToolStripMenuItem("Обновить этот график");
        refreshItem.Click += (s, e) => RefreshAllData();

        chartMenu.Items.Add(saveItem);
        chartMenu.Items.Add(new ToolStripSeparator()); // Разделитель
        chartMenu.Items.Add(refreshItem);

        // Привязываем меню ко всем вашим графикам
        analyticsChart.ContextMenuStrip = chartMenu;
        staffChart.ContextMenuStrip = chartMenu;
        suppliersCategoryChart.ContextMenuStrip = chartMenu;
        topPartsChart.ContextMenuStrip = chartMenu;
    }
    
    private void ExportChartToPng(Chart chart, string chartName)
    {
        try
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                sfd.Title = "Выберите место для сохранения графика";
                sfd.FileName = $"{chartName}_{DateTime.Now:yyyy-MM-dd_HHmm}";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Выбираем формат на основе расширения
                    ChartImageFormat format = ChartImageFormat.Png;
                    if (sfd.FileName.EndsWith(".jpg")) format = ChartImageFormat.Jpeg;
                    if (sfd.FileName.EndsWith(".bmp")) format = ChartImageFormat.Bmp;

                    chart.SaveImage(sfd.FileName, format);
                    MessageBox.Show($"График '{chartName}' успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось сохранить изображение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        FontManager.FontSizeChanged -= FontManager_FontSizeChanged;
        base.OnFormClosed(e);
    }

    
}