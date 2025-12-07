using System.ComponentModel;
    
namespace CourseWork_5sem;

partial class LoginForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
        btnLogin = new System.Windows.Forms.Button();
        txtLogin = new System.Windows.Forms.TextBox();
        txtPassword = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        btnTogglePasswordVisibility = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // btnLogin
        // 
        btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        btnLogin.BackColor = System.Drawing.Color.LightGray;
        btnLogin.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btnLogin.Location = new System.Drawing.Point(815, 357);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new System.Drawing.Size(250, 82);
        btnLogin.TabIndex = 0;
        btnLogin.Text = "Вход";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // txtLogin
        // 
        txtLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        txtLogin.BackColor = System.Drawing.SystemColors.Info;
        txtLogin.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        txtLogin.Location = new System.Drawing.Point(559, 167);
        txtLogin.Name = "txtLogin";
        txtLogin.Size = new System.Drawing.Size(535, 36);
        txtLogin.TabIndex = 1;
        // 
        // txtPassword
        // 
        txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        txtPassword.BackColor = System.Drawing.SystemColors.Info;
        txtPassword.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        txtPassword.Location = new System.Drawing.Point(559, 255);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new System.Drawing.Size(535, 36);
        txtPassword.TabIndex = 2;
        txtPassword.UseSystemPasswordChar = true;
        txtPassword.KeyDown += txtPassword_KeyDown;
        // 
        // label1
        // 
        label1.BackColor = System.Drawing.Color.Transparent;
        label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label1.Location = new System.Drawing.Point(400, 167);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(142, 40);
        label1.TabIndex = 3;
        label1.Text = "Логин";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        label2.BackColor = System.Drawing.Color.Transparent;
        label2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label2.Location = new System.Drawing.Point(400, 255);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(143, 40);
        label2.TabIndex = 4;
        label2.Text = "Пароль";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label3
        // 
        label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        label3.BackColor = System.Drawing.Color.Transparent;
        label3.Font = new System.Drawing.Font("Calibri", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label3.Location = new System.Drawing.Point(195, 5);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(858, 159);
        label3.TabIndex = 5;
        label3.Text = "Информационная система  магазина автозапчастей";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // btnTogglePasswordVisibility
        // 
        btnTogglePasswordVisibility.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        btnTogglePasswordVisibility.BackColor = System.Drawing.Color.LightGray;
        btnTogglePasswordVisibility.Location = new System.Drawing.Point(1113, 255);
        btnTogglePasswordVisibility.Name = "btnTogglePasswordVisibility";
        btnTogglePasswordVisibility.Size = new System.Drawing.Size(36, 36);
        btnTogglePasswordVisibility.TabIndex = 6;
        btnTogglePasswordVisibility.Text = "👁️";
        btnTogglePasswordVisibility.UseVisualStyleBackColor = false;
        btnTogglePasswordVisibility.Click += btnTogglePasswordVisibility_Click;
        // 
        // LoginForm
        // 
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        BackColor = System.Drawing.SystemColors.GradientActiveCaption;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        ClientSize = new System.Drawing.Size(1192, 599);
        Controls.Add(btnTogglePasswordVisibility);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(txtPassword);
        Controls.Add(txtLogin);
        Controls.Add(btnLogin);
        Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        Margin = new System.Windows.Forms.Padding(40, 16, 40, 16);
        MaximizeBox = false;
        Text = "Вход";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button btnTogglePasswordVisibility;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Button btnLogin;
    private System.Windows.Forms.TextBox txtLogin;
    private System.Windows.Forms.TextBox txtPassword;

    #endregion
}