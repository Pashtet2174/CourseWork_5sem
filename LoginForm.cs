namespace CourseWork_5sem;

public partial class LoginForm : Form
{
    
    public LoginForm()
    {
        InitializeComponent();
    }
    
    private void btnLogin_Click(object sender, EventArgs e)
    {
        // 1. Считываем данные из текстовых полей
        // Trim() удаляет случайные пробелы в начале и конце
        string loginInput = txtLogin.Text.Trim(); 
        string passwordInput = txtPassword.Text;

        // 2. Валидация на пустые поля
        if (string.IsNullOrWhiteSpace(loginInput))
        {
            MessageBox.Show("Пожалуйста, введите логин.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtLogin.Focus(); // Возвращаем курсор в поле логина
            return;
        }

        if (string.IsNullOrWhiteSpace(passwordInput))
        {
            MessageBox.Show("Пожалуйста, введите пароль.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPassword.Focus();
            return;
        }

        // 3. Блокируем интерфейс на время подключения
        btnLogin.Enabled = false;
        Cursor.Current = Cursors.WaitCursor;

        // 4. Попытка входа
        // Важно: Пользователь должен вводить точный логин БД (например, user_director)
        bool isConnected = DatabaseHelper.TryLogin(loginInput, passwordInput);

        // Возвращаем интерфейс в обычное состояние
        Cursor.Current = Cursors.Default;
        btnLogin.Enabled = true;

        if (isConnected)
        {
            // Успешный вход
            this.DialogResult = DialogResult.OK; 
        }
        else
        {
            // Ошибка входа - очищаем только пароль, логин оставляем для удобства
            txtPassword.Clear();
            txtPassword.Focus();
        }
    }

    // Обработка нажатия Enter (чтобы не кликать мышкой по кнопке)
    private void txtPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnLogin.PerformClick();
        }
    }
    
    // Завершение работы приложения при закрытии окна входа
    private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }


    private void btnTogglePasswordVisibility_Click(object sender, EventArgs e)
    {
        if (txtPassword.UseSystemPasswordChar == true)
        {
            txtPassword.UseSystemPasswordChar = false;
            txtPassword.PasswordChar = new char(); 
        }
        else 
        { 
            txtPassword.UseSystemPasswordChar = true;
        }
        txtPassword.Focus();
    }


    
}