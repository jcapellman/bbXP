using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postcategories")]
    public class PostCategoriesController(BbxpContext dbContext, IMemoryCache memoryCache) : BaseController(dbContext, memoryCache)
    {
        [HttpGet]
        public async Task<List<string>> GetPostCategoriesAsync() => await GetCategoriesAsync();
    }
}