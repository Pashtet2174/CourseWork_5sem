using System.ComponentModel;

namespace CourseWork_5sem;

partial class RecordEditForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordEditForm));
        saveButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        _flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
        SuspendLayout();
        // 
        // saveButton
        // 
        saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        saveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
        saveButton.Location = new System.Drawing.Point(0, 391);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(795, 58);
        saveButton.TabIndex = 1;
        saveButton.Text = "✅ Сохранить";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveData_Click;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        cancelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
        cancelButton.Location = new System.Drawing.Point(0, 315);
        cancelButton.Margin = new System.Windows.Forms.Padding(3, 3, 100, 3);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(795, 76);
        cancelButton.TabIndex = 2;
        cancelButton.Text = "❌ Отмена";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // _flowLayoutPanel
        // 
        _flowLayoutPanel.AutoScroll = true;
        _flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        _flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        _flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
        _flowLayoutPanel.Name = "_flowLayoutPanel";
        _flowLayoutPanel.Size = new System.Drawing.Size(795, 315);
        _flowLayoutPanel.TabIndex = 3;
        // 
        // RecordEditForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(795, 449);
        Controls.Add(_flowLayoutPanel);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        MinimumSize = new System.Drawing.Size(400, 400);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "RecordEditForm";
        ResumeLayout(false);
    }

    private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel;

    private System.Windows.Forms.Button cancelButton;

    private System.Windows.Forms.Button saveButton;

    #endregion
}