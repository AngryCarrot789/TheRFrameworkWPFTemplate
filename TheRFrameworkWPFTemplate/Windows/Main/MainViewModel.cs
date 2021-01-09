using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRFramework.Utilities;
using $safeprojectname$.Windows.Help;

namespace $safeprojectname$.Windows.Main
{
    public class MainViewModel : BaseViewModel
    {
        public HelpViewModel Help { get; set; }
        public ThemesViewModel Themes { get; set; }

        public MainViewModel()
        {
            Help = new HelpViewModel();
            Themes = new ThemesViewModel();
        }
    }
}
