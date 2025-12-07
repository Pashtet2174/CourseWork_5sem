using System;
using System.Collections.Generic;
using System.Data;
using System.Linq; // Добавлено для string.Join
using System.Windows.Forms;
using System.Globalization; // Добавлено для CultureInfo

namespace CourseWork_5sem
{
    public partial class RecordEditForm : Form
    {
        private readonly string _tableName;
        private readonly int _recordId; // -1 для INSERT, > 0 для UPDATE
        private DataRow _originalData;  // null для INSERT
        private List<ColumnMetadata> _metadataList;
            
        // Словарь для хранения ссылок на поля ввода по имени колонки
        private readonly Dictionary<string, Control> _inputControls = new Dictionary<string, Control>();

        // -------------------------------------------------------------------------
        // 1. СУЩЕСТВУЮЩИЙ КОНСТРУКТОР ДЛЯ РЕДАКТИРОВАНИЯ (UPDATE)
        // -------------------------------------------------------------------------
        /// <summary>
        /// Конструктор для формы редактирования.
        /// </summary>
        /// <param name="tableName">Имя таблицы (используется для SQL UPDATE).</param>
        /// <param name="recordId">ID редактируемой записи (используется в WHERE).</param>
        /// <param name="originalData">Исходные данные записи с подставленными FK (для отображения).</param>
        public RecordEditForm(string tableName, int recordId, DataRow originalData)
        {
            _tableName = tableName;
            _recordId = recordId;
            _originalData = originalData;
            
            // Вызов вспомогательного метода инициализации
            if (!InitMetadata()) return; 
            
            InitializeComponent();
            this.Text = $"Редактирование записи в таблице: {_tableName} (ID: {_recordId})";

            FontManager.FontSizeChanged += FontManager_FontSizeChanged;
            ApplyNewFontSize(FontManager.CurrentFontSize); 
            // Генерация UI
            InitializeDynamicControls();
                
            // Заполнение данных
            LoadOriginalData();
        }

        // -------------------------------------------------------------------------
        // 2. ДОБАВЛЕННЫЙ КОНСТРУКТОР ДЛЯ ДОБАВЛЕНИЯ (INSERT)
        // -------------------------------------------------------------------------
        /// <summary>
        /// Конструктор для формы добавления новой записи.
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        public RecordEditForm(string tableName)
        {
            _tableName = tableName;
            _recordId = -1; // Флаг: -1 означает "Новая запись"
            _originalData = null; // Данных для загрузки нет

            if (!InitMetadata()) return;
            InitializeComponent();

            this.Text = $"Добавление новой записи в таблицу: {_tableName}";
            InitializeDynamicControls();
            // LoadOriginalData не вызывается, поля остаются со значениями по умолчанию
        }

        // Вспомогательный метод для инициализации метаданных
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
            // Используем _metadataList вместо старого _displayColumns
            foreach (var metadata in _metadataList)
            {
                // Пропускаем ID
                if (metadata.SourceColumn == "id") continue;

                // Создаем Label
                Label label = new Label
                {
                    Text = metadata.DisplayName + ":",
                    AutoSize = true,
                    Margin = new Padding(10, 10, 0, 0)
                };
                _flowLayoutPanel.Controls.Add(label);

                // Создаем соответствующий контрол, передавая метаданные
                Control inputControl = CreateControlForColumn(metadata);

                // Ключ - это DisplayName (имя, которое есть в DataRow)
                _inputControls.Add(metadata.DisplayName, inputControl);
                _flowLayoutPanel.Controls.Add(inputControl);
            }
        }

