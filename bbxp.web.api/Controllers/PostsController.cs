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
        private readonly ILogger<PostsController> _logger;

        public PostsController(bbxpDbContext dbContext, IMemoryCache memoryCache, ILogger<PostsController> logger) : base(dbContext, memoryCache) {
            _logger = logger;
        }

        [HttpGet]
        [Route("{category}/{postCount}/")]
        public async Task<List<Posts>> GetPostsAsync([FromRoute] string category, [FromRoute] int postCount)
        {
            try
            {
                return await GetPostsAsync(postCount, category);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Get Posts due to {ex}", ex);

                throw;
            }
        }

        [HttpGet]
        [Route("{url}")]
        public async Task<ActionResult<Posts?>> GetSinglePostAsync([FromRoute] string url)
        {
            try
            {
                var post = await GetPostAsync(url);

                if (post == null)
                {
                    _logger.LogDebug("Post ({url}) was not found", url);

                    return NotFound($"Post ({url}) was not found");
                }

                return post;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Get Post due to {ex}", ex);

                throw;
            }
        }

        [HttpPatch]
        public async Task<bool> UpdateAsync(PostUpdateRequestItem post)
        {
            try
            {
                return await UpdatePostAsync(post);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Update Post due to {ex}", ex);

                throw;
            }
        }

        [HttpPost]
        public async Task<bool> AddAsync(PostCreationRequestItem post)
        {
            try
            {
                return await AddPostAsync(post);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Create Post due to {ex}", ex);

                throw;
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(int postId)
        {
            try
            {
                return await DeletePostAsync(postId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Delete Post due to {ex}", ex);

                throw;
            }
        }
    }
}