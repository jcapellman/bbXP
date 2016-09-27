using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace bbxp.UWP.Views {
    public sealed partial class ExternalPage : Page {
        public ExternalPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            wvMain.Navigate(new Uri(e.Parameter.ToString(), UriKind.RelativeOrAbsolute));
        }
    }
}