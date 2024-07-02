using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;

using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController(BbxpContext dbContext, IMemoryCache memoryCache, ILogger<PostsController> logger) : BaseController(dbContext, memoryCache)
    {
        [HttpGet]
        [Route("{category}/{postCount}/")]
        public IEnumerable<Posts> GetPostsAsync([FromRoute] string category, [FromRoute] int postCount)
        {
            try
            {
                return GetPostsAsync(postCount, category);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Get Posts due to {ex}", ex);

                throw;
            }
        }

        [HttpGet]
        [Route("{url}")]
        public ActionResult<Posts?> GetSinglePostAsync([FromRoute] string url)
        {
            try
            {
                var post = GetPostAsync(url);

                if (post == null)
                {
                    logger.LogDebug("Post ({url}) was not found", url);

                    return NotFound($"Post ({url}) was not found");
                }

                return post;
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Get Post due to {ex}", ex);

                throw;
            }
        }

        [Authorize]
        [HttpPatch]
        public async Task<bool> UpdateAsync(PostUpdateRequestItem post)
        {
            try
            {
                return await UpdatePostAsync(post);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Update Post due to {ex}", ex);

                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> AddAsync(PostCreationRequestItem post)
        {
            try
            {
                return await AddPostAsync(post);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Create Post due to {ex}", ex);

                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<bool> DeleteAsync(int postId)
        {
            try
            {
                return await DeletePostAsync(postId);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Delete Post due to {ex}", ex);

                throw;
            }
        }
    }
}