using System;
using System.Threading.Tasks;

using bbxp.lib.Settings;
using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class ArchivesController : BaseController {
        public ArchivesController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index() {
            var archiveList = new PostArchiveManager(MANAGER_CONTAINER).GetArchives();

            if (archiveList.HasError) {
                throw new Exception(archiveList.ExceptionMessage);
            }

            return View(archiveList.ReturnValue);
        }

        [Route("/archives/{year}/{month}")]
        public async Task<PartialViewResult> ArchivePosts(int year, string month) {
            var monthInt = Convert.ToDateTime(month + " 01, 1900").Month;

            var posts = await new PostManager(MANAGER_CONTAINER).GetMonthPostsAsync(year, monthInt);

            if (posts.HasError) {
                throw new Exception(posts.ExceptionMessage);
            }

            ViewData["Title"] = $"{month} {year}";

            return PartialView("_ArchivedPosts", posts.ReturnValue);
        }
    }
}