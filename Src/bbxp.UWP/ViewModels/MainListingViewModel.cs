﻿using System;
using System.Collections.ObjectModel;
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

        private bool _SelectedPostPopupOpen;

        public bool SelectedPostPopupOpen {
            get { return _SelectedPostPopupOpen; }
            set { _SelectedPostPopupOpen = value;  OnPropertyChanged(); }
        }

        private PostResponseItem _selectedPost;

        public PostResponseItem SelectedPost {
            get { return _selectedPost; }
            set { _selectedPost = value;  OnPropertyChanged();  if (value != null) { SelectedPostPopupOpen = true; } }
        }

        public async Task<bool> LoadData() {
            var postHandler = new PostHandler(GSettings);

            var posts = await postHandler.GetMainListing();

            if (posts.HasError) {
                throw new Exception(posts.ExceptionMessage);
            }

            foreach (var item in posts.ReturnValue) {
                item.Body = await GenerateFinalRender(item.Body);
            }

            Posts = new ObservableCollection<PostResponseItem>(posts.ReturnValue);

            return true;
        }
    }
}