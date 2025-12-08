using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
namespace CourseWork_5sem;

public partial class AnalyticsForm : Form
{
    public AnalyticsForm()
    {
        InitializeComponent();
        this.Text = "Аналитика и Отчеты";

        // Настройка формы для ВСТРАИВАНИЯ
        this.TopLevel = false;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Dock = DockStyle.Fill; 

        // =======================================================
        // ЛОГИКА УПРАВЛЕНИЯ ШРИФТОМ (для всех элементов формы)
        // =======================================================
        FontManager.FontSizeChanged += FontManager_FontSizeChanged;
        ApplyNewFontSize(FontManager.CurrentFontSize); 
        // =======================================================
        
        // Вызов метода загрузки аналитики при старте
        this.Load += (s, e) => LoadMetricsData();; 
    }

    /// <summary>
    /// Метод для загрузки данных и построения графиков.
    /// </summary>
    private void LoadAnalyticsData()
    {
        try
        {
            // Запрос, который возвращает данные для графика
            string sqlQuery = @"
                SELECT 
                    TO_CHAR(creation_date, 'YYYY-MM') AS month_year, 
                    COUNT(id) AS orders_count
                FROM 
                    customer_orders
                GROUP BY 
                    month_year
                ORDER BY 
                    month_year;";

            DataTable dt = DatabaseHelper.ExecuteDataTable(sqlQuery);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Предполагается, что у вас есть компонент Chart с именем analyticsChart
                // Очищаем старые серии и устанавливаем заголовок
                analyticsChart.Series.Clear();
                analyticsChart.Titles.Clear();
                analyticsChart.Titles.Add("Количество заказов по месяцам");

                Series series = new Series("Заказы")
                {
                    ChartType = SeriesChartType.Column, // Тип графика: столбчатая диаграмма
                    BorderWidth = 2
                };

                // Заполняем серию данными
                foreach (DataRow row in dt.Rows)
                {
                    string month = row["month_year"].ToString();
                    int count = Convert.ToInt32(row["orders_count"]);
                    series.Points.AddXY(month, count);
                }

                analyticsChart.Series.Add(series);
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
            // Ваш SQL-запрос
            string sqlQuery = @"
                SELECT 'total_parts_count' AS stat, COUNT(*)::TEXT AS val FROM parts
                UNION
                SELECT 'avg_order_amount', AVG(order_amount)::TEXT FROM customer_orders
                UNION  
                SELECT 'max_part_quantity', MAX(part_quantity)::TEXT FROM supplies
                UNION
                SELECT 'min_part_quantity', MIN(part_quantity)::TEXT FROM supplies;";

            DataTable dt = DatabaseHelper.ExecuteDataTable(sqlQuery);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Предполагается, что у вас есть второй компонент Chart с именем metricsChart
                metricsChart.Series.Clear();
                metricsChart.Titles.Clear();
                metricsChart.Titles.Add("Сводные агрегированные метрики");

                Series series = new Series("Метрики")
                {
                    ChartType = SeriesChartType.Doughnut, // Используем Пончиковую диаграмму
                    BorderWidth = 1,
                    IsValueShownAsLabel = true // Показываем значения на секторах
                };
                
                // Настраиваем легенду (показывает названия метрик)
                metricsChart.Legends.Clear();
                metricsChart.Legends.Add("Legend1");
                metricsChart.Legends[0].Docking = Docking.Bottom;
                
                // Заполняем серию данными
                foreach (DataRow row in dt.Rows)
                {
                    string statName = row["stat"].ToString();
                    double value;
                    
                    // Попытка преобразовать значение (используем CultureInfo.InvariantCulture для парсинга)
                    if (double.TryParse(row["val"].ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value))
                    {
                        // Добавляем точку в диаграмму с названием метрики и значением
                        DataPoint dp = new DataPoint(0, value)
                        {
                            LegendText = GetStatDisplayName(statName),
                            Label = $"{GetStatDisplayName(statName)}: {value:N0}" // Форматируем значение
                        };
                        series.Points.Add(dp);
                    }
                }
                
                metricsChart.Series.Add(series);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки метрик: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Преобразует техническое имя метрики в понятное название для пользователя.
    /// </summary>
    private string GetStatDisplayName(string statName)
    {
        return statName switch
        {
            "total_parts_count" => "Всего деталей",
            "avg_order_amount" => "Средний заказ",
            "max_supply_cost" => "Макс. стоимость поставки",
            "min_supply_cost" => "Мин. стоимость поставки",
            _ => statName,
        };
    }
    // =========================================================================
    // МЕТОДЫ УПРАВЛЕНИЯ ШРИФТОМ
    // =========================================================================

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
    
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        FontManager.FontSizeChanged -= FontManager_FontSizeChanged;
        base.OnFormClosed(e);
    }
}