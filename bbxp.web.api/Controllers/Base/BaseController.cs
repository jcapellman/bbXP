using bbxp.lib.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers.Base
{
    public class BaseController(IMemoryCache memoryCache) : ControllerBase
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

        protected T AddToCache<T>(string key, T value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(LibConstants.CACHE_HOUR_EXPIRATION));

            _memoryCache.Set(key, value, cacheEntryOptions);

            return value;
        }

        protected T? GetFromCache<T>(string key)
        {
            if (!_memoryCache.TryGetValue(key, out var value) || value is null)
            {
                return default;
            }

            return (T)value;
        }
    }
}