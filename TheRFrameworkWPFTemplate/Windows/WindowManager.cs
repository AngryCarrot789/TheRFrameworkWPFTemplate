using System.Windows;
using $safeprojectname$.Windows.Help;
using $safeprojectname$.Windows.Logger;
using $safeprojectname$.Windows.Main;
using $safeprojectname$.Windows.Preferences;

namespace $safeprojectname$.Windows
{
    public static class WindowManager
    {
        public static App Application { get; private set; }
        public static string[] StartupArguments { get; private set; }

        public static MainWindow Main { get; private set; }
        public static HelpWindow Help { get; private set; }
        public static PreferencesWindow Preferences { get; private set; }
        public static LoggerWindow Logs { get; private set; }

        public static void Initialise(App app, string[] args)
        {
            Application = app;
            StartupArguments = args;

            Main = new MainWindow();
            Help = new HelpWindow();
            Preferences = new PreferencesWindow();
            Logs = new LoggerWindow();

            // stops a memory leak, because next is to replace the window
            // viewmodels with the MainViewMode ones
            ApplicationLogger.LogInformation -= Logs.Model.Log;

            Help.Model = Main.Model.Help;
            Preferences.Model = Main.Model.Preferences;
            Logs.Model = Main.Model.Logs;

            // have to call after the main LoggerViewModel/LoggerWindow is initialised
            // because thats the only thing that hooks the events so far
            ApplicationLogger.Log("Application", "Starting application");
            ApplicationLogger.Log("Windows Events", "Setting up window closing event hooks...");

            Main.Closing += WindowClosing;
            Help.Closing += WindowClosing;
            Preferences.Closing += WindowClosing;

            ApplicationLogger.Log("Windows Events", "Success!");

            Application.MainWindow = Main;

            ApplicationLogger.Log("Application", "Successfully setup application");
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
            Preferences.Closing -= WindowClosing;

            Application.Shutdown();
        }

        public static void ShowPreferences()
        {
            Preferences.Show();
        }

        public static void HidePreferences()
        {
            Preferences.Hide();
        }

        public static void ShowLogger()
        {
            Logs.Show();
        }

        public static void HideLogger()
        {
            Logs.Hide();
        }
    }
}
