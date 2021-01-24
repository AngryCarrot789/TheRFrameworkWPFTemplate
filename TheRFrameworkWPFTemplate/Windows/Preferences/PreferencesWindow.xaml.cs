using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheRFramework.Utilities;

namespace $safeprojectname$.Windows.Preferences
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window, BaseView<PreferencesViewModel>
    {
        public PreferencesViewModel Model
        {
            get => DataContext as PreferencesViewModel; 
            set => DataContext = value;
        }

        public PreferencesWindow()
        {
            InitializeComponent();
        }
    }
}
