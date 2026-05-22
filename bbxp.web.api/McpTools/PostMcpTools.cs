using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;

using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;

using System.ComponentModel;

namespace bbxp.web.api.McpTools
{
    [McpServerToolType]
    public class PostMcpTools(BbxpContext dbContext)
    {
        [McpServerTool(ReadOnly = true, Idempotent = true), Description("Gets a list of blog posts by category. Use the default category to get the most recent posts.")]
        public async Task<List<Posts>> GetPostsAsync(
            [Description("The category name, or 'default' for the main feed.")] string category,
            [Description("The maximum number of posts to return.")] int postCount = 10)
        {
            return category switch
            {
                LibConstants.POST_REQUEST_DEFAULT_CATEGORY =>
                    await BbxpContext.GetActivePostsAsync(dbContext, postCount).ToListAsync(),
                _ =>
                    await BbxpContext.GetPostsByCategoryAsync(dbContext, category).ToListAsync(),
            };
        }

        [McpServerTool(ReadOnly = true, Idempotent = true), Description("Gets a single blog post by its URL-safe name.")]
        public async Task<Posts?> GetPostAsync(
            [Description("The URL-safe name of the post.")] string url)
        {
            return await BbxpContext.GetPostByUrlAsync(dbContext, url);
        }

        [McpServerTool(ReadOnly = true, Idempotent = true), Description("Searches blog posts by title keyword.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1862:Use the 'StringComparison' method overloads to perform case-insensitive string comparisons", Justification = "Not supported in Cloud SQL")]
        public async Task<List<Posts>> SearchPostsAsync(
            [Description("The search query to match against post titles.")] string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            return await dbContext.Posts.AsNoTracking()
                .Where(a => a.Title.ToLower().Contains(searchQuery))
                .ToListAsync();
        }

        [McpServerTool(ReadOnly = true, Idempotent = true), Description("Gets all available post categories.")]
        public async Task<List<string>> GetPostCategoriesAsync()
        {
            return await dbContext.Posts
                .Where(a => a.Active &&
                            a.Category != LibConstants.POST_REQUEST_DEFAULT_CATEGORY &&
                            a.Category != LibConstants.POST_REQUEST_INTERNAL_CATEGORY)
                .Select(a => a.Category)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
        }
    }
}
