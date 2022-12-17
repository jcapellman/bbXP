using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.web.blazor.Server.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : BaseController
    {
        public PostsController(bbxpDbContext dbContext, IMemoryCache memoryCache) : base(dbContext, memoryCache) { }

        [HttpGet]
        public async Task<IEnumerable<Posts>> GetPostsAsync() => await GetManyAsync<Posts>(a => a.Active);
    }
}