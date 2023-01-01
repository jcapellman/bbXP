using bbxp.desktop.windows.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;

namespace bbxp.desktop.windows
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}