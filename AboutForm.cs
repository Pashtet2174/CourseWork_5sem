namespace CourseWork_5sem;

public partial class AboutForm : Form
{
    public AboutForm()
    {
        InitializeComponent();
        this.TopLevel = false;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Dock = System.Windows.Forms.DockStyle.Fill;
        FontManager.FontSizeChanged += FontManager_FontSizeChanged;
        ApplyNewFontSize(FontManager.CurrentFontSize); 
    }
    private void FontManager_FontSizeChanged(object sender, EventArgs e)
    {
        ApplyNewFontSize(FontManager.CurrentFontSize);
    }
    private void ApplyNewFontSize(float newSize)
    {
        this.Font = new Font(this.Font.FontFamily, newSize, this.Font.Style);
        foreach (Control control in this.Controls)
        {
            UpdateControlFont(control, newSize);
        }
    }
    private void UpdateControlFont(Control parent, float newSize)
    {
        if (parent.Font != null)
        {
            parent.Font = new Font(parent.Font.FontFamily, newSize, parent.Font.Style);
        }
        foreach (Control child in parent.Controls)
        {
            UpdateControlFont(child, newSize);
        }
    }
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        FontManager.FontSizeChanged -= FontManager_FontSizeChanged;
        base.OnFormClosed(e);
    }
}