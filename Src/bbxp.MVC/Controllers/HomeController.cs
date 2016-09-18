using System;
using System.Threading.Tasks;

using bbxp.CommonLibrary.Handlers;
using bbxp.CommonLibrary.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class HomeController : BaseController {
        public HomeController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public async Task<IActionResult> Index() {
            var result = await new PostHandler(MANAGER_CONTAINER.GSetings).GetMainListing();

            if (result.HasError) {
                throw new Exception(result.ExceptionMessage);
            }

            return View(result.ReturnValue);
        }

        [Route("tag/{urlSafeTagName}")]
        public async Task<IActionResult> TagResult(string urlSafeTagName)
            => View("Index", await new PostTagHandler(MANAGER_CONTAINER.GSetings).GetPostsFromTag(urlSafeTagName));
        
        [Route("{year}/{month}/{day}/{postURL}")]
        public async Task<IActionResult> SinglePost(int year, int month, int day, string postURL) {
            var post = await new PostHandler(MANAGER_CONTAINER.GSetings).GetSinglePost($"{year}/{month}/{day}/{postURL}");

            if (post.HasError) {
                throw new Exception(post.ExceptionMessage);
            }

            ViewData["Title"] = post.ReturnValue.Title;

            return View("_PostPartial", post.ReturnValue);
        }
    }
}