using System.ComponentModel;

namespace CourseWork_5sem;

partial class ChangePasswordForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
        lblUsername = new System.Windows.Forms.Label();
        txtOldPassword = new System.Windows.Forms.TextBox();
        txtNewPassword = new System.Windows.Forms.TextBox();
        txtConfirmPassword = new System.Windows.Forms.TextBox();
        btnChangePassword = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // lblUsername
        // 
        lblUsername.BackColor = System.Drawing.Color.NavajoWhite;
        lblUsername.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        lblUsername.Location = new System.Drawing.Point(279, 118);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new System.Drawing.Size(255, 37);
        lblUsername.TabIndex = 0;
        lblUsername.Text = "Старый пароль";
        lblUsername.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // txtOldPassword
        // 
        txtOldPassword.BackColor = System.Drawing.Color.Bisque;
        txtOldPassword.Location = new System.Drawing.Point(279, 193);
        txtOldPassword.Name = "txtOldPassword";
        txtOldPassword.PasswordChar = '●';
        txtOldPassword.Size = new System.Drawing.Size(255, 28);
        txtOldPassword.TabIndex = 1;
        txtOldPassword.UseSystemPasswordChar = true;
        // 
        // txtNewPassword
        // 
        txtNewPassword.BackColor = System.Drawing.Color.Bisque;
        txtNewPassword.Location = new System.Drawing.Point(279, 356);
        txtNewPassword.Name = "txtNewPassword";
        txtNewPassword.PasswordChar = '●';
        txtNewPassword.Size = new System.Drawing.Size(255, 28);
        txtNewPassword.TabIndex = 2;
        txtNewPassword.UseSystemPasswordChar = true;
        // 
        // txtConfirmPassword
        // 
        txtConfirmPassword.BackColor = System.Drawing.Color.Bisque;
        txtConfirmPassword.Location = new System.Drawing.Point(279, 468);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.PasswordChar = '●';
        txtConfirmPassword.Size = new System.Drawing.Size(255, 28);
        txtConfirmPassword.TabIndex = 3;
        txtConfirmPassword.UseSystemPasswordChar = true;
        // 
        // btnChangePassword
        // 
        btnChangePassword.BackColor = System.Drawing.Color.Khaki;
        btnChangePassword.Location = new System.Drawing.Point(279, 534);
        btnChangePassword.Name = "btnChangePassword";
        btnChangePassword.Size = new System.Drawing.Size(255, 47);
        btnChangePassword.TabIndex = 4;
        btnChangePassword.Text = "Сменить пароль";
        btnChangePassword.UseVisualStyleBackColor = false;
        // 
        // label1
        // 
        label1.BackColor = System.Drawing.Color.NavajoWhite;
        label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label1.Location = new System.Drawing.Point(279, 291);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(255, 37);
        label1.TabIndex = 5;
        label1.Text = "Новый пароль";
        label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // label2
        // 
        label2.BackColor = System.Drawing.Color.NavajoWhite;
        label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label2.Location = new System.Drawing.Point(279, 400);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(255, 37);
        label2.TabIndex = 6;
        label2.Text = "Подтвердите пароль";
        label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // button1
        // 
        button1.BackColor = System.Drawing.Color.PapayaWhip;
        button1.Location = new System.Drawing.Point(557, 193);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(44, 30);
        button1.TabIndex = 7;
        button1.Text = "👁️";
        button1.UseVisualStyleBackColor = false;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.BackColor = System.Drawing.Color.PapayaWhip;
        button2.Location = new System.Drawing.Point(557, 356);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(44, 30);
        button2.TabIndex = 8;
        button2.Text = "👁️";
        button2.UseVisualStyleBackColor = false;
        button2.Click += button2_Click;
        // 
        // ChangePasswordForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        ClientSize = new System.Drawing.Size(871, 662);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(btnChangePassword);
        Controls.Add(txtConfirmPassword);
        Controls.Add(txtNewPassword);
        Controls.Add(txtOldPassword);
        Controls.Add(lblUsername);
        Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Смена пароля";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Button btnChangePassword;

    private System.Windows.Forms.TextBox txtOldPassword;
    private System.Windows.Forms.TextBox txtNewPassword;
    private System.Windows.Forms.TextBox txtConfirmPassword;

    private System.Windows.Forms.Label lblUsername;

    #endregion
}