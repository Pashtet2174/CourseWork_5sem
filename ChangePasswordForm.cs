namespace CourseWork_5sem;

public partial class ChangePasswordForm : Form
{
    private readonly string _currentUsername;

    public ChangePasswordForm()
    {
        InitializeComponent();
        this.Text = "Смена пароля пользователя";

        // Получаем текущее имя пользователя из DatabaseHelper (под которым мы подключены)
        _currentUsername = DatabaseHelper.CurrentUsername; // Предполагается, что вы сохраняете имя пользователя при входе
        
        if (string.IsNullOrEmpty(_currentUsername))
        {
            MessageBox.Show("Не удалось определить текущего пользователя. Войдите снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
            return;
        }

        // Настраиваем Label для отображения имени пользователя (если у вас есть такой Label)
        // lblUsername.Text = $"Пользователь: {_currentUsername}";

        // Подписываемся на событие кнопки
        btnChangePassword.Click += BtnChangePassword_Click;
    }

    private void BtnChangePassword_Click(object sender, EventArgs e)
    {
        string oldPassword = txtOldPassword.Text;
        string newPassword = txtNewPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;

        // 1. Валидация введенных данных
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

        // 2. Проверка старого пароля (требует повторной аутентификации)
        if (!CheckOldPassword(_currentUsername, oldPassword))
        {
            MessageBox.Show("Старый пароль введен неверно. Попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // 3. Вызов функции для смены пароля в БД
        ChangePasswordInDatabase(_currentUsername, newPassword);
    }

    /// <summary>
    /// Проверяет старый пароль, пытаясь подключиться к БД с текущим именем пользователя и старым паролем.
    /// </summary>
    private bool CheckOldPassword(string username, string password)
    {
        // Используем вспомогательный метод в DatabaseHelper, который временно 
        // пытается создать новое соединение с указанными учетными данными.
        return DatabaseHelper.TestConnection(username, password);
    }

    /// <summary>
    /// Вызывает функцию change_password_safe для смены пароля.
    /// </summary>
    private void ChangePasswordInDatabase(string username, string newPassword)
    {
        try
        {
            // Вызываем функцию change_password_safe(p_username, p_new_password)
            string sql = $"SELECT change_password_safe(@p_username, @p_new_password);";
            
            var parameters = new Dictionary<string, object>
            {
                { "@p_username", username },
                { "@p_new_password", newPassword }
            };

            DatabaseHelper.ExecuteNonQuery(sql, parameters);
            
            // 4. Обновление подключения
            
            // После успешной смены пароля в БД, необходимо обновить:
            // a) Хранимый пароль в DatabaseHelper (если вы его храните)
            // b) Текущее активное соединение (если вы используете одно).
            
            // ВАЖНО: Мы должны переподключиться, чтобы система продолжила работать с новым паролем.
            
            DatabaseHelper.UpdateConnectionCredentials(username, newPassword);
            
            MessageBox.Show("Пароль успешно изменен! Выполняется переподключение.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            // Функция может выдать ошибку, если у пользователя нет прав 
            // (хотя SECURITY DEFINER должен это предотвратить), или другие ошибки БД.
            MessageBox.Show($"Ошибка при смене пароля: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}