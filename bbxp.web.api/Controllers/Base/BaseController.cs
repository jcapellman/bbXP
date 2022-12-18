using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.blazor.Server.Controllers.Base
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

        private static string GetKey<T>(Func<T,bool>? expression = null)
        {
            if (expression == null)
            {
                return typeof(T).ToString();
            }

            return expression.ToString() ?? string.Empty;
        }

        private void AddToCache(string key, object value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(AppConstants.CACHE_HOUR_EXPIRATION));

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        protected IEnumerable<Posts> GetPosts(Func<Posts, bool> expression)
        {
            var key = GetKey(expression);

            if (_memoryCache.TryGetValue(key, out IEnumerable<Posts> result) && result != null)
            {
                return result;
            }

            var dbResult = _dbContext.GetPosts(expression);

            if (dbResult == null)
            {
                return Enumerable.Empty<Posts>();
            }

            AddToCache(key, dbResult);

            return dbResult;
        }

        protected async Task<bool> AddPostAsync(PostCreationRequestItem newPost)
        {
            var post = new Posts
            {
                PostDate = DateTime.Now,
                Body = newPost.Body,
                Title = newPost.Title,
                Category = newPost.Category
            };

            _dbContext.Posts.Add(post);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}