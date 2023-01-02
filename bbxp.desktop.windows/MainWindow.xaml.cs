using bbxp.desktop.windows.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;

namespace bbxp.desktop.windows
{
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel Context => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void btnSettings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foSettings.IsOpen = !foSettings.IsOpen;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Context.SavePost();
        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            Context.SaveSettings();

            foSettings.IsOpen = false;
        }
    }
}