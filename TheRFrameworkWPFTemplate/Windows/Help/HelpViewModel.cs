using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Help
{
    public class HelpViewModel : BaseViewModel
    {
        private string _applicationName;
        private string _authors;
        private string _description;

        public string ApplicationName
        {
            get => _applicationName;
            set => RaisePropertyChanged(ref _applicationName, value);
        }

        public string AuthorsDescription
        {
            get => _authors;
            set => RaisePropertyChanged(ref _authors, value);
        }

        public string Description
        {
            get => _description;
            set => RaisePropertyChanged(ref _description, value);
        }

        public Command ShowHelpCommand { get; }

        public HelpViewModel()
        {
            ShowHelpCommand = new Command(WindowManager.ShowHelp);

            ApplicationName = "Template Application";
            AuthorsDescription = "Made by Kettlesimulator/AngryCarrot789/TheR/Carrot";

            StringBuilder description = new StringBuilder();
            description.AppendLine("A description of the application here.");
            description.AppendLine("This template is useful for getting a simple app up and running very quickly, instead of having to spend half an hour writing MVVM boiler code");
            description.AppendLine("Another line because why not. You can add lines in the HelpViewModel instead of doing it in the window");
            description.Append("A final line here. lol");
            Description = description.ToString();
        }
    }
}
