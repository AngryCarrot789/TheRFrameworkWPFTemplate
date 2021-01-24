using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheRFramework;
using TheRFramework.Utilities;
using $safeprojectname$.Windows.Logger;
using $safeprojectname$.Windows.Preferences.SettingsControls;
using $safeprojectname$.Windows.Preferences.SettingsControls.Lists;
using $safeprojectname$.Windows.Preferences.SettingsControls.Strings;

namespace $safeprojectname$.Windows.Preferences
{
    public class PreferencesViewModel : BaseViewModel
    {
        public ObservableCollection<StringSettingViewModel> StringSettings { get; set; }
        public ObservableCollection<BooleanSettingViewModel> BooleanSettings { get; set; }
        // you could remove these "List Settings" if you dont need em. idk why i
        // added them apart from 'i was bored'
        public ObservableCollection<ListSettingViewModel> ListSettings { get; set; }

        public CommandParam<string> CloseViewCommand { get; }
        public Command ShowViewCommand { get; }
        public CommandParam<string> ReloadConfigCommand { get; }
        public Command SaveConfigCommand { get; }

        public TCSConfig Config { get; private set; }

        public PreferencesViewModel(TCSConfig config)
        {
            Config = config;
            StringSettings = new ObservableCollection<StringSettingViewModel>();
            BooleanSettings = new ObservableCollection<BooleanSettingViewModel>();
            ListSettings = new ObservableCollection<ListSettingViewModel>();

            CloseViewCommand = new CommandParam<string>(CloseView);
            ShowViewCommand = new Command(ShowView);

            ReloadConfigCommand = new CommandParam<string>(ReloadConfig);
            SaveConfigCommand = new Command(SaveConfig);

            // Example of how to use the config. you need the "regenerate config when app next starts"
            // which tells you if you need to "generate" the config values.
            if (Config.TryGetBoolean("regenerate config when app next starts (dont touch this)", out bool shouldRegenerate))
            {
                if (shouldRegenerate)
                {
                    // This shouldnt really be possible to get to unless
                    // you manually edit the config or set it in the app
                    // for some reason.......
                    GenerateConfig();
                }
                else
                {
                    // Config has all its stuff. So load the values
                    ReloadConfig();
                }
            }
            else
            {
                // The config file is empty, so generate it :)
                GenerateConfig();
            }
        }

        private void ShowView()
        {
            WindowManager.ShowPreferences();
        }

        private void CloseView(string saveConfig)
        {
            if (saveConfig != null && saveConfig == "t")
            {
                SaveConfig();
            }

            WindowManager.HidePreferences();
        }

        public void SaveConfig()
        {
            ApplicationLogger.Log("Config", "Saving config...");
            Config.ListValues.Clear();
            Config.StringValues.Clear();
            foreach(StringSettingViewModel s in StringSettings)
            {
                Config.SetString(s.FieldName, s.Value);
            }

            foreach (BooleanSettingViewModel b in BooleanSettings)
            {
                Config.SetString(b.FieldName, b.Value.ToString());
            }

            foreach (ListSettingViewModel list in ListSettings)
            {
                List<string> strings = new List<string>();
                foreach(SubListSettingViewModel str in list.StringValues)
                {
                    strings.Add(str.StringValue);
                }
                Config.SetList(list.FieldName, strings);
            }

            Config.SaveConfig();

            ApplicationLogger.Log("Config", "Saved Successfully!");
            ProcessConfig();
        }

        public void ReloadConfig(string fromRamOrDisk = "d")
        {
            if (fromRamOrDisk == "d")
            {
                ApplicationLogger.Log("Config", "Reloading config from disk...");
                Config.Reload();
            }

            ApplicationLogger.Log("Config", "Loading values from config...");

            StringSettings.Clear();
            BooleanSettings.Clear();
            ListSettings.Clear();

            foreach (KeyValuePair<string, string> pair in Config.StringValues)
            {
                if (bool.TryParse(pair.Value, out bool bResult))
                {
                    BooleanSettings.Add(new BooleanSettingViewModel(pair.Key, bResult));
                }
                else
                {
                    StringSettings.Add(new StringSettingViewModel(pair.Key, pair.Value));
                }
            }

            foreach (KeyValuePair<string, List<string>> pair in Config.ListValues)
            {
                ObservableCollection<SubListSettingViewModel> subs = new ObservableCollection<SubListSettingViewModel>();
                foreach (string str in pair.Value)
                {
                    subs.Add(new SubListSettingViewModel(str));
                }
                ListSettings.Add(new ListSettingViewModel() { FieldName = pair.Key, StringValues = subs });
            }

            ApplicationLogger.Log("Config", "Successfully reloaded config!");

            ProcessConfig();
        }

        private void ProcessConfig()
        {
            // Normally you'd use the config in code, like if pressing enter added a new line.
            // but you can use this method for processing things like the theme for the application

            ApplicationLogger.Log("Config", "Processing config...");

            if (Config.TryGetEnum("theme", out ThemesController.ThemeTypes themeValue))
            {
                ThemesController.SetTheme(themeValue);
            }

            ApplicationLogger.Log("Config", "Config values processed!");
        }

        private void GenerateConfig()
        {
            // Make sure not to leave any fields or values empty, e.g. "not empty", "" <- empty lol
            // otherwise the parser will probably crash or break something.....

            // This should be where you define all the preferences you need in your app.
            // this are some... "examples"...

            ApplicationLogger.Log("Config", "Generating Config...");

            BooleanSettings.Add(new BooleanSettingViewModel("regenerate config when app next starts (dont touch this)", false));

            StringSettings.Add(new StringSettingViewModel("sample string", "indeed"));
            StringSettings.Add(new StringSettingViewModel("another sample", "ok"));
            StringSettings.Add(new StringSettingViewModel("number of retries", "69"));

            // for enums you have to do it like this :/ might implement a combobox soon
            StringSettings.Add(new StringSettingViewModel("theme", "Dark"));

            BooleanSettings.Add(new BooleanSettingViewModel("F11 Key makes app fullscreen", true));
            BooleanSettings.Add(new BooleanSettingViewModel("Is this config hard to use", true));
            BooleanSettings.Add(new BooleanSettingViewModel("is it really though", true));
            BooleanSettings.Add(new BooleanSettingViewModel("are you sure about that", false));

            // You can create a a list of things like so, a bit more organised tbh
            ListSettings.Add(
                new ListSettingViewModel(
                    "entities",
                    new List<SubListSettingViewModel>()
                    {
                        new SubListSettingViewModel("Sheep"),
                        new SubListSettingViewModel("Cow"),
                        new SubListSettingViewModel("Google"),
                        new SubListSettingViewModel("Pigeon"),
                    }));

            ListSettings.Add(
                new ListSettingViewModel(
                    "people",
                    new List<SubListSettingViewModel>()
                    {
                        new SubListSettingViewModel("jake"),
                        new SubListSettingViewModel("h"),
                        new SubListSettingViewModel("joe"),
                        new SubListSettingViewModel("microwavee"),
                    }));

            // Could also use it like this, done by using the params keyword
            ListSettings.Add(
                new ListSettingViewModel(
                    "testing params",
                    new SubListSettingViewModel("t1"),
                    new SubListSettingViewModel("h"),
                    new SubListSettingViewModel("h"),
                    new SubListSettingViewModel("h")
                    ));

            ApplicationLogger.Log("Config", "Config Generated!");

            SaveConfig();
        }
    }
}
