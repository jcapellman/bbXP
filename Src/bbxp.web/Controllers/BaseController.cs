﻿using bbxp.lib.Containers;
using bbxp.lib.DAL;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.Controllers
{
    public class BaseController : Controller
    {
        private readonly GlobalSettings _globalSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly BbxpDbContext _dbContext;

        protected ManagerContainer ManagerContainer => new ManagerContainer { GSetings = _globalSettings, Cache = _memoryCache, DBContext = _dbContext };

        public BaseController(GlobalSettings globalSettings, IMemoryCache cache, BbxpDbContext dbContext)
        {
            _globalSettings = globalSettings;
            _memoryCache = cache;
            _dbContext = dbContext;
        }

        public ActionResult RedirectToError(string exceptionMessage) => RedirectToAction("Error", exceptionMessage);
    }
}