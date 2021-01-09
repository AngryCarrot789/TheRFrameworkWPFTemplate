using System.Windows;
using $safeprojectname$.Windows.Help;
using $safeprojectname$.Windows.Main;

namespace $safeprojectname$.Windows
{
    public static class WindowManager
    {
        public static App Application { get; private set; }
        public static string[] StartupArguments { get; private set; }

        public static MainWindow Main { get; private set; }
        public static HelpWindow Help { get; private set; }

        public static void Initialise(App app, string[] args)
        {
            Application = app;
            StartupArguments = args;

            Help = new HelpWindow();
            Main = new MainWindow();

            Main.Closing += WindowClosing;
            Help.Closing += WindowClosing;

            Application.MainWindow = Main;
        }

        private static void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            if (sender is MainWindow mainWindow)
            {
                ShutdownApplication();
            }
            else
            {
                (sender as Window)?.Hide();
            }
        }

        public static void ShowHelp()
        {
            Help.Show();
        }

        private static void ShutdownWindows()
        {

        }

        public static void ShutdownApplication()
        {
            ShutdownWindows();

            Main.Closing -= WindowClosing;
            Help.Closing -= WindowClosing;

            Application.Shutdown();
        }
    }
}
