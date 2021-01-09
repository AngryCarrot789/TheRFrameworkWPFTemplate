using TheRFramework.Utilities;
using TheRFramework;

namespace $safeprojectname$.Windows
{
    public class ThemesViewModel : BaseViewModel
    {
        public CommandParam<string> SetThemeCommand { get; }

        public ThemesViewModel()
        {
            SetThemeCommand = new CommandParam<string>(SetTheme);
        }

        public void SetTheme(string themeName)
        {
            switch (themeName)
            {
                case "nd": ThemesController.SetTheme(ThemesController.ThemeTypes.Dark); break;
                case "nl": ThemesController.SetTheme(ThemesController.ThemeTypes.Light); break;
                case "cd": ThemesController.SetTheme(ThemesController.ThemeTypes.ColourfulDark); break;
                case "cl": ThemesController.SetTheme(ThemesController.ThemeTypes.ColourfulLight); break;
            }
        }
    }
}
