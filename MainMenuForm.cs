using System.Diagnostics; 
using System.IO;
namespace CourseWork_5sem;

public partial class MainMenuForm : Form
{
    private System.Windows.Forms.MenuStrip _mainMenu;
    private Panel _contentPanel;
    public MainMenuForm()   
    {
        InitializeComponent();
        _contentPanel = new Panel();
        _contentPanel.Dock = DockStyle.Fill;
        _contentPanel.BackColor = Color.Transparent;
        this.Controls.Add(_contentPanel);
        this.Load += MainMenuForm_Load;
        FontManager.FontSizeChanged += FontManager_FontSizeChanged;
        ApplyNewFontSize(FontManager.CurrentFontSize); 
    }

    private void MainMenuForm_Load(object sender, EventArgs e)
    {
        // Получаем роль из вашего DatabaseHelper
            string role = DatabaseHelper.CurrentUserRole;
            this.Text = $"Главное меню - Авторизован как: {role}";
        
        // Инициализация динамического меню
        ConfigureMenuAccess(role);
        
    }

    private void ConfigureMenuAccess(string role)
    {
        // 1. Создаем MenuStrip
        _mainMenu = new MenuStrip();
        this.Controls.Add(_mainMenu);
        this.MainMenuStrip = _mainMenu;

        // 2. Получаем список всех доступных таблиц из БД
        List<string> accessibleTables = DatabaseHelper.GetAllAccessibleTables();
    
        // 3. Создаем динамические пункты, разделяя таблицы на Справочники и Мои таблицы
        CreateDynamicMenu(accessibleTables); // <-- Вызываем новый метод вместо старых
    
        // 4. Добавляем статичные пункты
    
        CreateSimpleMenuItem("Документы", Documents_Click);
        CreateMiscMenu();
        CreateHelpMenu();
        CreateSimpleMenuItem("Аналитика", AnalyticsMenu_Click);
    }

    #region Menu Creation Methods
    
    // --- 1. Справочники (Статичный список, доступен всем) ---
    /// <summary>
    /// Динамически строит пункты "Справочники" и "Мои таблицы", 
    /// используя список из БД и классификацию из AccessControl.
    /// </summary>
    private void CreateDynamicMenu(List<string> accessibleTables)
    {
        var refMenu = CreateMenuItem("Справочники");
        var myTablesMenu = CreateMenuItem("Мои таблицы");

        foreach (var tableName in accessibleTables.OrderBy(t => t))
        {
            if (!AccessControl.TableDisplayNames.TryGetValue(tableName, out string displayName))
            {
                continue; 
            }

            if (AccessControl.ReferenceTables.Contains(tableName))
            {
                // ИСПОЛЬЗУЕМ ЕДИНЫЙ ОБРАБОТЧИК
                AddSubItem(refMenu, tableName, displayName, OpenTable_Click);
            }
            else
            {
                // ИСПОЛЬЗУЕМ ЕДИНЫЙ ОБРАБОТЧИК
                AddSubItem(myTablesMenu, tableName, displayName, OpenTable_Click);
            }
        }
    
        if (refMenu.HasDropDownItems)
        {
            _mainMenu.Items.Add(refMenu);
        }

        if (myTablesMenu.HasDropDownItems)
        {
            _mainMenu.Items.Add(myTablesMenu);
        }
    }

    // --- 3. Разное (Настройки, Сменить пароль) ---
    private void CreateMiscMenu()
    {
        var miscMenu = CreateMenuItem("Разное");
        miscMenu.DropDownItems.Add(new ToolStripMenuItem("Настройки", null, MiscSettings_Click));
        miscMenu.DropDownItems.Add(new ToolStripMenuItem("Сменить пароль", null, MiscChangePassword_Click));
        _mainMenu.Items.Add(miscMenu);
    }

    // --- 4. Справка (Руководство, О программе) ---
    private void CreateHelpMenu()
    {
        var helpMenu = CreateMenuItem("Справка");
        helpMenu.DropDownItems.Add(new ToolStripMenuItem("Руководство пользователя", null, HelpManual_Click));
        helpMenu.DropDownItems.Add(new ToolStripMenuItem("О программе", null, HelpAbout_Click));
        _mainMenu.Items.Add(helpMenu);
    }
    
    #endregion

    #region Helpers
    
    // Вспомогательный метод для создания пункта меню верхнего уровня
    private ToolStripMenuItem CreateMenuItem(string text)
    {
        return new ToolStripMenuItem(text);
    }
    
    // Вспомогательный метод для создания пункта меню верхнего уровня с обработчиком
    private void CreateSimpleMenuItem(string text, EventHandler handler)
    {
        var item = CreateMenuItem(text);
        item.Click += handler;
        _mainMenu.Items.Add(item);
    }

