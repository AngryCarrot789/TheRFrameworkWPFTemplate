using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Preferences.SettingsControls
{
    public class SubListSettingViewModel : BaseViewModel
    {
        private string _strValue;

        public string StringValue
        {
            get => _strValue;
            set => RaisePropertyChanged(ref _strValue, value);
        }
    }
}
