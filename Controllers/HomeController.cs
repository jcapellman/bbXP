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
        
        [Route("{urlSafeName}")]
        public IActionResult Content(string urlSafeName) => View("Content", new ContentManager(MANAGER_CONTAINER).GetContent(urlSafeName));

        [Route("archives")]
        public IActionResult Archives() => View(new ArchivesManager(MANAGER_CONTAINER).GetArchives());

        [Route("{year}/{month}/")]
        public PartialViewResult ArchivePosts(int year, string month) {
            var monthInt = Convert.ToDateTime(month + " 01, 1900").Month;

            var posts = new PostManager(MANAGER_CONTAINER).GetMonthPosts(year, monthInt);

            ViewData["Title"] = $"{month} {year}";

            return PartialView(posts);
        }

        [Route("{year}/{month}/{day}/{postURL}")]
        public IActionResult SinglePost(int year, int month, int day, string postURL) {
            var post = new PostManager(MANAGER_CONTAINER).GetSinglePost($"{year}/{month}/{day}/{postURL}");
            
            ViewData["Title"] = post.Title;

            return View("PostPartial", post);
        }
    }
}