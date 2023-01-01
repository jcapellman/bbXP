using bbxp.desktop.windows.ViewModels.Base;
using bbxp.lib.Database.Tables;
using bbxp.lib.HttpHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbxp.desktop.windows.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string REST_SERVICE_BASE_URL = "";

        private Posts _selectedPost;

        public Posts SelectedPost
        {
            get => _selectedPost;

            set
            {
                _selectedPost = value;
                OnPropertyChanged();
            }
        }

        private List<Posts> _posts;

        public List<Posts> Posts
        {
            get => _posts;

            set
            {
                _posts = value;

                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Posts = new List<Posts>();

            LoadData();
        }

        public async void LoadData()
        {
            Posts = await new PostHttpHandler(REST_SERVICE_BASE_URL).GetPostsAsync(postCountLimit: int.MaxValue);

            if (Posts != null && Posts.Count > 0)
            {
                SelectedPost = Posts.FirstOrDefault();
            }
        }
    }
}