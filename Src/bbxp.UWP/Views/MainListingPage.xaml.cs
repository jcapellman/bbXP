using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using bbxp.UWP.ViewModels;

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
    }
}