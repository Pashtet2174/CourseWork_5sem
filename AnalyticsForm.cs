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
        {    
            InitRealTimeUpdate();  
            InitChartContextMenu(); 
        };
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
            analyticsChart.Series.Clear();
            analyticsChart.Titles.Clear();

            Title chartTitle = new Title();
            chartTitle.Text = "Динамика заказов по месяцам"; 
            chartTitle.Font = new Font("Arial", 14, FontStyle.Bold); 
            chartTitle.ForeColor = Color.FromArgb(45, 45, 45); 
            chartTitle.Alignment = ContentAlignment.TopCenter; 
            analyticsChart.Titles.Add(chartTitle);

            Series series = new Series("Заказы")
            {
                ChartType = SeriesChartType.Column, 
                Palette = ChartColorPalette.SeaGreen, 
                IsValueShownAsLabel = true 
            };

            foreach (DataRow row in dt.Rows)
            {
                string month = row["month_year"].ToString();
                int count = Convert.ToInt32(row["orders_count"]);
                series.Points.AddXY(month, count);
            }

            analyticsChart.Series.Add(series);
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
    private void LoadMetricsData()
{
    try
    {
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
            foreach (DataRow row in dt.Rows)
            {
                string statName = row["stat"].ToString();
                string value = row["val"].ToString();

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
            staffChart.Series.Clear();
            staffChart.Titles.Clear();
            Title title = new Title("Динамика найма сотрудников", Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black);
            staffChart.Titles.Add(title);
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

            Title title = new Title("Поставщики по категориям");
            title.Font = new Font("Arial", 12, FontStyle.Bold);
            suppliersCategoryChart.Titles.Add(title);
            Series series = new Series("Categories")
            {
                ChartType = SeriesChartType.Pie, 
                IsValueShownAsLabel = true,        
            };

            series["PieLabelStyle"] = "Outside"; 
            series.BorderColor = Color.White;
            series.BorderWidth = 2;
            suppliersCategoryChart.ChartAreas[0].Position.Auto = true; 
            suppliersCategoryChart.ChartAreas[0].InnerPlotPosition = new ElementPosition(5, 5, 90, 90);

            foreach (DataRow row in dt.Rows)
            {
                string category = row["category"].ToString();
                int count = Convert.ToInt32(row["supplier_count"]);
                int index = series.Points.AddXY(TranslateCategory(category), count);
                series.Points[index].LegendText = TranslateCategory(category);
            }

            suppliersCategoryChart.Series.Add(series);
            
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
            Title title = new Title("Топ-5 продаваемых деталей");
            title.Font = new Font("Arial", 12, FontStyle.Bold);
            topPartsChart.Titles.Add(title);

            Series series = new Series("Количество")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.MediumSlateBlue,
                IsValueShownAsLabel = true 
            };

            foreach (DataRow row in dt.Rows)
            {
                series.Points.AddXY(row["part_name"].ToString(), Convert.ToInt32(row["total_sold"]));
            }

            topPartsChart.Series.Add(series);

            topPartsChart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            topPartsChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false; 
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка загрузки статистики продаж: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
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
        
        if (parent is Chart chart)
        {
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

        cbAutoRefresh.CheckedChanged += (s, e) => 
        {
            if (cbAutoRefresh.Checked)
            {
                refreshTimer.Start();
                cbAutoRefresh.ForeColor = Color.Green; 
            }
            else
            {
                refreshTimer.Stop();
                cbAutoRefresh.ForeColor = Color.Black; 
            }
        };

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
        ContextMenuStrip chartMenu = new ContextMenuStrip();

        ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить как изображение...");
        saveItem.Click += (s, e) => 
        {
            if (chartMenu.SourceControl is Chart targetChart)
            {
                ExportChartToPng(targetChart, targetChart.Name);
            }
        };

        ToolStripMenuItem refreshItem = new ToolStripMenuItem("Обновить этот график");
        refreshItem.Click += (s, e) => RefreshAllData();

        chartMenu.Items.Add(saveItem);
        chartMenu.Items.Add(new ToolStripSeparator()); 
        chartMenu.Items.Add(refreshItem);

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