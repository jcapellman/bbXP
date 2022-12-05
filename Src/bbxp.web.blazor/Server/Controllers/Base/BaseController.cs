using bbxp.lib.Containers;
using bbxp.lib.DAL;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.blazor.Server.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly GlobalSettings _globalSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly BbxpDbContext _dbContext;

        protected ManagerContainer MContainer => new ManagerContainer { GSetings = _globalSettings, Cache = _memoryCache, DBContext = _dbContext };

        public BaseController(GlobalSettings globalSettings, IMemoryCache cache, BbxpDbContext dbContext)
        {
            _globalSettings = globalSettings;
            _memoryCache = cache;
            _dbContext = dbContext;
        }
    }
}