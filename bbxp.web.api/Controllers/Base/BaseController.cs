using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;

using LimDB.lib;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers.Base
{
    public class BaseController(BbxpContext dbContext, IMemoryCache memoryCache) : ControllerBase
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        protected void ClearCache()
        {
            if (_memoryCache is not MemoryCache memoryCache)
            {
                return;
            }

            memoryCache.Compact(100);
        }

        private void AddToCache(string key, object value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(AppConstants.CACHE_HOUR_EXPIRATION));

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        protected IEnumerable<Posts> GetPostsFromSearchAsync(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            return dbContext.Posts.Where(a => a.Title.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase));
        }

        protected async Task<List<string>> GetCategoriesAsync()
        {
            if (_memoryCache.TryGetValue(AppConstants.POST_REQUEST_DEFAULT_CATEGORY, out List<string> result) && result != null)
            {
                return result;
            }

            var dbResult = await dbContext.Posts.Where(a => a.Active &&
                                                         a.Category != AppConstants.POST_REQUEST_DEFAULT_CATEGORY &&
                                                         a.Category != AppConstants.POST_REQUEST_INTERNAL_CATEGORY)
                .Select(a => a.Category).Distinct().OrderBy(a => a).ToListAsync();

            AddToCache(AppConstants.POST_REQUEST_DEFAULT_CATEGORY, dbResult);

            return dbResult;
        }

        protected async Task<List<Posts>> GetPostsAsync(int postCountLimit = AppConstants.POST_REQUEST_DEFAULT_LIMIT, string category = AppConstants.POST_REQUEST_DEFAULT_CATEGORY)
        {
            if (_memoryCache.TryGetValue(category, out List<Posts> result) && result != null)
            {
                return result;
            }

            List<Posts> dbResult = [];

            dbResult = category switch
            {
                AppConstants.POST_REQUEST_DEFAULT_CATEGORY => 
                     await dbContext.Posts.Where(a => a.Active).OrderByDescending(a => a.PostDate).Take(postCountLimit).ToListAsync(),
                _ => 
                    await dbContext.Posts.Where(a => a.Active && a.Category == category).OrderByDescending(a => a.PostDate).ToListAsync(),
            };

            AddToCache(category, dbResult);

            return dbResult;
        }

        protected Posts? GetPostAsync(string postUrl)
        {
            if (_memoryCache.TryGetValue(postUrl, out Posts? result) && result != null)
            {
                return result;
            }

            var dbResult = dbContext.Posts.FirstOrDefault(a => a.Active && a.URL == postUrl);

            if (dbResult == null)
            {
                return null;
            }

            AddToCache(postUrl, dbResult);

            return dbResult;
        }

        protected async Task<bool> UpdatePostAsync(PostUpdateRequestItem updatePost)
        {
            var post = dbContext.Posts.FirstOrDefault(a => a.Id == updatePost.Id);

            if (post is null)
            {
                return false;
            }

            post.Title = updatePost.Title;
            post.Body = updatePost.Body;
            post.Category = updatePost.Category;
            post.PostDate = updatePost.PostDate;
            post.URL = updatePost.URL;

            return await dbContext.SaveChangesAsync() > 0;
        }

        private static string CreateUrlSafeTitle(string title) => 
            title.ToLower().Replace(' ', '_').Replace(".", "").Replace(",","").Replace("-", "_");

        protected async Task<bool> AddPostAsync(PostCreationRequestItem newPost)
        {
            var post = new Posts
            {
                PostDate = newPost.PostDate ?? DateTime.Now,
                Body = newPost.Body,
                Title = newPost.Title,
                Category = newPost.Category,
                URL = CreateUrlSafeTitle(newPost.Title)
            };

            dbContext.Posts.Add(post);

            return await dbContext.SaveChangesAsync() > 0;
        }
        protected async Task<bool> DeletePostAsync(int postId)
        {
            var post = await dbContext.Posts.FirstAsync(a => a.Id == postId);

            if (post == null)
            {
                return false;
            }

            post.Active = false;

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}