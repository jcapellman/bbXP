using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;
using LimDB.lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly LimDbContext<Posts> _dbContext;

        private readonly IMemoryCache _memoryCache;

        public BaseController(LimDbContext<Posts> dbContext, IMemoryCache memoryCache)
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

        protected IEnumerable<Posts> GetPostsFromSearchAsync(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            return _dbContext.GetMany(a => a.Title.ToLower().Contains(searchQuery));
        }

        protected IOrderedEnumerable<string> GetCategoriesAsync()
        {
            if (_memoryCache.TryGetValue(AppConstants.POST_REQUEST_DEFAULT_CATEGORY, out IOrderedEnumerable<string> result) && result != null)
            {
                return result;
            }

            var dbResult = _dbContext.GetMany(a => a.Active &&
                                                         a.Category != AppConstants.POST_REQUEST_DEFAULT_CATEGORY &&
                                                         a.Category != AppConstants.POST_REQUEST_INTERNAL_CATEGORY)
                .Select(a => a.Category).Distinct().OrderBy(a => a);

//            dbResult.Insert(0, AppConstants.POST_REQUEST_DEFAULT_CATEGORY);

            AddToCache(AppConstants.POST_REQUEST_DEFAULT_CATEGORY, dbResult);

            return dbResult;
        }

        protected IEnumerable<Posts> GetPostsAsync(int postCountLimit = AppConstants.POST_REQUEST_DEFAULT_LIMIT, string category = AppConstants.POST_REQUEST_DEFAULT_CATEGORY)
        {
            if (_memoryCache.TryGetValue(category, out IEnumerable<Posts> result) && result != null)
            {
                return result;
            }

            IEnumerable<Posts>? dbResult = null;

            dbResult = category switch
            {
                AppConstants.POST_REQUEST_DEFAULT_CATEGORY => 
                     _dbContext.GetMany(a => a.Active).OrderByDescending(a => a.PostDate).Take(postCountLimit),
                _ => 
                     _dbContext.GetMany(a => a.Active && a.Category == category).OrderByDescending(a => a.PostDate),
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

            var dbResult = _dbContext.GetOne(a => a.Active && a.URL == postUrl);

            if (dbResult == null)
            {
                return null;
            }

            AddToCache(postUrl, dbResult);

            return dbResult;
        }

        protected bool UpdatePostAsync(PostUpdateRequestItem updatePost)
        {
            var post = _dbContext.GetOneById(updatePost.Id);

            if (post == null)
            {
                return false;
            }

            post.Title = updatePost.Title;
            post.Body = updatePost.Body;
            post.Category = updatePost.Category;
            post.PostDate = updatePost.PostDate;
            post.URL = updatePost.URL;

            return false;
            // TODO: LimDb CRUD
            //  return await _dbContext.SaveChangesAsync() > 0;
        }

        private static string CreateURLSafeTitle(string title) => 
            title.ToLower().Replace(' ', '_').Replace(".", "").Replace(",","").Replace("-", "_");

        protected bool AddPostAsync(PostCreationRequestItem newPost)
        {
            var post = new Posts
            {
                PostDate = newPost.PostDate ?? DateTime.Now,
                Body = newPost.Body,
                Title = newPost.Title,
                Category = newPost.Category,
                URL = CreateURLSafeTitle(newPost.Title)
            };

            //_dbContext.Posts.Add(post);

            return false;
            // return await _dbContext.SaveChangesAsync() > 0;
        }

        protected bool DeletePostAsync(int postId)
        {
            var post = _dbContext.GetOneById(postId);

            if (post == null)
            {
                return false;
            }

            post.Active = false;

            return false;
            // return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}