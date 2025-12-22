using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq; 
using System.Windows.Forms; 

namespace CourseWork_5sem
{
    public partial class RecordEditForm : Form
    {
        private readonly string _tableName;
        private readonly int _recordId; 
        private DataRow _originalData;  
        private List<ColumnMetadata> _metadataList;
            
        private readonly Dictionary<string, Control> _inputControls = new Dictionary<string, Control>();
        
        public RecordEditForm(string tableName, int recordId, DataRow originalData)
        {
            _tableName = tableName;
            _recordId = recordId;
            _originalData = originalData;
            
            if (!InitMetadata()) return; 
            
            InitializeComponent();
            this.Text = $"Редактирование записи в таблице: {_tableName} (ID: {_recordId})";

            FontManager.FontSizeChanged += FontManager_FontSizeChanged;
            ApplyNewFontSize(FontManager.CurrentFontSize); 
            InitializeDynamicControls();
            LoadOriginalData();
        }

        
        public RecordEditForm(string tableName)
        {
            _tableName = tableName;
            _recordId = -1; 
            _originalData = null; 

            if (!InitMetadata()) return;
            InitializeComponent();

            this.Text = $"Добавление новой записи в таблицу: {_tableName}";
            InitializeDynamicControls();
        }

        private bool InitMetadata()
        {
            if (!EditMetadata.TableEditDefinitions.TryGetValue(_tableName, out _metadataList))
            {
                MessageBox.Show($"Отсутствуют метаданные для редактирования таблицы '{_tableName}'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
                return false;
            }    
            return true;
        }

        private void InitializeDynamicControls()
        {
            foreach (var metadata in _metadataList)
            {
                if (metadata.SourceColumn == "id") continue;

                Label label = new Label
                {
                    Text = metadata.DisplayName + ":",
                    AutoSize = true,
                    Margin = new Padding(10, 10, 0, 0)
                };
                _flowLayoutPanel.Controls.Add(label);

                Control inputControl = CreateControlForColumn(metadata);

                _inputControls.Add(metadata.DisplayName, inputControl);
                _flowLayoutPanel.Controls.Add(inputControl);
            }
        }

        private Control CreateControlForColumn(ColumnMetadata metadata)
        {
            Control control;
        
            if (metadata.IsForeignKey)
            {
                List<ReferenceItem> items = DatabaseHelper.LoadReferenceTable(metadata.ReferenceTable);

                ComboBox cb = new ComboBox
                {
                    Width = 200,
                    Tag = metadata.SourceColumn,
                    DataSource = items,
                    DisplayMember = "Name",      
                    ValueMember = "Id",          
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    SelectedIndex = -1 
                };
                control = cb;
            }
            else 
            {
                switch (metadata.Type)
                {
                    case ColumnType.Enum: 
                        List<string> enumValues = DatabaseHelper.GetEnumValues(metadata.EnumTypeName);
                    
                        ComboBox cbEnum = new ComboBox
                        {
                            Width = 200,
                            Tag = metadata.SourceColumn,
                            DataSource = enumValues,     
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            SelectedIndex = -1 
                        };
                        control = cbEnum;
                        break;
                    
                    case ColumnType.Date: 
                        DateTimePicker dtp = new DateTimePicker { 
                            Width = 200,
                            Format = DateTimePickerFormat.Short,
                            Value = DateTime.Now
                        };
                        control = dtp;
                        break;

                    case ColumnType.Numeric:
                    case ColumnType.Text:
                    default:
                        TextBox tb = new TextBox { Width = 200 };
                        control = tb;
                        break;
                }
            }
        
            control.Margin = new Padding(10, 10, 10, 0);
            return control;
        }

        private void LoadOriginalData()
        {
            if (_originalData == null) return;
            
            foreach (var pair in _inputControls)
            {
                string colName = pair.Key;
                Control control = pair.Value;
            
                if (_originalData.Table.Columns.Contains(colName) && _originalData[colName] != DBNull.Value)
                {
                    string value = _originalData[colName].ToString();

                    if (control is ComboBox comboBox)
                    {
                        comboBox.Text = value; 
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.Text = value;
                    }
                    else if (control is DateTimePicker datePicker)
                    {
                        if (DateTime.TryParse(value, out DateTime date))
                        {
                            datePicker.Value = date;
                        }
                    }
                }
            }
        }

        private void SaveData_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>();
            
            List<string> columnsForInsert = new List<string>(); 
            List<string> valuesForInsert = new List<string>();  
            
            List<string> setsForUpdate = new List<string>(); 

            int paramCounter = 0;
            
            foreach (var metadata in _metadataList)
            {
                string displayColumnName = metadata.DisplayName;
                string dbColumnName = metadata.SourceColumn;
                
                if (dbColumnName == "id") continue; 

                if (!_inputControls.TryGetValue(displayColumnName, out Control control))
                {
                    continue; 
                }

                bool isInputEmpty = string.IsNullOrWhiteSpace(GetControlValueString(control));

                
                if (control is ComboBox) 
                {
                    ComboBox cbControl = (ComboBox)control; 
    
                    if (cbControl.SelectedValue == null || cbControl.SelectedIndex == -1)
                    {
                        isInputEmpty = true;
                    }
                }

                if (isInputEmpty)
                {
                    MessageBox.Show($"Поле '{metadata.DisplayName}' обязательно для заполнения!", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return;
                }

                if (!ValidateInputControl(control, metadata))
                {
                    control.Focus();
                    return;
                }
                object valueToSave = null;

                if (control is ComboBox cb)
                {
                    if (metadata.IsForeignKey)
                    {
                        valueToSave = cb.SelectedValue; 
                    }
                    else if (metadata.Type == ColumnType.Enum)
                    {
                        valueToSave = cb.Text;
                    }
                }
                else
                {
                    valueToSave = GetControlValue(control, metadata.Type);
                }
                
                if (valueToSave == null || (valueToSave is string s && string.IsNullOrWhiteSpace(s)))
                {
                    if (_recordId == -1 && (metadata.IsForeignKey || metadata.Type == ColumnType.Enum))
                    {
                         MessageBox.Show($"Поле '{metadata.DisplayName}' обязательно для заполнения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                    }
                    valueToSave = DBNull.Value;
                }

                string paramName = $"@p{paramCounter++}";
                
                string valuePlaceholder = (metadata.Type == ColumnType.Enum) 
                    ? $"{paramName}::{metadata.EnumTypeName}" 
                    : paramName;
                
                columnsForInsert.Add($"\"{dbColumnName}\"");
                valuesForInsert.Add(valuePlaceholder);
                setsForUpdate.Add($"\"{dbColumnName}\" = {valuePlaceholder}");
                
                parameters.Add(paramName, valueToSave); 
            }
            
            string sql;
            
            if (_recordId == -1)
            {
                if (columnsForInsert.Count == 0) return;

                sql = $"INSERT INTO \"{_tableName}\" ({string.Join(", ", columnsForInsert)}) " +
                      $"VALUES ({string.Join(", ", valuesForInsert)})";
            }
            else
            {
                if (setsForUpdate.Count == 0) return;
                
                parameters.Add("@id", _recordId);
                sql = $"UPDATE \"{_tableName}\" SET {string.Join(", ", setsForUpdate)} WHERE id = @id";
            }

            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
                
                if (rowsAffected > 0)
                {
                    string action = (_recordId == -1) ? "добавлена" : "обновлена";
                    MessageBox.Show($"Запись успешно {action}.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Операция не выполнена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private object GetControlValue(Control control, ColumnType expectedType)
        {
            if (control is DateTimePicker dtp)
            {
                return dtp.Value.Date;
            }
        
            if (control is TextBox tb)
            {
                string text = tb.Text.Trim();
                if (string.IsNullOrWhiteSpace(text))
                {
                    return DBNull.Value; 
                }

                if (expectedType == ColumnType.Numeric)
                {
                    if (int.TryParse(text, out int intValue))
                    {
                        return intValue;
                    }
                    if (decimal.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal decimalValue))
                    {
                        return decimalValue;
                    }
                    return text; 
                }
                return text;
            }
            return null;
        }
        
        private string GetControlValueString(Control control)
        {
            if (control is TextBox tb) 
            {
                return tb.Text;
            }
            if (control is ComboBox cb) 
            {
                return (cb.SelectedItem != null && cb.SelectedIndex != -1) ? cb.Text : "";
            }
            if (control is DateTimePicker)
            {
                return "ValueExists"; 
            }
            return "";
        }

        private bool ValidateInputControl(Control control, ColumnMetadata metadata)
        {
            const int MaxLength = 30; 

            
            if (control is TextBox tb)
            {
                string text = tb.Text.Trim();
        
                
                if (string.IsNullOrWhiteSpace(text)) return true; 

                
                if (metadata.Type == ColumnType.Numeric)
                {
                    if (int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _) ||
                        decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                    {
                        return true; 
                    }
            
                    MessageBox.Show($"Поле '{metadata.DisplayName}' должно содержать числовое значение.", "Ошибка типа данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
        
                
                
                if (text.Length > MaxLength) 
                {
                    MessageBox.Show($"Длина поля '{metadata.DisplayName}' превышает максимально допустимую ({MaxLength} символов).", "Ошибка длины", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
    
            return true;
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