using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;
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
        public async Task<List<string>> GetAsync() => await GetPostCategoriesAsync();
    }
}
