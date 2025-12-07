namespace CourseWork_5sem;

public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill; 
            FontManager.FontSizeChanged += FontManager_FontSizeChanged;
            ApplyNewFontSize(FontManager.CurrentFontSize);
            
        }

        private void BtnIncreaseFont_Click(object sender, EventArgs e)
        {
            // Вызываем статический метод для увеличения шрифта
            FontManager.IncreaseFont();
            // Опционально: можно добавить MessageBox.Show("Шрифт увеличен.");
        }

        private void BtnDecreaseFont_Click(object sender, EventArgs e)
        {
            // Вызываем статический метод для уменьшения шрифта
            FontManager.DecreaseFont();
            // Опционально: можно добавить MessageBox.Show("Шрифт уменьшен.");
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