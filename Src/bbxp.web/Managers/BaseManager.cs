using System;

using bbxp.lib.Containers;

using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.Managers {
    public class BaseManager {
        protected readonly ManagerContainer mContainer;

        protected IMemoryCache Cache => mContainer.Cache;

        protected (bool IsFound, T CachedResult) GetCachedItem<T>(string key)
        {
            return !Cache.TryGetValue(key, out T cacheEntry) ? (false, default) : (true, Cache.Get<T>(key));
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