using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;

using bbxp.web.blazor.Server.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.blazor.Server.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : BaseController
    {
        public PostsController(bbxpDbContext dbContext, IMemoryCache memoryCache) : base(dbContext, memoryCache) { }

        [HttpGet]
        public IEnumerable<Posts> Get() => GetPosts(a => a.Active);

        [HttpPost]
        public async Task<bool> AddAsync(PostCreationRequestItem post) => await AddPostAsync(post);
    }
}