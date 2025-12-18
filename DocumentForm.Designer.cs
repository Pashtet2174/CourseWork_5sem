using System.ComponentModel;

namespace CourseWork_5sem;

partial class DocumentForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentForm));
        textBoxQuery = new System.Windows.Forms.TextBox();
        dataGridViewResults = new System.Windows.Forms.DataGridView();
        flowLayoutPanelTemplates = new System.Windows.Forms.FlowLayoutPanel();
        btnExecuteQuery = new System.Windows.Forms.Button();
        btnExportToCsv = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        panel1 = new System.Windows.Forms.Panel();
        ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
        flowLayoutPanelTemplates.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // textBoxQuery
        // 
        textBoxQuery.BackColor = System.Drawing.SystemColors.Info;
        textBoxQuery.Dock = System.Windows.Forms.DockStyle.Left;
        textBoxQuery.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        textBoxQuery.Location = new System.Drawing.Point(0, 0);
        textBoxQuery.Margin = new System.Windows.Forms.Padding(4);
        textBoxQuery.Multiline = true;
        textBoxQuery.Name = "textBoxQuery";
        textBoxQuery.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
        textBoxQuery.Size = new System.Drawing.Size(973, 476);
        textBoxQuery.TabIndex = 0;
        // 
        // dataGridViewResults
        // 
        dataGridViewResults.BackgroundColor = System.Drawing.Color.Silver;
        dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewResults.Dock = System.Windows.Forms.DockStyle.Bottom;
        dataGridViewResults.Location = new System.Drawing.Point(0, 476);
        dataGridViewResults.Margin = new System.Windows.Forms.Padding(4);
        dataGridViewResults.Name = "dataGridViewResults";
        dataGridViewResults.ReadOnly = true;
        dataGridViewResults.RowHeadersWidth = 51;
        dataGridViewResults.Size = new System.Drawing.Size(1755, 428);
        dataGridViewResults.TabIndex = 1;
        dataGridViewResults.Text = "dataGridView1";
        // 
        // flowLayoutPanelTemplates
        // 
        flowLayoutPanelTemplates.Controls.Add(btnExecuteQuery);
        flowLayoutPanelTemplates.Controls.Add(btnExportToCsv);
        flowLayoutPanelTemplates.Controls.Add(button6);
        flowLayoutPanelTemplates.Controls.Add(button5);
        flowLayoutPanelTemplates.Controls.Add(button4);
        flowLayoutPanelTemplates.Controls.Add(button3);
        flowLayoutPanelTemplates.Dock = System.Windows.Forms.DockStyle.Right;
        flowLayoutPanelTemplates.Location = new System.Drawing.Point(1232, 0);
        flowLayoutPanelTemplates.Margin = new System.Windows.Forms.Padding(4);
        flowLayoutPanelTemplates.Name = "flowLayoutPanelTemplates";
        flowLayoutPanelTemplates.Size = new System.Drawing.Size(523, 476);
        flowLayoutPanelTemplates.TabIndex = 2;
        // 
        // btnExecuteQuery
        // 
        btnExecuteQuery.BackColor = System.Drawing.Color.RosyBrown;
        btnExecuteQuery.Dock = System.Windows.Forms.DockStyle.Top;
        btnExecuteQuery.Location = new System.Drawing.Point(4, 4);
        btnExecuteQuery.Margin = new System.Windows.Forms.Padding(4);
        btnExecuteQuery.Name = "btnExecuteQuery";
        btnExecuteQuery.Size = new System.Drawing.Size(233, 106);
        btnExecuteQuery.TabIndex = 0;
        btnExecuteQuery.Text = "Запустить";
        btnExecuteQuery.UseVisualStyleBackColor = false;
        btnExecuteQuery.Click += BtnExecuteQuery_Click;
        // 
        // btnExportToCsv
        // 
        btnExportToCsv.BackColor = System.Drawing.Color.YellowGreen;
        btnExportToCsv.Dock = System.Windows.Forms.DockStyle.Top;
        btnExportToCsv.Location = new System.Drawing.Point(245, 4);
        btnExportToCsv.Margin = new System.Windows.Forms.Padding(4);
        btnExportToCsv.Name = "btnExportToCsv";
        btnExportToCsv.Size = new System.Drawing.Size(233, 106);
        btnExportToCsv.TabIndex = 1;
        btnExportToCsv.Text = "Сохранить в файл";
        btnExportToCsv.UseVisualStyleBackColor = false;
        btnExportToCsv.Click += BtnExportToCsv_Click;
        // 
        // button6
        // 
        button6.BackColor = System.Drawing.Color.Silver;
        button6.Dock = System.Windows.Forms.DockStyle.Top;
        button6.Location = new System.Drawing.Point(4, 118);
        button6.Margin = new System.Windows.Forms.Padding(4);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(233, 106);
        button6.TabIndex = 7;
        button6.Text = "Записи сотрудников";
        button6.UseVisualStyleBackColor = false;
        button6.Click += button6_Click;
        // 
        // button5
        // 
        button5.BackColor = System.Drawing.Color.Silver;
        button5.Dock = System.Windows.Forms.DockStyle.Top;
        button5.Location = new System.Drawing.Point(245, 118);
        button5.Margin = new System.Windows.Forms.Padding(4);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(233, 106);
        button5.TabIndex = 6;
        button5.Text = "Список заявок";
        button5.UseVisualStyleBackColor = false;
        button5.Click += button5_Click;
        // 
        // button4
        // 
        button4.BackColor = System.Drawing.Color.Silver;
        button4.Dock = System.Windows.Forms.DockStyle.Top;
        button4.Location = new System.Drawing.Point(4, 232);
        button4.Margin = new System.Windows.Forms.Padding(4);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(233, 106);
        button4.TabIndex = 5;
        button4.Text = "Список поставщиков и их поставок";
        button4.UseVisualStyleBackColor = false;
        button4.Click += button4_Click;
        // 
        // button3
        // 
        button3.BackColor = System.Drawing.Color.Silver;
        button3.Dock = System.Windows.Forms.DockStyle.Top;
        button3.Location = new System.Drawing.Point(245, 232);
        button3.Margin = new System.Windows.Forms.Padding(4);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(233, 106);
        button3.TabIndex = 4;
        button3.Text = "Список деталей";
        button3.UseVisualStyleBackColor = false;
        button3.Click += button3_Click;
        // 
        // panel1
        // 
        panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        panel1.BackColor = System.Drawing.Color.Transparent;
        panel1.Controls.Add(textBoxQuery);
        panel1.Controls.Add(flowLayoutPanelTemplates);
        panel1.Controls.Add(dataGridViewResults);
        panel1.Location = new System.Drawing.Point(162, 85);
        panel1.Margin = new System.Windows.Forms.Padding(4);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(1755, 904);
        panel1.TabIndex = 3;
        // 
        // DocumentForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoSize = true;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        ClientSize = new System.Drawing.Size(1991, 1157);
        Controls.Add(panel1);
        Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        Margin = new System.Windows.Forms.Padding(4);
        Text = "Документы";
        ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
        flowLayoutPanelTemplates.ResumeLayout(false);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button6;

    private System.Windows.Forms.Button button3;

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.Button btnExportToCsv;

    private System.Windows.Forms.Button btnExecuteQuery;

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTemplates;

    private System.Windows.Forms.DataGridView dataGridViewResults;

    private System.Windows.Forms.TextBox textBoxQuery;

    #endregion
}