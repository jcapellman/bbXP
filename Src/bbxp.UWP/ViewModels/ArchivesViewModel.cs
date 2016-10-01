using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using bbxp.PCL.Handlers;
using bbxp.PCL.Transports.PostArchive;
using bbxp.PCL.Transports.Posts;

namespace bbxp.UWP.ViewModels {
    public class ArchivesViewModel : BaseViewModel {
        private ObservableCollection<PostArchiveListingWrapper> _archivePosts;

        public ObservableCollection<PostArchiveListingWrapper> ArchivePosts {
            get { return _archivePosts; }
            set { _archivePosts = value; OnPropertyChanged(); }
        }

        private PostArchiveListingWrapper _SelectedArchivePost;

        public PostArchiveListingWrapper SelectedArchivePost {
            get { return _SelectedArchivePost; }
            set {
                _SelectedArchivePost = _SelectedArchivePost == value ? null : value;

                OnPropertyChanged();
                LoadPosts();
            }
        }
        
        public PostResponseItem GetOriginalPost() => _originalPosts.FirstOrDefault(a => a.RelativeURL == SelectedArchivePost.SelectedPost.RelativeURL);

        private List<PostResponseItem> _originalPosts;

        private async void LoadPosts() {
            if (SelectedArchivePost == null) {
                return;
            }

            if (SelectedArchivePost.Posts.Count > 0) {
                var index = ArchivePosts.IndexOf(SelectedArchivePost);

                var post = SelectedArchivePost;

                post.Posts.Clear();

                ArchivePosts[index] = post;
            } else {
                var month = SelectedArchivePost.DateString.Split(' ')[0];

                var monthInt = Convert.ToDateTime(month + " 01, 1900").Month;

                var posts = await new PostArchiveHandler(GSettings).GetPostsFromMonth(Convert.ToInt32(SelectedArchivePost.DateString.Split(' ')[1]), monthInt);

                var index = ArchivePosts.IndexOf(SelectedArchivePost);

                _originalPosts.AddRange(posts.ReturnValue);

                SelectedArchivePost.Posts = posts.ReturnValue;

                ArchivePosts[index] = SelectedArchivePost;
            }
        }

        public async void LoadData() {
            _originalPosts = new List<PostResponseItem>();

            var archiveHandler = new PostArchiveHandler(GSettings);

            var archivePosts = await archiveHandler.GetArchiveList();

            if (archivePosts.HasError) {
                throw new Exception(archivePosts.ExceptionMessage);
            }

            var posts = new ObservableCollection<PostArchiveListingWrapper>();

            foreach (var item in archivePosts.ReturnValue) {
                var wrapperItem = new PostArchiveListingWrapper {
                    RelativeURL = item.RelativeURL,
                    Count = item.Count,
                    DateString = item.DateString,
                    Posts = new List<PostResponseItem>()
                };
                
                posts.Add(wrapperItem);
            }

            ArchivePosts = posts;
        }
    }
}