    // Вспомогательный метод для добавления подпунктов
    private void AddSubItem(ToolStripMenuItem parent, string dbTableName, string text, EventHandler handler)
    {
        var item = new ToolStripMenuItem(text);
        item.Tag = dbTableName; // Храним имя таблицы в Tag
        item.Click += handler;
        parent.DropDownItems.Add(item);
    }
    
    #endregion

    #region Event Handlers (Заглушки)

    // Обработчик для пункта "Документы"
    private void Documents_Click(object sender, EventArgs e)
    {
        // 1. Очистка старого контента
        _contentPanel.Controls.Clear();

        // 2. Создание и отображение новой формы документов
        try
        {
            // Создаем новую форму
            var docForm = new DocumentForm();
        
            // Встраиваем ее в панель
            _contentPanel.Controls.Add(docForm);
        
            // Отображаем форму, теперь она заполнит панель
            docForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть модуль Документы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Обработчик для "Мои таблицы"
    private void OpenTable_Click(object sender, EventArgs e)
    {
        var item = sender as ToolStripMenuItem;
        string tableName = item?.Tag?.ToString();

        if (string.IsNullOrEmpty(tableName))
        {
            MessageBox.Show("Не удалось определить имя таблицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // 1. Очистка старого контента
        _contentPanel.Controls.Clear();

        // 2. Создание и отображение новой формы просмотра таблицы
        try
        {
            var tableForm = new TableBrowserForm(tableName);
            _contentPanel.Controls.Add(tableForm);
            tableForm.Show();
        }
        catch (Exception ex)
        {
            // Обработка ошибок, которые могут возникнуть в TableBrowserForm (например, ошибки подключения)
            MessageBox.Show($"Не удалось открыть таблицу '{item.Text}': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    // Обработчики для "Разное"
    private void MiscSettings_Click(object sender, EventArgs e)
    {
        _contentPanel.Controls.Clear();

        // 2. Создание и отображение формы настроек
        try
        {
            var settingsForm = new SettingsForm();
        
            // Встраиваем ее в панель
            _contentPanel.Controls.Add(settingsForm);
        
            // Отображаем форму
            settingsForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть модуль 'Настройки': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void MiscChangePassword_Click(object sender, EventArgs e)
    {
        using (var changeForm = new ChangePasswordForm())
        {
            // Открываем как диалоговое окно (модально)
            changeForm.ShowDialog();
        
            // Если форма вернула DialogResult.OK, это означает успешную смену пароля.
            // Вы можете добавить дополнительную логику, если нужно.
        }
    }
    
    // Обработчики для "Справка"
    private void HelpManual_Click(object sender, EventArgs e)
    {
        string fileName = "form.html";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        // 2. Проверяем, существует ли файл физически
        if (File.Exists(path))
        {
            try
            {
                // 3. Запускаем браузер по умолчанию
                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true 
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии браузера: {ex.Message}");
            }
        }
        else
        {
            // Если забыл положить файл в папку bin/Debug
            MessageBox.Show($"Файл не найден по пути: {path}\n\n" +
                            "Убедитесь, что в свойствах файла 'Copy to Output Directory' установлено 'Copy always'.");
        }
    }
    
    private void HelpAbout_Click(object sender, EventArgs e)
    {
        _contentPanel.Controls.Clear();

        // 2. Создание и отображение новой формы "О программе"
        try
        {
            var aboutForm = new AboutForm();
        
            // Встраиваем ее в панель
            _contentPanel.Controls.Add(aboutForm);
        
            // Отображаем форму, теперь она заполнит панель
            aboutForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть модуль 'О программе': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void MainMenuForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        // Важно закрыть соединение при выходе
        DatabaseHelper.CloseConnection();
        //Application.Exit();
    }
    
    #endregion

    private void button1_Click(object sender, EventArgs e)
    {
        // 1. Оповещаем пользователя о выходе (опционально)
        DialogResult result = MessageBox.Show(
            "Вы действительно хотите выйти из профиля?", 
            "Подтверждение выхода", 
            MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            // 1. Очистка и закрытие соединения
            DatabaseHelper.CurrentUserRole = null; 
            DatabaseHelper.CloseConnection(); 

            // 2. Создаем и показываем форму входа
            this.DialogResult = DialogResult.OK;
            // !!! КЛЮЧЕВОЕ ИЗМЕНЕНИЕ: Используем DialogResult и this.Close() !!!
            // Установка DialogResult сигнализирует Windows Forms, что форма закрывается успешно.
            
        }
    }
    
    
    public void ShowMenuStrip()
    {
        _mainMenu.Visible = true;
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
    
    private void AnalyticsMenu_Click(object sender, EventArgs e)
    {
        // 1. Очистка старого контента
        _contentPanel.Controls.Clear();

        // 2. Создание и отображение формы аналитики
        try
        {
            var analyticsForm = new AnalyticsForm();
        
            // Встраиваем ее в панель
            _contentPanel.Controls.Add(analyticsForm);
        
            // Отображаем форму
            analyticsForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть модуль 'Аналитика': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    
}