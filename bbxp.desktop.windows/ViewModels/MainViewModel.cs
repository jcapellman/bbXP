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

namespace bbxp.desktop.windows.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string SETTINGS_FILENAME = "bbxpsettings.json";

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

        private string _token = string.Empty;

        public MainViewModel()
        {
            Setting = new Settings();
            Posts = new List<Posts>();

            LoadSettings();

            LoadData();
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

        public async void SavePost()
        {
            if (SelectedPost.Id == 0)
            {
                var createPost = new PostCreationRequestItem { Body = SelectedPost.Body, Category = SelectedPost.Category, Title = SelectedPost.Title };

                await new PostHttpHandler(Setting.RESTServiceURL, _token).CreateNewPost(createPost);

                return;
            }

            var updatePost = new PostUpdateRequestItem { Body = SelectedPost.Body, Category = SelectedPost.Category, Title = SelectedPost.Title, Id = SelectedPost.Id };

            await new PostHttpHandler(Setting.RESTServiceURL, _token).UpdatePost(updatePost);
        }

        public void SaveSettings()
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, SETTINGS_FILENAME);

            File.WriteAllText(fullPath, JsonSerializer.Serialize(Setting));

            LoadData();
        }

        private async void LoadData()
        {
            if (string.IsNullOrEmpty(Setting?.RESTServiceURL))
            {
                return;
            }

            try
            {
                _token = await new AccountHttpHandler(Setting.RESTServiceURL).LoginAsync(new UserLoginRequestItem { 
                    UserName = Setting.Username, 
                    Password = Setting.Password
                });
            } catch (Exception ex)
            {
                var message = ex.Message;

                return;
            }

            Posts = await new PostHttpHandler(Setting.RESTServiceURL).GetPostsAsync(postCountLimit: int.MaxValue);

            if (Posts != null && Posts.Count > 0)
            {
                SelectedPost = Posts.FirstOrDefault();
            }
        }
    }
}