using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace CourseWork_5sem
{
    
    public partial class TableBrowserForm : Form
    {
        private readonly string _tableName;
        private DataTable _dataTable;

        
        
        
        
        public TableBrowserForm(string tableName)
        {
            _tableName = tableName;
            InitializeComponent();
            this.Load += TableBrowserForm_Load;
            FontManager.FontSizeChanged += FontManager_FontSizeChanged;
            ApplyNewFontSize(FontManager.CurrentFontSize); 
            
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill; 
        }
        
        private void TableBrowserForm_Load(object sender, EventArgs e)
        {
            
            LoadData(_tableName, null);
        }

        
        private void LoadData(string tableName, string searchTerm)
        {
            try
            {
                
                string displaySql = DatabaseHelper.GetDisplayQuery(tableName);
                
                
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    
                    var columns = DatabaseHelper.GetColumnNames(tableName);
                    
                    
                    string searchCondition = string.Join(" OR ", 
                        columns.Select(col => $"CAST({col} AS TEXT) ILIKE @searchTerm")); 
                        

                    displaySql = $"SELECT * FROM ({displaySql}) AS base_query WHERE {searchCondition}";
                }
                
                
                var parameters = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    parameters.Add("@searchTerm", $"%{searchTerm}%");
                }
                
                
                _dataTable = DatabaseHelper.ExecuteDataTable(displaySql, parameters);

                
                _dataGridView.DataSource = _dataTable;
                
                
                _dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        

        private void RefreshData(object sender, EventArgs e)
        {
            
            _searchTextBox.Clear();
            LoadData(_tableName, null);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            
            LoadData(_tableName, _searchTextBox.Text);
        }

        private void AddRecord_Click(object sender, EventArgs e)
        {
            
            using (var addForm = new RecordEditForm(_tableName))
            {
                
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    
                    RefreshData(null, null);
            
                    
                }
            }
        }

        private void EditRecord_Click(object sender, EventArgs e)
        {
            
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись для изменения.", "Изменение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            DataGridViewRow selectedRow = _dataGridView.SelectedRows[0];
            
            
            if (selectedRow.Cells[0].Value == DBNull.Value || selectedRow.Cells[0].Value == null)
            {
                MessageBox.Show("Невозможно получить ID выбранной записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                
                int recordId = Convert.ToInt32(selectedRow.Cells[0].Value);

                
                
                
                string displaySql = DatabaseHelper.GetDisplayQuery(_tableName);
                string selectSql = $"SELECT * FROM ({displaySql}) AS base_query WHERE id = @id";
                
                var parameters = new Dictionary<string, object>
                {
                    { "@id", recordId }
                };

                DataRow originalData = DatabaseHelper.ExecuteDataRow(selectSql, parameters);

                if (originalData == null)
                {
                    MessageBox.Show("Не удалось загрузить данные для редактирования.", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                
                
                
                
                
                using (var editForm = new RecordEditForm(_tableName, recordId, originalData))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        
                        RefreshData(null, null);
                    }
                }
                

            }
            catch (Npgsql.PostgresException ex)
            {
                
                MessageBox.Show($"Ошибка БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                
                MessageBox.Show("Ошибка: ID выбранной записи имеет неверный формат (не число).", "Ошибка типа данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Критическая ошибка при изменении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteRecord_Click(object sender, EventArgs e)
        {
            
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            
            DataGridViewRow selectedRow = _dataGridView.SelectedRows[0];
            
            
            
            if (selectedRow.Cells[0].Value == DBNull.Value || selectedRow.Cells[0].Value == null)
            {
                MessageBox.Show("Невозможно получить ID выбранной записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            object idValue = selectedRow.Cells[0].Value;
            
            int recordId = Convert.ToInt32(idValue);
            
            
            DialogResult result = MessageBox.Show(
                $"Вы действительно хотите удалить запись (ID: {recordId}) из таблицы '{_tableName}'?",
                "Подтверждение удаления", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    
                    
                    string deleteSql = $"DELETE FROM \"{_tableName}\" WHERE id = @id;";
                    
                    var parameters = new Dictionary<string, object>
                    {
                        { "@id", recordId }
                    };

                    
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(deleteSql, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        RefreshData(null, null); 
                    }
                    else if (rowsAffected == 0)
                    {
                        MessageBox.Show("Запись не найдена или не удалена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show($"Критическая ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
}