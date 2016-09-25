using Windows.UI.Xaml.Navigation;

using bbxp.PCL.Transports.Posts;
using bbxp.UWP.ViewModels;

namespace bbxp.UWP.Views {
    public sealed partial class PostContentPage : BasePage {
        private PostContentViewModel viewModel => (PostContentViewModel)DataContext;

        public PostContentPage() {
            this.InitializeComponent();

            DataContext = new PostContentViewModel();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            var post = (e.Parameter as PostResponseItem);

            if (post == null) {
                ShowMessage("Could not load Post");

                return;
            }

            viewModel.SetPost(post);
        }
    }
}