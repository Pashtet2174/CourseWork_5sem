using System.ComponentModel;

namespace CourseWork_5sem;

partial class SettingsForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
        btnIncreaseFont = new System.Windows.Forms.Button();
        btnDecreaseFont = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // btnIncreaseFont
        // 
        btnIncreaseFont.Location = new System.Drawing.Point(67, 53);
        btnIncreaseFont.Name = "btnIncreaseFont";
        btnIncreaseFont.Size = new System.Drawing.Size(117, 68);
        btnIncreaseFont.TabIndex = 0;
        btnIncreaseFont.Text = "➕ ";
        btnIncreaseFont.UseVisualStyleBackColor = true;
        btnIncreaseFont.Click += BtnIncreaseFont_Click;
        // 
        // btnDecreaseFont
        // 
        btnDecreaseFont.Location = new System.Drawing.Point(218, 53);
        btnDecreaseFont.Name = "btnDecreaseFont";
        btnDecreaseFont.Size = new System.Drawing.Size(111, 68);
        btnDecreaseFont.TabIndex = 1;
        btnDecreaseFont.Text = "➖ ";
        btnDecreaseFont.UseVisualStyleBackColor = true;
        btnDecreaseFont.Click += BtnDecreaseFont_Click;
        // 
        // label1
        // 
        label1.BackColor = System.Drawing.Color.Silver;
        label1.Location = new System.Drawing.Point(67, 9);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(262, 41);
        label1.TabIndex = 2;
        label1.Text = "Изменитьразмер шрифта";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        ClientSize = new System.Drawing.Size(1482, 853);
        Controls.Add(label1);
        Controls.Add(btnDecreaseFont);
        Controls.Add(btnIncreaseFont);
        Text = "Настройки";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Button btnIncreaseFont;
    private System.Windows.Forms.Button btnDecreaseFont;

    #endregion
}