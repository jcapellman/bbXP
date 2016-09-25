using Windows.UI.Xaml.Controls;

using bbxp.UWP.ViewModels;

namespace bbxp.UWP.Views {
    public sealed partial class SearchPage : Page {
        private SearchViewModel viewModel => (SearchViewModel) DataContext;

        public SearchPage() {
            this.InitializeComponent();

            DataContext = new SearchViewModel();
        }

        private void lvSearchListing_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (viewModel.SelectedPost == null) {
                return;
            }

            Frame.Navigate(typeof(PostContentPage), viewModel.GetOriginalPost());
        }
    }
}