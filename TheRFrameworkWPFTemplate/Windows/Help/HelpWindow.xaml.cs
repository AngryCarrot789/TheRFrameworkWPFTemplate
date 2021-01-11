using System.Windows;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Help
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window, BaseView<HelpViewModel>
    {
        public HelpViewModel Model  { get => DataContext as HelpViewModel; set => DataContext = value; }

        public HelpWindow()
        {
            InitializeComponent();
        }
    }
}
