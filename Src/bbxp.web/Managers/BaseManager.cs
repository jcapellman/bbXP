using System;

using bbxp.lib.Containers;
using bbxp.lib.Enums;

using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.Managers {
    public class BaseManager {
        protected readonly ManagerContainer mContainer;

        private IMemoryCache Cache => mContainer.Cache;

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

        protected void AddCachedItem<T>(string key, T obj)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.MaxValue);

            Cache.Set(key, obj, cacheEntryOptions);
        }

        protected BaseManager(ManagerContainer container) {
            mContainer = container;
        }
    }
}