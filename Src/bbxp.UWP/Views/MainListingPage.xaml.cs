using System.Linq;

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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedItem = e.AddedItems.FirstOrDefault();

            if (selectedItem == null) {
                return;
            }

            viewModel.SelectedPost = (PostResponseItem)selectedItem;
        }
    }
}