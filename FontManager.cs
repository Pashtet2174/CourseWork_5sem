namespace CourseWork_5sem;

public static class FontManager
{
    // Базовый размер шрифта, который вы используете в приложении (например, 9.75)
    private const float DefaultFontSize = 9.75f; 
        
    // Переменная для хранения текущего множителя размера шрифта
    private static float _fontMultiplier = 1.0f; // 1.0f = 100% (нормальный размер)

    // Максимальное увеличение (например, 1.5x от базового размера)
    private const float MaxMultiplier = 1.5f; 

    // Событие, которое оповещает формы о необходимости обновить шрифт
    public static event EventHandler FontSizeChanged;

    /// <summary>
    /// Возвращает текущий размер шрифта для использования в элементах управления.
    /// </summary>
    public static float CurrentFontSize => DefaultFontSize * _fontMultiplier;

    /// <summary>
    /// Увеличивает размер шрифта, если не достигнут максимум.
    /// </summary>
    public static void IncreaseFont()
    {
        if (_fontMultiplier < MaxMultiplier)
        {
            _fontMultiplier += 0.1f; // Увеличиваем на 10%
                
            // Вызываем событие, чтобы уведомить все подписанные формы
            OnFontSizeChanged();
        }
    }

    /// <summary>
    /// Уменьшает размер шрифта, если не ниже минимума (1.0).
    /// </summary>
    public static void DecreaseFont()
    {
        if (_fontMultiplier > 1.0f) // Не уменьшаем ниже базового размера
        {
            _fontMultiplier -= 0.1f; // Уменьшаем на 10%
                
            // Вызываем событие, чтобы уведомить все подписанные формы
            OnFontSizeChanged();
        }
    }

    private static void OnFontSizeChanged()
    {
        // Проверяем, есть ли подписчики, и вызываем событие
        FontSizeChanged?.Invoke(null, EventArgs.Empty);
    }
}