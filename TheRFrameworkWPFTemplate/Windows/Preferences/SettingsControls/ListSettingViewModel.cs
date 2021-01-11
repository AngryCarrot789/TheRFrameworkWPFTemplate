using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Preferences.SettingsControls
{
    public class ListSettingViewModel : BaseViewModel
    {
        private string _fieldName;
        private ObservableCollection<SubListSettingViewModel> _stringValue;

        public string FieldName
        {
            get => _fieldName;
            set => RaisePropertyChanged(ref _fieldName, value);
        }

        public ObservableCollection<SubListSettingViewModel> StringValues
        {
            get => _stringValue;
            set => RaisePropertyChanged(ref _stringValue, value);
        }
    }
}
