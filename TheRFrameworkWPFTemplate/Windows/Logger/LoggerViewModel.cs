using System;
using System.Collections.ObjectModel;
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

            ClearItemsCommand = new Command(ClearItems);
            ShowViewCommand = new Command(WindowManager.ShowLogger);

            // optional, but hook into the main application logger
            // and display it here
            ApplicationLogger.LogInformation += Logger_LogInformation;
        }

        private void Logger_LogInformation(DateTime date, string header, string description)
        {
            Log(date, header, description);
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

        public void ClearItems()
        {
            Logged.Clear();
        }
    }
}
