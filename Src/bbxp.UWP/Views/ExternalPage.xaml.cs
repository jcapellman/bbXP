using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace bbxp.UWP.Views {
    public sealed partial class ExternalPage : Page {
        public ExternalPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            var menuItem = (MenuItem)e.Parameter;

            txtHeader.Text = menuItem.Name.ToUpper();

            wvMain.Navigate(new Uri(menuItem.Parameter, UriKind.RelativeOrAbsolute));
        }
    }
}