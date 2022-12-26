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
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOptions<AppConfiguration> appConfiguration) : base(appConfiguration)
        {
            _logger = logger;
        }

        [Route("/{category}/{postCountLimit}/")]
        public async Task<IActionResult> Index(string category = AppConstants.POST_REQUEST_DEFAULT_CATEGORY, int postCountLimit = AppConstants.POST_REQUEST_DEFAULT_LIMIT)
        {
            var postHttpHandler = new PostHttpHandler(_appConfiguration.APIUrl);

            var posts = await postHttpHandler.GetPostsAsync(category, postCountLimit);

            var categories = await postHttpHandler.GetPostCategoriesAsync();

            var model = new IndexModel(_appConfiguration)
            {
                Posts = posts.Select(a => new PostViewModel(_appConfiguration) { Post = a}).ToList(),
                Categories = categories
            };

            return View(model);
        }

        [Route("{postURL}")]
        public async Task<IActionResult> GetSinglePostAsync(string postURL)
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