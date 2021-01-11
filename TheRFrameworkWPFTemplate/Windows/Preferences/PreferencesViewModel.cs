using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheRFramework.Utilities;
using $safeprojectname$.Windows.Preferences.SettingsControls;

namespace $safeprojectname$.Windows.Preferences
{
    public class PreferencesViewModel : BaseViewModel
    {
        public ObservableCollection<StringSettingViewModel> StringSettings { get; set; }
        public ObservableCollection<BooleanSettingViewModel> BooleanSettings { get; set; }
        // you could remove these "List Settings" if you dont need em. idk why i
        // added them apart from 'i was bored'
        public ObservableCollection<ListSettingViewModel> ListSettings { get; set; }

        public Command CloseViewCommand { get; }
        public Command ShowViewCommand { get; }
        public Command ReloadConfigCommand { get; }
        public Command SaveConfigCommand { get; }

        public TCSConfig Config { get; private set; }

        public PreferencesViewModel()
        {
            Config = new TCSConfig(TCSConfig.DEFAULT_CONFIG_FILE_NAME);
            StringSettings = new ObservableCollection<StringSettingViewModel>();
            BooleanSettings = new ObservableCollection<BooleanSettingViewModel>();
            ListSettings = new ObservableCollection<ListSettingViewModel>();

            CloseViewCommand = new Command(WindowManager.HidePreferences);
            ShowViewCommand = new Command(ShowView);

            ReloadConfigCommand = new Command(ReloadConfig);
            SaveConfigCommand = new Command(SaveConfig);
        }

        private void ShowView()
        {
            ReloadConfig();
            WindowManager.ShowPreferences();
        }

        public void SaveConfig()
        {
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

            foreach (ListSettingViewModel l in ListSettings)
            {
                List<string> strings = new List<string>();
                foreach(SubListSettingViewModel str in l.StringValues)
                {
                    strings.Add(str.StringValue);
                }
                Config.SetList(l.FieldName, strings);
            }

            Config.SaveConfig();
        }

        public void ReloadConfig()
        {
            Config.Reload();

            StringSettings.Clear();
            BooleanSettings.Clear();
            ListSettings.Clear();

            foreach(KeyValuePair<string, string> pair in Config.StringValues)
            {
                if (bool.TryParse(pair.Value, out bool bResult))
                {
                    BooleanSettings.Add(new BooleanSettingViewModel() { FieldName = pair.Key, Value = bResult });
                }
                else
                {
                    StringSettings.Add(new StringSettingViewModel() { FieldName = pair.Key, Value = pair.Value });
                }
            }

            foreach (KeyValuePair<string, List<string>> pair in Config.ListValues)
            {
                ObservableCollection<SubListSettingViewModel> subs = new ObservableCollection<SubListSettingViewModel>();
                foreach(string str in pair.Value)
                {
                    subs.Add(new SubListSettingViewModel() { StringValue = str });
                }
                ListSettings.Add(new ListSettingViewModel() { FieldName = pair.Key, StringValues = subs });
            }
        }
    }
}
