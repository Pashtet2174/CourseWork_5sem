namespace CourseWork_5sem;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }
    private void btnLogin_Click(object sender, EventArgs e)
    {
        string loginInput = txtLogin.Text.Trim(); 
        string passwordInput = txtPassword.Text;
        
        if (string.IsNullOrWhiteSpace(loginInput))
        {
            MessageBox.Show("Пожалуйста, введите логин.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtLogin.Focus(); 
            return;
        }

        if (string.IsNullOrWhiteSpace(passwordInput))
        {
            MessageBox.Show("Пожалуйста, введите пароль.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPassword.Focus();
            return;
        }
        
        btnLogin.Enabled = false;
        Cursor.Current = Cursors.WaitCursor;
        bool isConnected = DatabaseHelper.TryLogin(loginInput, passwordInput); 
        Cursor.Current = Cursors.Default;
        btnLogin.Enabled = true;

        if (isConnected)
        {
            this.DialogResult = DialogResult.OK; 
        }
        else
        {
            txtPassword.Clear();
            txtPassword.Focus();
        }
    }
    private void txtPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnLogin.PerformClick();
        }
    }
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