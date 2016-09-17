using System;
using bbxp.MVC.Managers;
using bbxp.MVC.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class HomeController : BaseController {
        public HomeController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index() => View(new PostManager(MANAGER_CONTAINER).GetHomeListing());
        
        [Route("tag/{urlSafeTagName}")]
        public IActionResult TagResult(string urlSafeTagName) => View("Index", new PostManager(MANAGER_CONTAINER).GetPostsFromTag(urlSafeTagName));        
        
        [Route("{year}/{month}/{day}/{postURL}")]
        public IActionResult SinglePost(int year, int month, int day, string postURL) {
            var post = new PostManager(MANAGER_CONTAINER).GetSinglePost($"{year}/{month}/{day}/{postURL}");
            
            ViewData["Title"] = post.Title;

            return View("_PostPartial", post);
        }
    }
}