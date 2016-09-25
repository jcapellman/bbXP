using bbxp.PCL.Transports.Posts;

namespace bbxp.UWP.ViewModels {
    public class PostContentViewModel : BaseViewModel {
        private PostResponseItem _post;

        public PostResponseItem Post {
            get { return _post; }
            set { _post = value; OnPropertyChanged(); }
        }

        public async void SetPost(PostResponseItem post) {
            post.Body = await GenerateFinalRender(post.Body, false);

            Post = post;
        }
    }
}