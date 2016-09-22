using System;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using bbxp.UWP.ViewModels;

namespace bbxp.UWP.Views {
    public sealed partial class ContentPage : Page {
        private ContentViewModel viewModel => (ContentViewModel)DataContext;

        public ContentPage() {
            this.InitializeComponent();

            DataContext = new ContentViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var urlArg = e.Parameter.ToString();

            var result = await viewModel.LoadData(urlArg);

            if (result) {
                wvMain.NavigateToString(viewModel.Content.Body);
            }
        }
    }
}