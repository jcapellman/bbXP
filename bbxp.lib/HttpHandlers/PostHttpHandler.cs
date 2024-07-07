using bbxp.lib.Common;
using bbxp.lib.Database.Tables;
using bbxp.lib.HttpHandlers.Base;
using bbxp.lib.JSON;

namespace bbxp.lib.HttpHandlers
{
    public class PostHttpHandler(string baseAddress, string? token = null) : BaseHttpHandler(baseAddress, token)
    {
        public async Task<bool> CreateNewPost(PostCreationRequestItem newPost) => await PostAsync("post-admin", newPost);

        public async Task<bool> UpdatePost(PostUpdateRequestItem updatePost) => await PatchAsync("post-admin", updatePost);

        public async Task<List<Posts>?> GetPostsAsync(string category = LibConstants.POST_REQUEST_DEFAULT_CATEGORY, int postCountLimit = LibConstants.POST_REQUEST_DEFAULT_LIMIT) 
            => await GetAsync<List<Posts>?>($"posts/{category}/{postCountLimit}");

        public async Task<List<Posts>?> GetPostsFromDateAsync(DateTime date) => await GetAsync<List<Posts>>($"post-admin/{date:MM-dd-yyyy}");

        public async Task<Posts?> GetSinglePostAsync(string postUrl) => await GetAsync<Posts?>($"posts/{postUrl}");

        public async Task<List<string>?> GetPostCategoriesAsync() => await GetAsync<List<string>?>("postcategories/");

        public async Task<List<Posts>?> SearchPostsAsync(string searchQuery) => await GetAsync<List<Posts>?>($"postsearch/{searchQuery}");
    }
}