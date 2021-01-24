using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Preferences.SettingsControls.Lists
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

        public ListSettingViewModel() { }
        public ListSettingViewModel(string field)
        {
            FieldName = field;
        }
        public ListSettingViewModel(string field, List<SubListSettingViewModel> values)
        {
            FieldName = field;
            StringValues = new ObservableCollection<SubListSettingViewModel>(values);
        }

        public ListSettingViewModel(string field, params SubListSettingViewModel[] values)
        {
            FieldName = field;
            StringValues = new ObservableCollection<SubListSettingViewModel>(values);
        }
    }
}
