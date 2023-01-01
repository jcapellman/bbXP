using bbxp.desktop.windows.ViewModels.Base;
using bbxp.lib.Database.Tables;
using bbxp.lib.HttpHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbxp.desktop.windows.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string SETTINGS_FILENAME = "bbxpsettings.json";

        private string _restServiceURL;

        public string RestServiceURL
        {
            get => _restServiceURL;

            set
            {
                _restServiceURL = value;
                OnPropertyChanged();
            }
        }

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

            LoadSettings();

            LoadData();
        }

        private void LoadSettings()
        {
            var fullFilePath = Path.Combine(AppContext.BaseDirectory, SETTINGS_FILENAME);

            if (!File.Exists(fullFilePath))
            {
                return;
            }

            RestServiceURL = File.ReadAllText(fullFilePath);
        }

        public void SaveSettings()
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, SETTINGS_FILENAME);

            File.WriteAllText(fullPath, RestServiceURL);

            LoadData();
        }

        private async void LoadData()
        {
            if (string.IsNullOrEmpty(RestServiceURL))
            {
                return;
            }

            Posts = await new PostHttpHandler(RestServiceURL).GetPostsAsync(postCountLimit: int.MaxValue);

            if (Posts != null && Posts.Count > 0)
            {
                SelectedPost = Posts.FirstOrDefault();
            }
        }
    }
}