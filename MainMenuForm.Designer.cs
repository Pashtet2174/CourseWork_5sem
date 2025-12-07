using System.ComponentModel;

namespace CourseWork_5sem;

partial class MainMenuForm
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
        button1 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(724, 1);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(74, 30);
        button1.TabIndex = 0;
        button1.Text = "выход";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // MainMenuForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(button1);
        Text = "MainMenuForm";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button button1;

    #endregion
}