using System.Threading.Tasks;

using bbxp.lib.Settings;

using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class HomeController : BaseController {
        public HomeController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public async Task<IActionResult> Index() {
            var result = await new PostManager(MANAGER_CONTAINER).GetHomeListingAsync();

            if (result.HasError) {
                return RedirectToError(result.ExceptionMessage);
            }

            return View(result.ReturnValue);
        }

        [Route("tag/{urlSafeTagName}")]
        public IActionResult TagResult(string urlSafeTagName)
            => View("Index", new PostManager(MANAGER_CONTAINER).GetPostsFromTag(urlSafeTagName));
        
        [Route("{year}/{month}/{day}/{postURL}")]
        public async Task<IActionResult> SinglePost(int year, int month, int day, string postURL) {
            var post = await new PostManager(MANAGER_CONTAINER).GetSinglePostAsync($"{year}/{month}/{day}/{postURL}");

            if (post.HasError)
            {
                return RedirectToError(post.ExceptionMessage);
            }

            ViewData["Title"] = post.ReturnValue.Title;

            return View("_PostPartial", post.ReturnValue);
        }
    }
}