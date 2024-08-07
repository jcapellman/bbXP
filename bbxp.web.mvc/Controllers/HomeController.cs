﻿using bbxp.lib.Common;
using bbxp.lib.Configuration;
using bbxp.lib.HttpHandlers;
using bbxp.web.mvc.Controllers.Base;
using bbxp.web.mvc.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace bbxp.web.mvc.Controllers
{
    public class HomeController(IOptions<AppConfiguration> appConfiguration) : BaseController(appConfiguration)
    {
        [Route("{category}/{postCountLimit}/")]
        [ResponseCache(VaryByQueryKeys = ["*"], Duration = int.MaxValue)]
        public async Task<IActionResult> Index(string category, int postCountLimit) => await GetPostsAsync(category, postCountLimit);

        [Route("postsearch/")]
        [Route("postsearch/{searchQuery}")]
        public async Task<IActionResult> PostSearchAsync(string? searchQuery = null)
        {
            var model = new SearchViewModel(_appConfiguration);

            if (searchQuery is null)
            {
                ViewData["Title"] = "Search";

                model.SearchQuery = string.Empty;

                return View("PostSearch", model);
            }

            var postHttpHandler = new PostHttpHandler(_appConfiguration.APIUrl);

            var posts = await postHttpHandler.SearchPostsAsync(searchQuery);

            model.Posts = posts is not null ? posts.Select(a => new PostViewModel(_appConfiguration) { Post = a }).ToList() : [];
            model.SearchQuery = searchQuery;

            ViewData["Title"] = searchQuery;

            return View("PostSearch", model);
        }

        public async Task<IActionResult> Index() => await GetPostsAsync();

        private async Task<IActionResult> GetPostsAsync(string category = LibConstants.POST_REQUEST_DEFAULT_CATEGORY, int postCountLimit = LibConstants.POST_REQUEST_DEFAULT_LIMIT)
        {
            var postHttpHandler = new PostHttpHandler(_appConfiguration.APIUrl);

            var posts = await postHttpHandler.GetPostsAsync(category, postCountLimit);

            var categories = await postHttpHandler.GetPostCategoriesAsync();

            var model = new IndexModel(_appConfiguration)
            {
                Posts = posts is not null ? posts.Select(a => new PostViewModel(_appConfiguration) { Post = a}).ToList() : [],
                Categories = categories is not null ? categories : []
            };

            ViewData["Title"] = category;

            return View(model);
        }

        /// <summary>
        /// Used for legacy handling only
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="postURL"></param>
        /// <returns></returns>
        [Route("{year}/{month}/{day}/{postURL}")]
        [ResponseCache(VaryByQueryKeys = ["*"], Duration = int.MaxValue)]
        public async Task<IActionResult> GetSinglePostLegacyAsync(int year, int month, int day, string postURL)
            => await GetSinglePostAsync(postURL, year, month, day);

        [Route("{postURL}")]
        [ResponseCache(VaryByQueryKeys = ["*"], Duration = int.MaxValue)]
        public async Task<IActionResult> GetSinglePostAsync(string postURL, int year, int month, int day)
        {
            var postHttpHandler = new PostHttpHandler(_appConfiguration.APIUrl);

            var post = await postHttpHandler.GetSinglePostAsync(postURL);

            if (post == null)
            {
                return NotFound();
            }

            ViewData["Title"] = post.Title;

            return View("_Post", new PostViewModel(_appConfiguration) { Post = post });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}