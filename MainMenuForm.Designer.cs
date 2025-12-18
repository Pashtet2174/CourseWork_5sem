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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
        button1 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        button1.BackColor = System.Drawing.Color.Silver;
        button1.CausesValidation = false;
        button1.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)64)), ((int)((byte)64)), ((int)((byte)64)));
        button1.Location = new System.Drawing.Point(1406, 0);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(74, 30);
        button1.TabIndex = 0;
        button1.Text = "выход";
        button1.UseVisualStyleBackColor = false;
        button1.Click += button1_Click;
        // 
        // MainMenuForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        ClientSize = new System.Drawing.Size(1482, 853);
        Controls.Add(button1);
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        MaximumSize = new System.Drawing.Size(1500, 900);
        MinimumSize = new System.Drawing.Size(1500, 900);
        Text = "MainMenuForm";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button button1;

    #endregion
}