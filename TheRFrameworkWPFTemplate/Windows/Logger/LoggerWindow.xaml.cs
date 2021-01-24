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

namespace $safeprojectname$.Windows.Logger
{
    /// <summary>
    /// Interaction logic for LoggerWindow.xaml
    /// </summary>
    public partial class LoggerWindow : Window, BaseView<LoggerViewModel>
    {
        public LoggerViewModel Model { get => DataContext as LoggerViewModel; set => DataContext = value; }

        public LoggerWindow()
        {
            InitializeComponent();
        }
    }
}
