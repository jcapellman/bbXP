using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.web.blazor.Server.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postcategories")]
    public class PostCategoriesController : BaseController
    {
        public PostCategoriesController(bbxpDbContext dbContext, IMemoryCache memoryCache) : base(dbContext, memoryCache) { }

        [HttpGet]
        [Route("{category}/{postCount}")]
        public async Task<List<Posts>> GetAsync([FromRoute] string category, [FromRoute] int postCount) => await GetPostsAsync(postCount, category);
    }
}
