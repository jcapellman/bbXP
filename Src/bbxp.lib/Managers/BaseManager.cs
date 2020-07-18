using bbxp.lib.Containers;
using bbxp.lib.DAL;
using bbxp.lib.Enums;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Security.Cryptography;
using System.Text;

namespace bbxp.lib.Managers
{
    public class BaseManager
    {
        protected readonly ManagerContainer mContainer;

        private IMemoryCache Cache => mContainer.Cache;

        protected BbxpDbContext DbContext => mContainer.DBContext;

        protected (bool IsFound, T CachedResult) GetCachedItem<T>(MainCacheKeys key) =>
            GetCachedItem<T>(key.ToString());

        protected (bool IsFound, T CachedResult) GetCachedItem<T>(string key)
        {
            return !Cache.TryGetValue(key, out T cacheEntry) ? (false, default) : (true, Cache.Get<T>(key));
        }

        protected void AddCachedItem<T>(MainCacheKeys key, T obj)
        {
            AddCachedItem(key.ToString(), obj);
        }

        protected void RemoveCachedItem(MainCacheKeys key)
        {
            Cache.Remove(key.ToString());
        }

        protected void RemoveCachedItem(string key)
        {
            Cache.Remove(key);
        }

        protected void AddCachedItem<T>(string key, T obj)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.MaxValue);

            Cache.Set(key, obj, cacheEntryOptions);
        }

        protected static string HashString(string input)
        {
            using (var algorithm = SHA512.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(input));

                var hashbytes = algorithm.ComputeHash(hash);

                return Convert.ToBase64String(hashbytes);
            }
        }

        protected BaseManager(ManagerContainer container)
        {
            mContainer = container;
        }
    }
}