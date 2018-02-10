using bbxp.lib.Settings;

using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class HomeController : BaseController {
        public HomeController(IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache) { }

        public IActionResult Index() {
            var result = new PostManager(ManagerContainer).GetHomeListing();

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue);
        }

        [Route("tag/{urlSafeTagName}")]
        public IActionResult TagResult(string urlSafeTagName)
        {
            var result = new PostManager(ManagerContainer).GetPostsFromTag(urlSafeTagName);

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View("Index", result.ReturnValue);
        }

        [Route("{year}/{month}/{day}/{postURL}")]
        public IActionResult SinglePost(int year, int month, int day, string postUrl) {
            var post = new PostManager(ManagerContainer).GetSinglePost($"{year}/{month}/{day}/{postUrl}");

            if (post.HasError)
            {
                return RedirectToError(post.ExceptionMessage);
            }

            ViewData["Title"] = post.ReturnValue.Title;

            return View("_PostPartial", post.ReturnValue);
        }
    }
}