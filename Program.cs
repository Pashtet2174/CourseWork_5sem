namespace CourseWork_5sem;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        
        ApplicationConfiguration.Initialize();
        while (true)
        {
            // 1. Показываем форму входа модально
            LoginForm loginForm = new LoginForm();
            
            // ShowDialog() блокирует выполнение до тех пор, пока форма не закроется
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // 2. Если вход успешен (LoginForm установила DialogResult.OK), показываем главное меню
                MainMenuForm mainMenu = new MainMenuForm();
                
                // ShowDialog() блокирует выполнение до тех пор, пока меню не закроется
                if (mainMenu.ShowDialog() == DialogResult.OK)
                {
                    // 3. Если пользователь вышел из меню (MainMenuForm установила DialogResult.OK),
                    // цикл продолжается, и снова показывается LoginForm (Шаг 1).
                    // Если пользователь просто закрыл крестиком, приложение закроется (Шаг 4).
                }
                else
                {
                    // 4. Пользователь закрыл крестиком (MainMenuForm вернула DialogResult.Cancel или None).
                    break; // Выход из цикла и завершение приложения
                }
            }
            else
            {
                // 5. Пользователь закрыл крестиком LoginForm или отменил вход.
                break; // Выход из цикла и завершение приложения
            }
        }
        
    }
}