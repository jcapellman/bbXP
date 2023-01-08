using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly bbxpDbContext _dbContext;

        private readonly IMemoryCache _memoryCache;

        public BaseController(bbxpDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        protected void ClearCache()
        {
            if (_memoryCache is MemoryCache memoryCache)
            {
                var percentage = 1.0;

                memoryCache.Compact(percentage);
            }
        }

        private void AddToCache(string key, object value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(AppConstants.CACHE_HOUR_EXPIRATION));

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        protected async Task<List<Posts>> GetPostsFromSearchAsync(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            return await _dbContext.Set<Posts>().Where(a => a.Title.ToLower().Contains(searchQuery)).ToListAsync();
        }

        protected async Task<List<string>> GetCategoriesAsync()
        {
            if (_memoryCache.TryGetValue(AppConstants.POST_REQUEST_DEFAULT_CATEGORY, out List<string> result) && result != null)
            {
                return result;
            }

            var dbResult = await _dbContext.Set<Posts>().Where(a => a.Active && 
                a.Category != AppConstants.POST_REQUEST_DEFAULT_CATEGORY && 
                a.Category != AppConstants.POST_REQUEST_INTERNAL_CATEGORY)
                .Select(a => a.Category).Distinct().OrderBy(a => a).ToListAsync();

            if (dbResult == null)
            {
                return new List<string>();
            }

            dbResult.Insert(0, AppConstants.POST_REQUEST_DEFAULT_CATEGORY);

            AddToCache(AppConstants.POST_REQUEST_DEFAULT_CATEGORY, dbResult);

            return dbResult;
        }

        protected async Task<List<Posts>> GetPostsAsync(int postCountLimit = AppConstants.POST_REQUEST_DEFAULT_LIMIT, string category = AppConstants.POST_REQUEST_DEFAULT_CATEGORY)
        {
            if (_memoryCache.TryGetValue(category, out List<Posts> result) && result != null)
            {
                return result;
            }

            List<Posts>? dbResult = null;

            dbResult = category switch
            {
                AppConstants.POST_REQUEST_DEFAULT_CATEGORY => 
                    await _dbContext.Set<Posts>().Where(a => a.Active).OrderByDescending(a => a.PostDate).Take(postCountLimit).ToListAsync(),
                _ => 
                    await _dbContext.Set<Posts>().Where(a => a.Active && a.Category == category).OrderByDescending(a => a.PostDate).Take(postCountLimit).ToListAsync(),
            };

            if (dbResult == null)
            {
                return new List<Posts>();
            }

            AddToCache(category, dbResult);

            return dbResult;
        }

        protected async Task<Posts?> GetPostAsync(string postUrl)
        {
            if (_memoryCache.TryGetValue(postUrl, out Posts? result) && result != null)
            {
                return result;
            }

            var dbResult = await _dbContext.Set<Posts>().FirstOrDefaultAsync(a => a.Active && a.URL == postUrl);

            if (dbResult == null)
            {
                return null;
            }

            AddToCache(postUrl, dbResult);

            return dbResult;
        }

        protected async Task<bool> UpdatePostAsync(PostUpdateRequestItem updatePost)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(a => a.Id == updatePost.Id);

            if (post == null)
            {
                return false;
            }

            post.Title = updatePost.Title;
            post.Body = updatePost.Body;
            post.Category = updatePost.Category;
            post.PostDate = updatePost.PostDate;
            post.URL = updatePost.URL;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        private static string CreateURLSafeTitle(string title) => 
            title.ToLower().Replace(' ', '_').Replace(".", "").Replace(",","").Replace("-", "_");

        protected async Task<bool> AddPostAsync(PostCreationRequestItem newPost)
        {
            var post = new Posts
            {
                PostDate = newPost.PostDate ?? DateTime.Now,
                Body = newPost.Body,
                Title = newPost.Title,
                Category = newPost.Category,
                URL = CreateURLSafeTitle(newPost.Title)
            };

            _dbContext.Posts.Add(post);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        protected async Task<bool> DeletePostAsync(int postId)
        {
            var post = await _dbContext.Posts.FirstAsync(a => a.Id == postId);

            if (post == null)
            {
                return false;
            }

            post.Active = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}