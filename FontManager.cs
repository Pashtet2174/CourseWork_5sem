namespace CourseWork_5sem;

public static class FontManager
{
    private const float DefaultFontSize = 9.75f; 
    private static float _fontMultiplier = 1.0f; 
    private const float MaxMultiplier = 1.5f; 
    public static event EventHandler FontSizeChanged;
    public static float CurrentFontSize => DefaultFontSize * _fontMultiplier;

    
    public static void IncreaseFont()
    {
        if (_fontMultiplier < MaxMultiplier)
        {
            _fontMultiplier += 0.1f;
            OnFontSizeChanged();
        }
    }
    
    public static void DecreaseFont()
    {
        if (_fontMultiplier > 1.0f) 
        {
            _fontMultiplier -= 0.1f; // Уменьшаем на 10%
            OnFontSizeChanged();
        }
    }

    private static void OnFontSizeChanged()
    {
        FontSizeChanged?.Invoke(null, EventArgs.Empty);
    }
}