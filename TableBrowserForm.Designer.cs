using System.ComponentModel;

namespace CourseWork_5sem;

partial class TableBrowserForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableBrowserForm));
        _dataGridView = new System.Windows.Forms.DataGridView();
        _crudPanel = new System.Windows.Forms.Panel();
        _searchTextBox = new System.Windows.Forms.TextBox();
        _searchButton = new System.Windows.Forms.Button();
        _deleteButton = new System.Windows.Forms.Button();
        _editButton = new System.Windows.Forms.Button();
        _addButton = new System.Windows.Forms.Button();
        _refreshButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        _crudPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _dataGridView
        // 
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        _dataGridView.Location = new System.Drawing.Point(0, 40);
        _dataGridView.Name = "_dataGridView";
        _dataGridView.ReadOnly = true;
        _dataGridView.RowHeadersWidth = 51;
        _dataGridView.Size = new System.Drawing.Size(1482, 813);
        _dataGridView.TabIndex = 1;
        _dataGridView.Text = "dataGridView1";
        // 
        // _crudPanel
        // 
        _crudPanel.Controls.Add(_searchTextBox);
        _crudPanel.Controls.Add(_searchButton);
        _crudPanel.Controls.Add(_deleteButton);
        _crudPanel.Controls.Add(_editButton);
        _crudPanel.Controls.Add(_addButton);
        _crudPanel.Controls.Add(_refreshButton);
        _crudPanel.Dock = System.Windows.Forms.DockStyle.Top;
        _crudPanel.Location = new System.Drawing.Point(0, 0);
        _crudPanel.Name = "_crudPanel";
        _crudPanel.Size = new System.Drawing.Size(1482, 40);
        _crudPanel.TabIndex = 0;
        // 
        // _searchTextBox
        // 
        _searchTextBox.Dock = System.Windows.Forms.DockStyle.Right;
        _searchTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        _searchTextBox.Location = new System.Drawing.Point(1188, 0);
        _searchTextBox.Margin = new System.Windows.Forms.Padding(5);
        _searchTextBox.Name = "_searchTextBox";
        _searchTextBox.PlaceholderText = "Поиск...";
        _searchTextBox.Size = new System.Drawing.Size(200, 27);
        _searchTextBox.TabIndex = 5;
        // 
        // _searchButton
        // 
        _searchButton.AutoSize = true;
        _searchButton.Dock = System.Windows.Forms.DockStyle.Right;
        _searchButton.Location = new System.Drawing.Point(1388, 0);
        _searchButton.Margin = new System.Windows.Forms.Padding(5);
        _searchButton.Name = "_searchButton";
        _searchButton.Size = new System.Drawing.Size(94, 40);
        _searchButton.TabIndex = 4;
        _searchButton.Text = "🔍 Найти";
        _searchButton.UseVisualStyleBackColor = true;
        _searchButton.Click += Search_Click;
        // 
        // _deleteButton
        // 
        _deleteButton.AutoSize = true;
        _deleteButton.Dock = System.Windows.Forms.DockStyle.Left;
        _deleteButton.Location = new System.Drawing.Point(337, 0);
        _deleteButton.Margin = new System.Windows.Forms.Padding(5);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.Size = new System.Drawing.Size(102, 40);
        _deleteButton.TabIndex = 3;
        _deleteButton.Text = "❌ Удалить";
        _deleteButton.UseVisualStyleBackColor = true;
        _deleteButton.Click += DeleteRecord_Click;
        // 
        // _editButton
        // 
        _editButton.AutoSize = true;
        _editButton.Dock = System.Windows.Forms.DockStyle.Left;
        _editButton.Location = new System.Drawing.Point(224, 0);
        _editButton.Margin = new System.Windows.Forms.Padding(5);
        _editButton.Name = "_editButton";
        _editButton.Size = new System.Drawing.Size(113, 40);
        _editButton.TabIndex = 2;
        _editButton.Text = "✏️ Изменить";
        _editButton.UseVisualStyleBackColor = true;
        _editButton.Click += EditRecord_Click;
        // 
        // _addButton
        // 
        _addButton.AutoSize = true;
        _addButton.Dock = System.Windows.Forms.DockStyle.Left;
        _addButton.Location = new System.Drawing.Point(113, 0);
        _addButton.Margin = new System.Windows.Forms.Padding(5);
        _addButton.Name = "_addButton";
        _addButton.Size = new System.Drawing.Size(111, 40);
        _addButton.TabIndex = 1;
        _addButton.Text = "➕ Добавить";
        _addButton.UseVisualStyleBackColor = true;
        _addButton.Click += AddRecord_Click;
        // 
        // _refreshButton
        // 
        _refreshButton.AutoSize = true;
        _refreshButton.Dock = System.Windows.Forms.DockStyle.Left;
        _refreshButton.Location = new System.Drawing.Point(0, 0);
        _refreshButton.Margin = new System.Windows.Forms.Padding(5);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.Size = new System.Drawing.Size(113, 40);
        _refreshButton.TabIndex = 0;
        _refreshButton.Text = "🔄 Обновить";
        _refreshButton.UseVisualStyleBackColor = true;
        _refreshButton.Click += RefreshData;
        // 
        // TableBrowserForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        ClientSize = new System.Drawing.Size(1482, 853);
        Controls.Add(_dataGridView);
        Controls.Add(_crudPanel);
        Text = "TableBrowserForm";
        ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
        _crudPanel.ResumeLayout(false);
        _crudPanel.PerformLayout();
        ResumeLayout(false);
    }

    private System.Windows.Forms.TextBox _searchTextBox;

    private System.Windows.Forms.Button _searchButton;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Button _deleteButton;

    private System.Windows.Forms.Button _editButton;

    private System.Windows.Forms.Button _addButton;

    private System.Windows.Forms.Button _refreshButton;

    private System.Windows.Forms.Panel _crudPanel;

    private System.Windows.Forms.DataGridView _dataGridView;

    #endregion
}