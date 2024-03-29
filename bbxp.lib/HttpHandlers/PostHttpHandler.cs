﻿using bbxp.lib.Common;
using bbxp.lib.Database.Tables;
using bbxp.lib.HttpHandlers.Base;
using bbxp.lib.JSON;

namespace bbxp.lib.HttpHandlers
{
    public class PostHttpHandler : BaseHttpHandler
    {
        public PostHttpHandler(string baseAddress, string? token = null) : base(baseAddress, token) { }

        public async Task<bool> CreateNewPost(PostCreationRequestItem newPost) => await PostAsync("posts", newPost);

        public async Task<bool> UpdatePost(PostUpdateRequestItem updatePost) => await PatchAsync("posts", updatePost);

        public async Task<List<Posts>> GetPostsAsync(string category = AppConstants.POST_REQUEST_DEFAULT_CATEGORY, int postCountLimit = AppConstants.POST_REQUEST_DEFAULT_LIMIT) 
            => await GetAsync<List<Posts>>($"posts/{category}/{postCountLimit}");

        public async Task<Posts?> GetSinglePostAsync(string postUrl) => await GetAsync<Posts?>($"posts/{postUrl}");

        public async Task<List<string>> GetPostCategoriesAsync() => await GetAsync<List<string>>("postcategories/");

        public async Task<List<Posts>> SearchPostsAsync(string searchQuery) => await GetAsync<List<Posts>>($"postsearch/{searchQuery}");
    }
}