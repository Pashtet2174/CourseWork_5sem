namespace CourseWork_5sem;

public partial class ChangePasswordForm : Form
{
    private readonly string _currentUsername;

    public ChangePasswordForm()
    {
        InitializeComponent();
        this.Text = "Смена пароля пользователя";
        FontManager.FontSizeChanged += FontManager_FontSizeChanged;
        ApplyNewFontSize(FontManager.CurrentFontSize); 
        _currentUsername = DatabaseHelper.CurrentUsername; 
        if (string.IsNullOrEmpty(_currentUsername))
        {
            MessageBox.Show("Не удалось определить текущего пользователя. Войдите снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
            return;
        }
        btnChangePassword.Click += BtnChangePassword_Click;
    }

    private void BtnChangePassword_Click(object sender, EventArgs e)
    {
        string oldPassword = txtOldPassword.Text;
        string newPassword = txtNewPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;

        if (string.IsNullOrWhiteSpace(oldPassword) || 
            string.IsNullOrWhiteSpace(newPassword) || 
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (newPassword.Length < 6)
        {
             MessageBox.Show("Новый пароль должен содержать не менее 6 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             return;
        }

        if (newPassword != confirmPassword)
        {
            MessageBox.Show("Новый пароль и подтверждение не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        if (newPassword == oldPassword)
        {
            MessageBox.Show("Новый пароль не может совпадать со старым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        if (!CheckOldPassword(_currentUsername, oldPassword))
        {
            MessageBox.Show("Старый пароль введен неверно. Попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        ChangePasswordInDatabase(_currentUsername, newPassword);
    }
    
    private bool CheckOldPassword(string username, string password)
    {
        return DatabaseHelper.TestConnection(username, password);
    }
    
    private void ChangePasswordInDatabase(string username, string newPassword)
    {
        try
        {
            string sql = $"SELECT change_password_safe(@p_username, @p_new_password);";
            
            var parameters = new Dictionary<string, object>
            {
                { "@p_username", username },
                { "@p_new_password", newPassword }
            };

            DatabaseHelper.ExecuteNonQuery(sql, parameters);
            DatabaseHelper.UpdateConnectionCredentials(username, newPassword);
            
            MessageBox.Show("Пароль успешно изменен! Выполняется переподключение.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при смене пароля: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void  button1_Click(object sender, EventArgs e)
    {
        if (txtOldPassword.UseSystemPasswordChar == true)
        {
            txtOldPassword.UseSystemPasswordChar = false;
            txtOldPassword.PasswordChar = new char(); 
        }
        else 
        { 
            txtOldPassword.UseSystemPasswordChar = true;
        }
        txtOldPassword.Focus();
    }
    
    private void  button2_Click(object sender, EventArgs e)
    {
        if (txtNewPassword.UseSystemPasswordChar == true)
        {
            txtNewPassword.UseSystemPasswordChar = false;
            txtNewPassword.PasswordChar = new char(); 
        }
        else 
        { 
            txtNewPassword.UseSystemPasswordChar = true;
        }       
        txtNewPassword.Focus();
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