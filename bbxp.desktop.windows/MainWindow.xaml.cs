using bbxp.desktop.windows.ViewModels;

using System.Windows;

namespace bbxp.desktop.windows
{
    public partial class MainWindow : Window
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