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
        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
        System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
        System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyticsForm));
        analyticsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        staffChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        suppliersCategoryChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        topPartsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        panel1 = new System.Windows.Forms.Panel();
        lblTotalPartsCount = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        panel2 = new System.Windows.Forms.Panel();
        lblAvgOrderAmount = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        panel3 = new System.Windows.Forms.Panel();
        lblMaxSupplyCost = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        panel4 = new System.Windows.Forms.Panel();
        lblMinSupplyCost = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        cbAutoRefresh = new System.Windows.Forms.CheckBox();
        ((System.ComponentModel.ISupportInitialize)analyticsChart).BeginInit();
        ((System.ComponentModel.ISupportInitialize)staffChart).BeginInit();
        ((System.ComponentModel.ISupportInitialize)suppliersCategoryChart).BeginInit();
        ((System.ComponentModel.ISupportInitialize)topPartsChart).BeginInit();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        panel3.SuspendLayout();
        panel4.SuspendLayout();
        SuspendLayout();
        // 
        // analyticsChart
        // 
        analyticsChart.BackImageTransparentColor = System.Drawing.Color.White;
        analyticsChart.BackSecondaryColor = System.Drawing.Color.Gainsboro;
        analyticsChart.BorderlineColor = System.Drawing.Color.Silver;
        analyticsChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
        chartArea1.Name = "ChartArea1";
        analyticsChart.ChartAreas.Add(chartArea1);
        legend1.Name = "Legend1";
        analyticsChart.Legends.Add(legend1);
        analyticsChart.Location = new System.Drawing.Point(222, 234);
        analyticsChart.Name = "analyticsChart";
        analyticsChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
        series1.ChartArea = "ChartArea1";
        series1.Legend = "Legend1";
        series1.Name = "Series1";
        analyticsChart.Series.Add(series1);
        analyticsChart.Size = new System.Drawing.Size(404, 320);
        analyticsChart.TabIndex = 0;
        analyticsChart.Text = "chart1";
        // 
        // staffChart
        // 
        chartArea2.Name = "ChartArea1";
        staffChart.ChartAreas.Add(chartArea2);
        legend2.Name = "Legend1";
        staffChart.Legends.Add(legend2);
        staffChart.Location = new System.Drawing.Point(843, 234);
        staffChart.Name = "staffChart";
        staffChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
        series2.ChartArea = "ChartArea1";
        series2.Legend = "Legend1";
        series2.Name = "Series1";
        staffChart.Series.Add(series2);
        staffChart.Size = new System.Drawing.Size(426, 320);
        staffChart.TabIndex = 1;
        staffChart.Text = "chart1";
        // 
        // suppliersCategoryChart
        // 
        chartArea3.Name = "ChartArea1";
        suppliersCategoryChart.ChartAreas.Add(chartArea3);
        legend3.Name = "Legend1";
        suppliersCategoryChart.Legends.Add(legend3);
        suppliersCategoryChart.Location = new System.Drawing.Point(222, 569);
        suppliersCategoryChart.Name = "suppliersCategoryChart";
        suppliersCategoryChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
        series3.ChartArea = "ChartArea1";
        series3.Legend = "Legend1";
        series3.Name = "Series1";
        suppliersCategoryChart.Series.Add(series3);
        suppliersCategoryChart.Size = new System.Drawing.Size(404, 247);
        suppliersCategoryChart.TabIndex = 2;
        suppliersCategoryChart.Text = "chart1";
        // 
        // topPartsChart
        // 
        chartArea4.Name = "ChartArea1";
        topPartsChart.ChartAreas.Add(chartArea4);
        legend4.Name = "Legend1";
        topPartsChart.Legends.Add(legend4);
        topPartsChart.Location = new System.Drawing.Point(843, 569);
        topPartsChart.Name = "topPartsChart";
        topPartsChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
        series4.ChartArea = "ChartArea1";
        series4.Legend = "Legend1";
        series4.Name = "Series1";
        topPartsChart.Series.Add(series4);
        topPartsChart.Size = new System.Drawing.Size(426, 247);
        topPartsChart.TabIndex = 3;
        topPartsChart.Text = "chart1";
        // 
        // panel1
        // 
        panel1.Controls.Add(lblTotalPartsCount);
        panel1.Controls.Add(label1);
        panel1.Location = new System.Drawing.Point(172, 21);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(225, 186);
        panel1.TabIndex = 4;
        // 
        // lblTotalPartsCount
        // 
        lblTotalPartsCount.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        lblTotalPartsCount.Location = new System.Drawing.Point(22, 80);
        lblTotalPartsCount.Name = "lblTotalPartsCount";
        lblTotalPartsCount.Size = new System.Drawing.Size(178, 72);
        lblTotalPartsCount.TabIndex = 1;
        lblTotalPartsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label1.Location = new System.Drawing.Point(22, 20);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(178, 46);
        label1.TabIndex = 0;
        label1.Text = "Всего запчастей";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // panel2
        // 
        panel2.Controls.Add(lblAvgOrderAmount);
        panel2.Controls.Add(label2);
        panel2.Location = new System.Drawing.Point(472, 21);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(225, 186);
        panel2.TabIndex = 5;
        // 
        // lblAvgOrderAmount
        // 
        lblAvgOrderAmount.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        lblAvgOrderAmount.Location = new System.Drawing.Point(25, 80);
        lblAvgOrderAmount.Name = "lblAvgOrderAmount";
        lblAvgOrderAmount.Size = new System.Drawing.Size(178, 72);
        lblAvgOrderAmount.TabIndex = 2;
        lblAvgOrderAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label2.Location = new System.Drawing.Point(25, 20);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(178, 46);
        label2.TabIndex = 1;
        label2.Text = "Средний заказ";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // panel3
        // 
        panel3.Controls.Add(lblMaxSupplyCost);
        panel3.Controls.Add(label3);
        panel3.Location = new System.Drawing.Point(822, 21);
        panel3.Name = "panel3";
        panel3.Size = new System.Drawing.Size(225, 186);
        panel3.TabIndex = 6;
        // 
        // lblMaxSupplyCost
        // 
        lblMaxSupplyCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        lblMaxSupplyCost.Location = new System.Drawing.Point(24, 80);
        lblMaxSupplyCost.Name = "lblMaxSupplyCost";
        lblMaxSupplyCost.Size = new System.Drawing.Size(178, 72);
        lblMaxSupplyCost.TabIndex = 2;
        lblMaxSupplyCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label3
        // 
        label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label3.Location = new System.Drawing.Point(24, 0);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(178, 98);
        label3.TabIndex = 1;
        label3.Text = "Максимальная стоимость  поставки";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // panel4
        // 
        panel4.Controls.Add(lblMinSupplyCost);
        panel4.Controls.Add(label4);
        panel4.Location = new System.Drawing.Point(1122, 21);
        panel4.Name = "panel4";
        panel4.Size = new System.Drawing.Size(225, 186);
        panel4.TabIndex = 7;
        // 
        // lblMinSupplyCost
        // 
        lblMinSupplyCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        lblMinSupplyCost.Location = new System.Drawing.Point(27, 80);
        lblMinSupplyCost.Name = "lblMinSupplyCost";
        lblMinSupplyCost.Size = new System.Drawing.Size(178, 72);
        lblMinSupplyCost.TabIndex = 3;
        lblMinSupplyCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label4
        // 
        label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label4.Location = new System.Drawing.Point(27, -4);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(178, 102);
        label4.TabIndex = 2;
        label4.Text = "Минимальная стоимость  поставки";
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // cbAutoRefresh
        // 
        cbAutoRefresh.Location = new System.Drawing.Point(40, 432);
        cbAutoRefresh.Name = "cbAutoRefresh";
        cbAutoRefresh.Size = new System.Drawing.Size(153, 101);
        cbAutoRefresh.TabIndex = 8;
        cbAutoRefresh.Text = "Автообновление\r\n";
        cbAutoRefresh.UseVisualStyleBackColor = true;
        // 
        // AnalyticsForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        ClientSize = new System.Drawing.Size(1482, 853);
        Controls.Add(cbAutoRefresh);
        Controls.Add(panel4);
        Controls.Add(panel3);
        Controls.Add(panel2);
        Controls.Add(panel1);
        Controls.Add(topPartsChart);
        Controls.Add(suppliersCategoryChart);
        Controls.Add(staffChart);
        Controls.Add(analyticsChart);
        MaximumSize = new System.Drawing.Size(1500, 900);
        MinimumSize = new System.Drawing.Size(1500, 900);
        Text = "AnalyticsForm";
        ((System.ComponentModel.ISupportInitialize)analyticsChart).EndInit();
        ((System.ComponentModel.ISupportInitialize)staffChart).EndInit();
        ((System.ComponentModel.ISupportInitialize)suppliersCategoryChart).EndInit();
        ((System.ComponentModel.ISupportInitialize)topPartsChart).EndInit();
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        panel3.ResumeLayout(false);
        panel4.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.CheckBox cbAutoRefresh;

    private System.Windows.Forms.Label lblAvgOrderAmount;
    private System.Windows.Forms.Label lblMaxSupplyCost;
    private System.Windows.Forms.Label lblMinSupplyCost;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lblTotalPartsCount;

    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.DataVisualization.Charting.Chart suppliersCategoryChart;
    private System.Windows.Forms.DataVisualization.Charting.Chart topPartsChart;

    private System.Windows.Forms.DataVisualization.Charting.Chart staffChart;

    private System.Windows.Forms.DataVisualization.Charting.Chart analyticsChart;

    #endregion
}