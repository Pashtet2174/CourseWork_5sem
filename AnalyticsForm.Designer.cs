using System.ComponentModel;

namespace CourseWork_5sem;

partial class AnalyticsForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
        System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
        System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
        analyticsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        metricsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        ((System.ComponentModel.ISupportInitialize)analyticsChart).BeginInit();
        ((System.ComponentModel.ISupportInitialize)metricsChart).BeginInit();
        SuspendLayout();
        // 
        // analyticsChart
        // 
        chartArea1.Name = "ChartArea1";
        analyticsChart.ChartAreas.Add(chartArea1);
        legend1.Name = "Legend1";
        analyticsChart.Legends.Add(legend1);
        analyticsChart.Location = new System.Drawing.Point(77, 87);
        analyticsChart.Name = "analyticsChart";
        series1.ChartArea = "ChartArea1";
        series1.Legend = "Legend1";
        series1.Name = "Series1";
        analyticsChart.Series.Add(series1);
        analyticsChart.Size = new System.Drawing.Size(334, 220);
        analyticsChart.TabIndex = 0;
        analyticsChart.Text = "chart1";
        // 
        // metricsChart
        // 
        chartArea2.Name = "ChartArea1";
        metricsChart.ChartAreas.Add(chartArea2);
        legend2.Name = "Legend1";
        metricsChart.Legends.Add(legend2);
        metricsChart.Location = new System.Drawing.Point(445, 92);
        metricsChart.Name = "metricsChart";
        series2.ChartArea = "ChartArea1";
        series2.Legend = "Legend1";
        series2.Name = "Series1";
        metricsChart.Series.Add(series2);
        metricsChart.Size = new System.Drawing.Size(333, 215);
        metricsChart.TabIndex = 1;
        metricsChart.Text = "chart1";
        // 
        // AnalyticsForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(metricsChart);
        Controls.Add(analyticsChart);
        Text = "AnalyticsForm";
        ((System.ComponentModel.ISupportInitialize)analyticsChart).EndInit();
        ((System.ComponentModel.ISupportInitialize)metricsChart).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.DataVisualization.Charting.Chart metricsChart;

    private System.Windows.Forms.DataVisualization.Charting.Chart analyticsChart;

    #endregion
}