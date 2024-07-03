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
            var cacheResult = GetFromCache<List<string>>(AppConstants.POST_REQUEST_DEFAULT_CATEGORY);

            if (cacheResult is not null)
            {
                return cacheResult;
            }

            var dbResult = await dbContext.Posts.Where(a => a.Active &&
                                                         a.Category != AppConstants.POST_REQUEST_DEFAULT_CATEGORY &&
                                                         a.Category != AppConstants.POST_REQUEST_INTERNAL_CATEGORY)
                .Select(a => a.Category).Distinct().OrderBy(a => a).ToListAsync();

            return AddToCache(AppConstants.POST_REQUEST_DEFAULT_CATEGORY, dbResult);
        }
    }
}