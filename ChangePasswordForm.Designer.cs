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
        lblUsername = new System.Windows.Forms.Label();
        txtOldPassword = new System.Windows.Forms.TextBox();
        txtNewPassword = new System.Windows.Forms.TextBox();
        txtConfirmPassword = new System.Windows.Forms.TextBox();
        btnChangePassword = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // lblUsername
        // 
        lblUsername.Location = new System.Drawing.Point(11, 12);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new System.Drawing.Size(131, 30);
        lblUsername.TabIndex = 0;
        lblUsername.Text = "label1";
        // 
        // txtOldPassword
        // 
        txtOldPassword.Location = new System.Drawing.Point(253, 54);
        txtOldPassword.Name = "txtOldPassword";
        txtOldPassword.PasswordChar = '*';
        txtOldPassword.Size = new System.Drawing.Size(251, 27);
        txtOldPassword.TabIndex = 1;
        txtOldPassword.UseSystemPasswordChar = true;
        // 
        // txtNewPassword
        // 
        txtNewPassword.Location = new System.Drawing.Point(250, 137);
        txtNewPassword.Name = "txtNewPassword";
        txtNewPassword.Size = new System.Drawing.Size(243, 27);
        txtNewPassword.TabIndex = 2;
        txtNewPassword.UseSystemPasswordChar = true;
        // 
        // txtConfirmPassword
        // 
        txtConfirmPassword.Location = new System.Drawing.Point(250, 217);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.Size = new System.Drawing.Size(288, 27);
        txtConfirmPassword.TabIndex = 3;
        txtConfirmPassword.UseSystemPasswordChar = true;
        // 
        // btnChangePassword
        // 
        btnChangePassword.Location = new System.Drawing.Point(261, 288);
        btnChangePassword.Name = "btnChangePassword";
        btnChangePassword.Size = new System.Drawing.Size(301, 82);
        btnChangePassword.TabIndex = 4;
        btnChangePassword.Text = "Сменить пароль";
        btnChangePassword.UseVisualStyleBackColor = true;
        // 
        // ChangePasswordForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(btnChangePassword);
        Controls.Add(txtConfirmPassword);
        Controls.Add(txtNewPassword);
        Controls.Add(txtOldPassword);
        Controls.Add(lblUsername);
        Text = "ChangePasswordForm";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button btnChangePassword;

    private System.Windows.Forms.TextBox txtOldPassword;
    private System.Windows.Forms.TextBox txtNewPassword;
    private System.Windows.Forms.TextBox txtConfirmPassword;

    private System.Windows.Forms.Label lblUsername;

    #endregion
}