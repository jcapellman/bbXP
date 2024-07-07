using bbxp.desktop.windows.Managers;
using bbxp.desktop.windows.Objects.JSON;
using bbxp.desktop.windows.ViewModels.Base;

using bbxp.lib.Database.Tables;
using bbxp.lib.HttpHandlers;
using bbxp.lib.JSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace bbxp.desktop.windows.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string SETTINGS_FILENAME = "bbxpsettings.json";

        private string _SearchTerm;

        public string SearchTerm
        {
            get => _SearchTerm;

            set
            {
                _SearchTerm = value.ToLower();

                OnPropertyChanged();

                if (string.IsNullOrEmpty(_SearchTerm))
                {
                    FilteredPosts = Posts;
                } else if (_SearchTerm.Length >= 3)
                {
                    FilteredPosts = Posts.Where(a => a.Title.Contains(_SearchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();
                }

                if (FilteredPosts.Count > 0)
                {
                    SelectedPost = FilteredPosts.First();
                }
            }
        }

        private Visibility _showLoadingIndicator;

        public Visibility ShowLoadingIndicator
        {
            get => _showLoadingIndicator;

            set
            {
                _showLoadingIndicator = value;
                OnPropertyChanged();

                if (value == Visibility.Visible)
                {
                    ShowPostForm = Visibility.Collapsed;
                    ShowPostListing = Visibility.Collapsed;
                } else
                {
                    ShowPostForm = Visibility.Visible;
                    ShowPostListing = Visibility.Visible;
                }
            }
        }

        private Visibility _showCodeView;

        public Visibility ShowCodeView
        {
            get => _showCodeView;

            set
            {
                _showCodeView = value;
                OnPropertyChanged();
            }
        }

        private readonly Visibility _showPreview;

        public Visibility ShowPreview
        {
            get => _showPreview;

            set
            {
                _showCodeView = value;
                OnPropertyChanged();
            }
        }

        private Visibility _showPostListing;

        public Visibility ShowPostListing
        {
            get => _showPostListing;

            set
            {
                _showPostListing = value;
                OnPropertyChanged();
            }
        }

        private Visibility _showPostForm;

        public Visibility ShowPostForm
        {
            get => _showPostForm;

            set
            {
                _showPostForm = value;
                OnPropertyChanged();
            }
        }

        private Settings _settings;

        public Settings Setting
        {
            get => _settings;

            set
            {
                _settings = value;
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

        private List<Posts> _filteredPosts;

        public List<Posts> FilteredPosts
        {
            get => _filteredPosts;

            set
            {
                _filteredPosts = value;

                OnPropertyChanged();
            }
        }

        private string _token = string.Empty;

        public MainViewModel()
        {
            ShowLoadingIndicator = Visibility.Visible;
            ShowCode();

            Setting = new Settings();
            Posts = [];
            FilteredPosts = [];

            SearchTerm = string.Empty;

            LoadSettings();

            LoadData();
        }

        public void ShowCode()
        {
            ShowCodeView = Visibility.Visible;
            ShowPreview = Visibility.Collapsed;
        }

        public void ShowMarkdownRender()
        {
            ShowPreview = Visibility.Visible;
            ShowCodeView = Visibility.Collapsed;
        }

        private void LoadSettings()
        {
            var fullFilePath = Path.Combine(AppContext.BaseDirectory, SETTINGS_FILENAME);

            if (!File.Exists(fullFilePath))
            {
                Setting = new Settings();

                return;
            }

            var str = File.ReadAllText(fullFilePath);

            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            try
            {
                Setting = JsonSerializer.Deserialize<Settings>(str) ?? new Settings();
            } catch (JsonException jex)
            {
                // TODO: Log
                Console.WriteLine(jex.Message);

                Setting = new Settings();
            }
        }

        public async Task<bool> SavePostAsync()
        {
            ShowLoadingIndicator = Visibility.Visible;

            bool result;

            if (SelectedPost.Id == default)
            {
                var createPost = new PostCreationRequestItem { PostDate = SelectedPost.PostDate, Body = SelectedPost.Body, Category = SelectedPost.Category, Title = SelectedPost.Title };

                result = await new PostHttpHandler(Setting.RESTServiceURL, _token).CreateNewPost(createPost);
            } else
            {
                var updatePost = new PostUpdateRequestItem { URL = SelectedPost.URL, PostDate = SelectedPost.PostDate, Body = SelectedPost.Body, Category = SelectedPost.Category, Title = SelectedPost.Title, Id = SelectedPost.Id };

                result = await new PostHttpHandler(Setting.RESTServiceURL, _token).UpdatePost(updatePost);
            }

            if (!result)
            {
                ShowLoadingIndicator = Visibility.Collapsed;

                return false;
            }

            LoadData();

            return true;
        }

        public void NewPost()
        {
            SelectedPost = new Posts { Id = default, Body = string.Empty, Title = string.Empty, Category = string.Empty, PostDate = DateTime.Now, URL = string.Empty };

            ShowCode();
        }

        public void SaveSettings()
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, SETTINGS_FILENAME);

            File.WriteAllText(fullPath, JsonSerializer.Serialize(Setting));

            LoadData();
        }

        private async void LoadData()
        {
            ShowLoadingIndicator = Visibility.Visible;

            if (string.IsNullOrEmpty(Setting?.RESTServiceURL))
            {
                ShowLoadingIndicator = Visibility.Collapsed;

                return;
            }

            if (string.IsNullOrEmpty(_token))
            {
                try
                {
                    _token = await new AccountHttpHandler(Setting.RESTServiceURL).LoginAsync(new UserLoginRequestItem
                    {
                        UserName = Setting.Username,
                        Password = Setting.Password
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    ShowLoadingIndicator = Visibility.Collapsed;

                    return;
                }
            }

            SearchTerm = string.Empty;

            Posts = await new PostHttpHandler(Setting.RESTServiceURL, _token).GetPostsFromDateAsync(LiteDbManager.GetMostRecentPostUpdate()) ?? [];

            if (Posts != null && Posts.Count > 0)
            {
                LiteDbManager.UpdateDatabase(Posts);
            }

            Posts = LiteDbManager.GetLocalPosts();

            if (Posts.Count > 0) {
                FilteredPosts = Posts;

                SelectedPost = FilteredPosts.FirstOrDefault() ?? FilteredPosts.First();
            }

            ShowLoadingIndicator = Visibility.Collapsed;
        }
    }
}