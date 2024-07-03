using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postcategories")]
    public class PostCategoriesController(BbxpContext dbContext, IMemoryCache memoryCache) : BaseController(memoryCache)
    {
        [HttpGet]
        public async Task<List<string>> GetPostCategoriesAsync()
        {
            var cacheResult = GetFromCache<List<string>>(LibConstants.POST_CATEGORY_CACHE_KEY);

            if (cacheResult is not null)
            {
                return cacheResult;
            }

            var dbResult = await dbContext.Posts.Where(a => a.Active &&
                                                         a.Category != LibConstants.POST_REQUEST_DEFAULT_CATEGORY &&
                                                         a.Category != LibConstants.POST_REQUEST_INTERNAL_CATEGORY)
                .Select(a => a.Category).Distinct().OrderBy(a => a).ToListAsync();

            return AddToCache(LibConstants.POST_CATEGORY_CACHE_KEY, dbResult);
        }
    }
}