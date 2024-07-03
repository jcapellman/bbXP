using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;

using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController(BbxpContext dbContext, IMemoryCache memoryCache, ILogger<PostsController> logger) : BaseController(memoryCache)
    {
        private static string CreateUrlSafeTitle(string title) =>
            title.ToLower().Replace(' ', '_').Replace(".", "").Replace(",", "").Replace("-", "_");

        [HttpGet]
        [Route("{category}/{postCount}/")]
        public async Task<List<Posts>> GetPostsAsync([FromRoute] string category, [FromRoute] int postCount)
        {
            try
            {
                var cacheResult = GetFromCache<List<Posts>>(category);

                if (cacheResult is not null)
                {
                    return cacheResult;
                }

                List<Posts> dbResult = [];

                dbResult = category switch
                {
                    AppConstants.POST_REQUEST_DEFAULT_CATEGORY =>
                         await dbContext.Posts.Where(a => a.Active).OrderByDescending(a => a.PostDate).Take(postCount).ToListAsync(),
                    _ =>
                        await dbContext.Posts.Where(a => a.Active && a.Category == category).OrderByDescending(a => a.PostDate).ToListAsync(),
                };

                return AddToCache(category, dbResult);
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
                var cacheResult = GetFromCache<Posts>(url);

                if (cacheResult is not null)
                {
                    return cacheResult;
                }

                var dbResult = dbContext.Posts.FirstOrDefault(a => a.Active && a.URL == url);

                if (dbResult is null)
                {
                    logger.LogDebug("Post ({url}) was not found", url);

                    return NotFound($"Post ({url}) was not found");
                }

                return AddToCache(url, dbResult);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Get Post due to {ex}", ex);

                throw;
            }
        }

        [Authorize]
        [HttpPatch]
        public async Task<bool> UpdateAsync(PostUpdateRequestItem updatePost)
        {
            try
            {
                var post = dbContext.Posts.FirstOrDefault(a => a.Id == updatePost.Id);

                if (post is null)
                {
                    return false;
                }

                post.Title = updatePost.Title;
                post.Body = updatePost.Body;
                post.Category = updatePost.Category;
                post.PostDate = updatePost.PostDate;
                post.URL = updatePost.URL;

                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Update Post due to {ex}", ex);

                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> AddAsync(PostCreationRequestItem newPost)
        {
            try
            {
                var post = new Posts
                {
                    PostDate = newPost.PostDate ?? DateTime.Now,
                    Body = newPost.Body,
                    Title = newPost.Title,
                    Category = newPost.Category,
                    URL = CreateUrlSafeTitle(newPost.Title)
                };

                dbContext.Posts.Add(post);

                return await dbContext.SaveChangesAsync() > 0;
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
                var post = await dbContext.Posts.FirstAsync(a => a.Id == postId);

                if (post == null)
                {
                    return false;
                }

                post.Active = false;

                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Delete Post due to {ex}", ex);

                throw;
            }
        }
    }
}