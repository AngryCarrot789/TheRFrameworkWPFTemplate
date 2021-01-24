using System;
using System.Collections.ObjectModel;
using System.Windows;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Logger
{
    public class LoggerViewModel : BaseViewModel
    {
        private LoggedItemViewModel _selectedItem;
        public LoggedItemViewModel SelectedItem
        {
            get => _selectedItem;
            set => RaisePropertyChanged(ref _selectedItem, value);
        }

        public ObservableCollection<LoggedItemViewModel> Logged { get; set; }

        public Command ClearItemsCommand { get; }
        public Command ShowViewCommand { get; }

        public LoggerViewModel()
        {
            Logged = new ObservableCollection<LoggedItemViewModel>();

            ClearItemsCommand = new Command(ClearLogs);
            ShowViewCommand = new Command(WindowManager.ShowLogger);
        }

        public void Log(DateTime date, string head, string description)
        {
            LoggedItemViewModel logged = new LoggedItemViewModel()
            {
                Time = date,
                LogHeader = head,
                LogDescription = description
            };
            Logged.Add(logged);
        }

        public void ClearLogs()
        {
            Logged.Clear();
        }
    }
}
