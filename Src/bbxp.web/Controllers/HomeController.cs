using System.Linq;

using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;
using bbxp.web.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class HomeController : BaseController {
        public HomeController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        public IActionResult Index() {
            var result = new PostManager(ManagerContainer).GetHomeListing();

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue.Select(a => new PostModel { IsSinglePost = false, Post = a}).ToList());
        }

        [Route("tag/{urlSafeTagName}")]
        public IActionResult TagResult(string urlSafeTagName)
        {
            var result = new PostManager(ManagerContainer).GetPostsFromTag(urlSafeTagName);

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View("Index", result.ReturnValue.Select(a => new PostModel { IsSinglePost = false, Post = a}).ToList());
        }

        [Route("{postURL}")]
        public IActionResult SinglePost(string postURL) {
            var post = new PostManager(ManagerContainer).GetSinglePost(postURL);

            if (post.HasError)
            {
                // Handle older routes cached in Search Engines
                if (!postURL.Contains("/"))
                {
                    return RedirectToError(post.ExceptionMessage);
                }

                postURL = postURL.Split('/')[2];

                return RedirectPermanent(postURL);
            }

            ViewData["Title"] = post.ReturnValue.Title;

            return View("_PostPartial", new PostModel {
                IsSinglePost = true,
                Post = post.ReturnValue
            });
        }
    }
}