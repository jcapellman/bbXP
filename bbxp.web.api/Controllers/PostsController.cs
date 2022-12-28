using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;

using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : BaseController
    {
        public PostsController(bbxpDbContext dbContext, IMemoryCache memoryCache) : base(dbContext, memoryCache) { }

        [HttpGet]
        [Route("{category}/{postCount}/")]
        public async Task<List<Posts>> GetPostsAsync([FromRoute] string category, [FromRoute] int postCount) => await GetPostsAsync(postCount, category);
        
        [HttpGet]
        [Route("{url}")]
        public async Task<Posts?> GetSinglePostAsync([FromRoute] string url) => await GetPostAsync(url);

        [HttpPatch]
        public async Task<bool> UpdateAsync(PostUpdateRequestItem post) => await UpdatePostAsync(post);

        [HttpPost]
        public async Task<bool> AddAsync(PostCreationRequestItem post) => await AddPostAsync(post);

        [HttpDelete]
        public async Task<bool> DeleteAsync(int postId) => await DeletePostAsync(postId);
    }
}