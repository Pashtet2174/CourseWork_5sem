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
        _dataGridView = new System.Windows.Forms.DataGridView();
        _crudPanel = new System.Windows.Forms.Panel();
        _deleteButton = new System.Windows.Forms.Button();
        _editButton = new System.Windows.Forms.Button();
        _addButton = new System.Windows.Forms.Button();
        _refreshButton = new System.Windows.Forms.Button();
        _searchButton = new System.Windows.Forms.Button();
        _searchTextBox = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        _crudPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _dataGridView
        // 
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
        _dataGridView.Location = new System.Drawing.Point(0, 48);
        _dataGridView.Name = "_dataGridView";
        _dataGridView.ReadOnly = true;
        _dataGridView.RowHeadersWidth = 51;
        _dataGridView.Size = new System.Drawing.Size(800, 402);
        _dataGridView.TabIndex = 0;
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
        _crudPanel.Size = new System.Drawing.Size(800, 40);
        _crudPanel.TabIndex = 1;
        // 
        // _deleteButton
        // 
        _deleteButton.Dock = System.Windows.Forms.DockStyle.Left;
        _deleteButton.Location = new System.Drawing.Point(333, 0);
        _deleteButton.Margin = new System.Windows.Forms.Padding(5);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.Size = new System.Drawing.Size(102, 30);
        _deleteButton.TabIndex = 3;
        _deleteButton.Text = "❌ Удалить";
        _deleteButton.UseVisualStyleBackColor = true;
        _deleteButton.Click += DeleteRecord_Click;
        _deleteButton.AutoSize = true;
        // 
        // _editButton
        // 
        _editButton.Dock = System.Windows.Forms.DockStyle.Left;
        _editButton.Location = new System.Drawing.Point(222, 0);
        _editButton.Margin = new System.Windows.Forms.Padding(5);
        _editButton.Name = "_editButton";
        _editButton.Size = new System.Drawing.Size(111, 30);
        _editButton.TabIndex = 2;
        _editButton.Text = "✏️ Изменить";
        _editButton.UseVisualStyleBackColor = true;
        _editButton.Click += EditRecord_Click;
        _editButton.AutoSize = true;
        // 
        // _addButton
        // 
        _addButton.Dock = System.Windows.Forms.DockStyle.Left;
        _addButton.Location = new System.Drawing.Point(111, 0);
        _addButton.Margin = new System.Windows.Forms.Padding(5);
        _addButton.Name = "_addButton";
        _addButton.Size = new System.Drawing.Size(111, 30);
        _addButton.TabIndex = 1;
        _addButton.Text = "➕ Добавить";
        _addButton.UseVisualStyleBackColor = true;
        _addButton.Click += AddRecord_Click;
        _addButton.AutoSize = true;
        // 
        // _refreshButton
        // 
        _refreshButton.Dock = System.Windows.Forms.DockStyle.Left;
        _refreshButton.Location = new System.Drawing.Point(0, 0);
        _refreshButton.Margin = new System.Windows.Forms.Padding(5);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.Size = new System.Drawing.Size(111, 30);
        _refreshButton.TabIndex = 0;
        _refreshButton.Text = "🔄 Обновить";
        _refreshButton.UseVisualStyleBackColor = true;
        _refreshButton.Click += RefreshData;
        _refreshButton.AutoSize = true;
        // 
        // _searchButton
        // 
        _searchButton.Dock = System.Windows.Forms.DockStyle.Right;
        _searchButton.Location = new System.Drawing.Point(706, 0);
        _searchButton.Margin = new System.Windows.Forms.Padding(5);
        _searchButton.Name = "_searchButton";
        _searchButton.Size = new System.Drawing.Size(94, 30);
        _searchButton.TabIndex = 4;
        _searchButton.Text = "🔍 Найти";
        _searchButton.UseVisualStyleBackColor = true;
        _searchButton.Click += Search_Click;
        _searchButton.AutoSize = true;
        // 
        // textBox1
        // 
        _searchTextBox.Dock = System.Windows.Forms.DockStyle.Right;
        _searchTextBox.Location = new System.Drawing.Point(506, 0);
        _searchTextBox.Margin = new System.Windows.Forms.Padding(5);
        _searchTextBox.Name = "_searchTextBox";
        _searchTextBox.PlaceholderText = "Поиск...";
        _searchTextBox.Size = new System.Drawing.Size(200, 27);
        _searchTextBox.TabIndex = 5;
        // 
        // TableBrowserForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(_crudPanel);
        Controls.Add(_dataGridView);
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