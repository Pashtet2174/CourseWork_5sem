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
            string role = DatabaseHelper.CurrentUserRole;
            this.Text = $"Главное меню - Авторизован как: {role}";
        ConfigureMenuAccess(role);
        
    }

    private void ConfigureMenuAccess(string role)
    {
        _mainMenu = new MenuStrip();
        this.Controls.Add(_mainMenu);
        this.MainMenuStrip = _mainMenu;

        List<string> accessibleTables = DatabaseHelper.GetAllAccessibleTables();
    
        CreateDynamicMenu(accessibleTables);
    
        CreateSimpleMenuItem("Документы", Documents_Click);
        CreateMiscMenu();
        CreateHelpMenu();
        CreateSimpleMenuItem("Аналитика", AnalyticsMenu_Click);
    }

    
    
    
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
                AddSubItem(refMenu, tableName, displayName, OpenTable_Click);
            }
            else
            {
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

    private void CreateMiscMenu()
    {
        var miscMenu = CreateMenuItem("Разное");
        miscMenu.DropDownItems.Add(new ToolStripMenuItem("Настройки", null, MiscSettings_Click));
        miscMenu.DropDownItems.Add(new ToolStripMenuItem("Сменить пароль", null, MiscChangePassword_Click));
        _mainMenu.Items.Add(miscMenu);
    }

    private void CreateHelpMenu()
    {
        var helpMenu = CreateMenuItem("Справка");
        helpMenu.DropDownItems.Add(new ToolStripMenuItem("Руководство пользователя", null, HelpManual_Click));
        helpMenu.DropDownItems.Add(new ToolStripMenuItem("О программе", null, HelpAbout_Click));
        _mainMenu.Items.Add(helpMenu);
    }
    
    
    private ToolStripMenuItem CreateMenuItem(string text)
    {
        return new ToolStripMenuItem(text);
    }
    
    private void CreateSimpleMenuItem(string text, EventHandler handler)
    {
        var item = CreateMenuItem(text);
        item.Click += handler;
        _mainMenu.Items.Add(item);
    }

    private void AddSubItem(ToolStripMenuItem parent, string dbTableName, string text, EventHandler handler)
    {
        var item = new ToolStripMenuItem(text);
        item.Tag = dbTableName; 
        item.Click += handler;
        parent.DropDownItems.Add(item);
    }
    

    private void Documents_Click(object sender, EventArgs e)
    {
        _contentPanel.Controls.Clear();

        try
        {
            var docForm = new DocumentForm();
        
            _contentPanel.Controls.Add(docForm);
        
            docForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть модуль Документы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OpenTable_Click(object sender, EventArgs e)
    {
        var item = sender as ToolStripMenuItem;
        string tableName = item?.Tag?.ToString();

        if (string.IsNullOrEmpty(tableName))
        {
            MessageBox.Show("Не удалось определить имя таблицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        _contentPanel.Controls.Clear();

        try
        {
            var tableForm = new TableBrowserForm(tableName);
            _contentPanel.Controls.Add(tableForm);
            tableForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть таблицу '{item.Text}': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void MiscSettings_Click(object sender, EventArgs e)
    {
        _contentPanel.Controls.Clear();

        try
        {
            var settingsForm = new SettingsForm();
        
            _contentPanel.Controls.Add(settingsForm);
        
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
            changeForm.ShowDialog();
        
        }
    }
    
    private void HelpManual_Click(object sender, EventArgs e)
    {
        string fileName = "form.html";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        if (File.Exists(path))
        {
            try
            {
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
            MessageBox.Show($"Файл не найден по пути: {path}\n\n" +
                            "Убедитесь, что в свойствах файла 'Copy to Output Directory' установлено 'Copy always'.");
        }
    }
    
    private void HelpAbout_Click(object sender, EventArgs e)
    {
        _contentPanel.Controls.Clear();

        try
        {
            var aboutForm = new AboutForm();
        
            _contentPanel.Controls.Add(aboutForm);
        
            aboutForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть модуль 'О программе': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void MainMenuForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        DatabaseHelper.CloseConnection();
    }
    

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show(
            "Вы действительно хотите выйти из профиля?", 
            "Подтверждение выхода", 
            MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            DatabaseHelper.CurrentUserRole = null; 
            DatabaseHelper.CloseConnection(); 
            this.DialogResult = DialogResult.OK;
            
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
        _contentPanel.Controls.Clear();

        try
        {
            var analyticsForm = new AnalyticsForm();
        
            _contentPanel.Controls.Add(analyticsForm);
        
            analyticsForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть модуль 'Аналитика': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    
}