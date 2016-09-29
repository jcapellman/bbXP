using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using bbxp.UWP.ViewModels;

namespace bbxp.UWP.Views {
    public sealed partial class ArchivesPage : BasePage {
        private ArchivesViewModel viewModel => (ArchivesViewModel) DataContext;

        public ArchivesPage() {
            this.InitializeComponent();

            DataContext = new ArchivesViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            viewModel.LoadData();
        }

        private void lvPostListing_SelectionChanged(object sender, RoutedEventArgs e) {
            if (viewModel.SelectedArchivePost == null) {
                return;
            }

            Frame.Navigate(typeof(PostContentPage), viewModel.GetOriginalPost());
        }
    }
}