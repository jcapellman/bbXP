using System.Threading.Tasks;

using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;
using bbxp.lib.Transports.AdminPost;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        public AdminController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        public ActionResult Index()
        {
            var posts = new AdminPostManager(ManagerContainer).GetPosts();

            return posts.HasError ? RedirectToError(posts.ExceptionMessage) : View(posts.ReturnValue);
        }

        public ActionResult EditPost(int id)
        {
            var post = new AdminPostManager(ManagerContainer).GetPost(id);

            return post.HasError ? RedirectToError(post.ExceptionMessage) : View(post.ReturnValue);
        }

        public ActionResult NewPost() => View();

        [HttpPost]
        public async Task<ActionResult> CreatePost(AdminPostRequestItem model)
        {
            var result = await new AdminPostManager(ManagerContainer).CreatePostAsync(model);

            return result.HasError ? RedirectToError(result.ExceptionMessage) : RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePost(AdminPostResponseItem model)
        {
            var result = await new AdminPostManager(ManagerContainer).UpdatePostAsync(new AdminPostRequestItem
            {
                Title = model.Title,
                Body = model.Body,
                PostID = model.ID,
                TagList = model.Tags
            });

            return result.HasError ? RedirectToError(result.ExceptionMessage) : RedirectToAction("Index", "Admin");
        }
    }
}