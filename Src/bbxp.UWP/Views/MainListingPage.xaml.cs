using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using bbxp.UWP.ViewModels;
using bbxp.PCL.Transports.Posts;

namespace bbxp.UWP.Views {
    public sealed partial class MainListingPage : Page {
        private MainListingViewModel viewModel => (MainListingViewModel)DataContext;

        public MainListingPage() {
            this.InitializeComponent();

            DataContext = new MainListingViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel.LoadData();

            if (!result) {
             //   ShowMessage("Could not load posts");
            }
        }
        
        private async void lvPostListing_RefreshCommand(object sender, EventArgs e) {
            await viewModel.LoadData();
        }

        private void lvPostListing_SelectionChanged(object sender, RoutedEventArgs e) {
            if (viewModel.SelectedPost == null) {
                return;
            }

            Frame.Navigate(typeof (PostContentPage), viewModel.GetOriginalPost());
        }
    }
}