using bbxp.lib.Settings;

using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class HomeController : BaseController {
        public HomeController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index() {
            var result = new PostManager(MANAGER_CONTAINER).GetHomeListing();

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue);
        }

        [Route("tag/{urlSafeTagName}")]
        public IActionResult TagResult(string urlSafeTagName)
        {
            var result = new PostManager(MANAGER_CONTAINER).GetPostsFromTag(urlSafeTagName);

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View("Index", result.ReturnValue);
        }

        [Route("{year}/{month}/{day}/{postURL}")]
        public IActionResult SinglePost(int year, int month, int day, string postUrl) {
            var post = new PostManager(MANAGER_CONTAINER).GetSinglePost($"{year}/{month}/{day}/{postUrl}");

            if (post.HasError)
            {
                return RedirectToError(post.ExceptionMessage);
            }

            ViewData["Title"] = post.ReturnValue.Title;

            return View("_PostPartial", post.ReturnValue);
        }
    }
}