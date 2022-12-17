using bbxp.lib.Database;
using bbxp.lib.Database.Tables.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

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

        protected async Task<IEnumerable<T>> GetManyAsync<T>(Func<T, bool>? expression = null) where T : BaseTable
        {
            var key = GetKey<T>(expression);

            if (_memoryCache.TryGetValue(key, out IEnumerable<T> result) && result != null)
            {
                return result;
            }

            var dbResult = await _dbContext.FindAsync<IEnumerable<T>>(expression);

            if (dbResult == null)
            {
                return Enumerable.Empty<T>();
            }

            _memoryCache.Set(key, dbResult);

            return dbResult;
        }
    }
}