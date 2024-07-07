using bbxp.lib.Database.Tables;
using bbxp.lib.JSON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using bbxp.lib.Common;
using bbxp.lib.Database;

using bbxp.web.api.Controllers.Base;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/post-admin")]
    public class PostAdminController(BbxpContext dbContext, IMemoryCache memoryCache, ILogger<PostAdminController> logger) : BaseController(memoryCache)
    {
        /// <summary>
        /// Allows the updating of an existing post
        /// </summary>
        /// <param name="updatePost"></param>
        /// <returns></returns>
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
                    URL = newPost.Title.ToUrlSafeTitle()
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

        /// <summary>
        /// Returns all of the posts greater than the date passed in
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Posts>> GetPostsFromDateAsync(DateTime date) => await dbContext.Posts.Where(a => a.Modified > date).ToListAsync();
    }
}