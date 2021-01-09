using System.Windows;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, BaseView<MainViewModel>
    {
        public MainViewModel Model => DataContext as MainViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
