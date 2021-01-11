using System;
using System.Windows;
using System.Windows.Media;
using $safeprojectname$.Windows.Logger;

namespace TheRFramework
{
    public static class ThemesController
    {
        public enum ThemeTypes
        {
            Light, ColourfulLight,
            Dark, ColourfulDark
        }

        public static ThemeTypes CurrentTheme { get; set; }

        private static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[0]; }
            set { Application.Current.Resources.MergedDictionaries[0] = value; }
        }

        private static void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
            ApplicationLogger.Log("Theme", "Successful!");
        }
        public static void SetTheme(ThemeTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeTypes.Dark: 
                    themeName = "DarkTheme";
                    ApplicationLogger.Log("Theme", "Changing the theme to the dark theme");
                    break;
                case ThemeTypes.Light: 
                    themeName = "LightTheme"; 
                    ApplicationLogger.Log("Theme", "Changing the theme to the light theme");
                    break;
                case ThemeTypes.ColourfulDark: 
                    themeName = "ColourfulDarkTheme"; 
                    ApplicationLogger.Log("Theme", "Changing the theme to the colourful dark theme");
                    break;
                case ThemeTypes.ColourfulLight: 
                    themeName = "ColourfulLightTheme"; 
                    ApplicationLogger.Log("Theme", "Changing the theme to the colourful light theme");
                    break;
            }

            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(new Uri($"Themes/{themeName}.xaml", UriKind.Relative));
            }
            catch { }
        }

        public static object GetResource(object key)
        {
            return ThemeDictionary[key];
        }

        public static SolidColorBrush GetSolidBrush(string solidColorBrushKey)
        {
            if (GetResource(solidColorBrushKey) is SolidColorBrush brush)
                return brush;
            else 
                return new SolidColorBrush(Colors.White);
        }

        public static Color GetColour(string colourBrushKey)
        {
            if (GetResource(colourBrushKey) is Color colour)
                return colour;
            else
                return Colors.Transparent;
        }
    }
}