        private Control CreateControlForColumn(ColumnMetadata metadata)
        {
            Control control;
        
            if (metadata.IsForeignKey)
            {
                // 1. Внешний ключ (FK): ComboBox с ReferenceItem
                List<ReferenceItem> items = DatabaseHelper.LoadReferenceTable(metadata.ReferenceTable);

                ComboBox cb = new ComboBox
                {
                    Width = 200,
                    Tag = metadata.SourceColumn, // 'position_id' (колонка для UPDATE)
                    DataSource = items,
                    DisplayMember = "Name",      
                    ValueMember = "Id",          
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    // ДОБАВЛЕНИЕ: Значение по умолчанию — нет выбора
                    SelectedIndex = -1 
                };
                control = cb;
            }
            else // НЕ FK: Используем тип данных
            {
                switch (metadata.Type)
                {
                    case ColumnType.Enum: 
                        // 2. ENUM: ComboBox со строками (без ID)
                        List<string> enumValues = DatabaseHelper.GetEnumValues(metadata.EnumTypeName);
                    
                        ComboBox cbEnum = new ComboBox
                        {
                            Width = 200,
                            Tag = metadata.SourceColumn, // 'status' (колонка для UPDATE)
                            DataSource = enumValues,     
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            // ДОБАВЛЕНИЕ: Значение по умолчанию — нет выбора
                            SelectedIndex = -1 
                        };
                        control = cbEnum;
                        break;
                    
                    case ColumnType.Date: 
                        // 3. Дата
                        DateTimePicker dtp = new DateTimePicker { 
                            Width = 200,
                            Format = DateTimePickerFormat.Short,
                            // ДОБАВЛЕНИЕ: Значение по умолчанию — сегодня
                            Value = DateTime.Now
                        };
                        control = dtp;
                        break;

                    case ColumnType.Numeric:
                    case ColumnType.Text:
                    default:
                        // 4. Текст/Числа
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
            // КОРРЕКЦИЯ: Добавлена защита от режима добавления
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
                        // Для FK и ENUM: ищем значение по отображаемому тексту
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
            
            // ДОБАВЛЕНИЕ: Списки для формирования SQL-запроса INSERT
            List<string> columnsForInsert = new List<string>(); 
            List<string> valuesForInsert = new List<string>();  
            
            // Использование setStatements для UPDATE
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
                // =========================================================
                // 1. ПОЛНАЯ ВАЛИДАЦИЯ ДАННЫХ
                // =========================================================

                bool isInputEmpty = string.IsNullOrWhiteSpace(GetControlValueString(control));

                // Проверка ComboBox (FK/ENUM):
                if (control is ComboBox) 
                {
                    // Используем явное приведение типов, чтобы избежать объявления новой переменной
                    ComboBox cbControl = (ComboBox)control; 
    
                    // Для ComboBox "пусто" означает, что ничего не выбрано (-1) или значение null.
                    if (cbControl.SelectedValue == null || cbControl.SelectedIndex == -1)
                    {
                        isInputEmpty = true;
                    }
                }

                // 1.1. Строгая проверка NOT NULL для ВСЕХ полей, кроме ID
                if (isInputEmpty)
                {
                    MessageBox.Show($"Поле '{metadata.DisplayName}' обязательно для заполнения!", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return;
                }

                // 1.2. Проверка типов и длины (для TextBox)
                if (!ValidateInputControl(control, metadata))
                {
                    control.Focus();
                    return;
                }
                object valueToSave = null;

                // --- 1. ЛОГИКА ПОЛУЧЕНИЯ ЗНАЧЕНИЯ (ОСТАЕТСЯ ПРЕЖНЕЙ) ---
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
                
                // --- 2. ОБРАБОТКА NULL и ВАЛИДАЦИЯ ---
                if (valueToSave == null || (valueToSave is string s && string.IsNullOrWhiteSpace(s)))
                {
                    // ДОБАВЛЕНИЕ: Строгая проверка для режима добавления (INSERT)
                    if (_recordId == -1 && (metadata.IsForeignKey || metadata.Type == ColumnType.Enum))
                    {
                         MessageBox.Show($"Поле '{metadata.DisplayName}' обязательно для заполнения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                    }
                    valueToSave = DBNull.Value;
                }

                // --- 3. ФОРМИРОВАНИЕ SQL И ПАРАМЕТРОВ ---
                string paramName = $"@p{paramCounter++}";
                
                // Формируем плейсхолдер с приведением типа для ENUM
                string valuePlaceholder = (metadata.Type == ColumnType.Enum) 
                    ? $"{paramName}::{metadata.EnumTypeName}" 
                    : paramName;
                
                // ДОБАВЛЕНИЕ: Накапливаем данные для INSERT и UPDATE
                columnsForInsert.Add($"\"{dbColumnName}\"");
                valuesForInsert.Add(valuePlaceholder);
                setsForUpdate.Add($"\"{dbColumnName}\" = {valuePlaceholder}");
                
                // Параметры
                parameters.Add(paramName, valueToSave); 
            }
            
            // 4. Формирование и выполнение запроса (ОСНОВНОЕ ИЗМЕНЕНИЕ ЛОГИКИ)
            string sql;
            
            if (_recordId == -1)
            {
                // === РЕЖИМ ВСТАВКИ (INSERT) ===
                if (columnsForInsert.Count == 0) return;

                sql = $"INSERT INTO \"{_tableName}\" ({string.Join(", ", columnsForInsert)}) " +
                      $"VALUES ({string.Join(", ", valuesForInsert)})";
            }
            else
            {
                // === РЕЖИМ ОБНОВЛЕНИЯ (UPDATE) ===
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
        
        // Вспомогательный метод для получения значения контрола (ОСТАЕТСЯ ПРЕЖНИМ)
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
        
        /// <summary>
        /// Получает строковое значение контрола для проверки на пустоту.
        /// </summary>
        private string GetControlValueString(Control control)
        {
            if (control is TextBox tb) 
            {
                return tb.Text;
            }
            if (control is ComboBox cb) 
            {
                // Для ComboBox возвращаем текст, если что-то выбрано, иначе - пустую строку
                return (cb.SelectedItem != null && cb.SelectedIndex != -1) ? cb.Text : "";
            }
            // DateTimePicker всегда имеет значение, возвращаем "ValueExists"
            if (control is DateTimePicker)
            {
                return "ValueExists"; 
            }
            return "";
        }

        /// <summary>
        /// Проверяет введенные данные на соответствие ожидаемому типу и ограничениям (длина строки, числовой тип).
        /// </summary>
        private bool ValidateInputControl(Control control, ColumnMetadata metadata)
        {
            // Устанавливаем фиксированное ограничение длины
            const int MaxLength = 30; // <--- Фиксированное ограничение

            // Валидация для текстовых полей и полей-чисел
            if (control is TextBox tb)
            {
                string text = tb.Text.Trim();
        
                // Если поле пустое, и оно не обязательно, пропускаем проверку типа/длины
                if (string.IsNullOrWhiteSpace(text)) return true; 

                // 1. Проверка на числовой тип (ColumnType.Numeric)
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
        
                // 2. Проверка длины строки (для ColumnType.Text и других)
                // Проверяем, если длина превышает установленный лимит
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