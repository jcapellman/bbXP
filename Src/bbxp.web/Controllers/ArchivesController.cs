﻿using System;

using bbxp.lib.Settings;

using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class ArchivesController : BaseController {
        public ArchivesController(IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache) { }

        public IActionResult Index() {
            var archiveList = new PostArchiveManager(ManagerContainer).GetArchives();

            return archiveList.HasError ? RedirectToError(archiveList.ExceptionMessage) : View(archiveList.ReturnValue);
        }

        [Route("/archives/{year}/{month}")]
        public PartialViewResult ArchivePosts(int year, string month) {
            var monthInt = Convert.ToDateTime(month + " 01, 1900").Month;

            var posts = new PostManager(ManagerContainer).GetMonthPosts(year, monthInt);

            if (posts.HasError) {
                throw new Exception(posts.ExceptionMessage);
            }

            ViewData["Title"] = $"{month} {year}";

            return PartialView("_ArchivedPosts", posts.ReturnValue);
        }
    }
}