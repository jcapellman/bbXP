using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using bbxp.PCL.Handlers;
using bbxp.PCL.Transports.Posts;

namespace bbxp.UWP.ViewModels {
    public class MainListingViewModel : BaseViewModel {
        private ObservableCollection<PostResponseItem> _posts;

        public ObservableCollection<PostResponseItem> Posts {
            get { return _posts; }
            set { _posts = value; OnPropertyChanged(); }
        }

        private PostResponseItem _selectedPost;

        public PostResponseItem SelectedPost {
            get { return _selectedPost; }
            set { _selectedPost = value; OnPropertyChanged(); }
        }

        public PostResponseItem GetOriginalPost() => _originalPosts.FirstOrDefault(a => a.RelativeURL == SelectedPost.RelativeURL);

        private List<PostResponseItem> _originalPosts;

        public async Task<bool> LoadData() {
            var postHandler = new PostHandler(GSettings);

            var posts = await postHandler.GetMainListing();

            if (posts.HasError) {
                throw new Exception(posts.ExceptionMessage);
            }

            _originalPosts = posts.ReturnValue.Select(a => new PostResponseItem(a)).ToList();

            foreach (var item in posts.ReturnValue) {
                item.Body = await GenerateFinalRender(item.Body, true);
            }

            Posts = new ObservableCollection<PostResponseItem>(posts.ReturnValue);

            return true;
        }
    }
}