using System.Windows;
using TheRFramework.Utilities;

namespace TheRFrameworkWPFTemplate.Windows.Help
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window, BaseView<HelpViewModel>
    {
        public HelpViewModel Model => DataContext as HelpViewModel;

        public HelpWindow()
        {
            InitializeComponent();
        }
    }
}
