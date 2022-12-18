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
        public IEnumerable<Posts> Get() => GetPosts(a => a.Active).OrderByDescending(a => a.PostDate);

        [HttpPatch]
        public async Task<bool> UpdateAsync(PostUpdateRequestItem post) => await UpdatePostAsync(post);

        [HttpPost]
        public async Task<bool> AddAsync(PostCreationRequestItem post) => await AddPostAsync(post);

        [HttpDelete]
        public async Task<bool> DeleteAsync(int postId) => await DeletePostAsync(postId);
    }
}