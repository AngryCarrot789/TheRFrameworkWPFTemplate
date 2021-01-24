using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRFramework.Utilities;
using $safeprojectname$.Windows.Help;
using $safeprojectname$.Windows.Logger;
using $safeprojectname$.Windows.Preferences;

namespace $safeprojectname$.Windows.Main
{
    public class MainViewModel : BaseViewModel
    {
        public HelpViewModel Help { get; set; }
        public ThemesViewModel Themes { get; set; }
        public PreferencesViewModel Preferences { get; set; }
        public LoggerViewModel Logs { get; set; }

        // $safeprojectname$

        public MainViewModel()
        {
            Help = new HelpViewModel();
            Themes = new ThemesViewModel();
            Preferences = new PreferencesViewModel(TCSConfig.Main);
            Logs = new LoggerViewModel();
        }
    }
}
