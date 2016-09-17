using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using bbxp.MVC.Managers;
using bbxp.MVC.Settings;

namespace bbxp.MVC.Controllers {
    public class ArchivesController : BaseController {
        public ArchivesController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index() => View(new ArchivesManager(MANAGER_CONTAINER).GetArchives());
        
        [Route("/archives/{year}/{month}")]
        public PartialViewResult ArchivePosts(int year, string month) {
            var monthInt = Convert.ToDateTime(month + " 01, 1900").Month;

            var posts = new PostManager(MANAGER_CONTAINER).GetMonthPosts(year, monthInt);

            ViewData["Title"] = $"{month} {year}";

            return PartialView("_ArchivedPosts", posts);
        }
    }
}