using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Preferences.SettingsControls
{
    public class StringSettingViewModel : BaseViewModel
    {
        private string _fieldName;
        private string _value;

        public string FieldName
        {
            get => _fieldName;
            set => RaisePropertyChanged(ref _fieldName, value);
        }

        public string Value
        {
            get => _value;
            set => RaisePropertyChanged(ref _value, value);
        }
    }
}
