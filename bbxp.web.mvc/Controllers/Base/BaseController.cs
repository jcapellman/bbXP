using bbxp.lib.Configuration;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.mvc.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly AppConfiguration _appConfiguration;

        public BaseController(IOptions<AppConfiguration> appConfiguration)
        {
            _appConfiguration = appConfiguration.Value;
        }
    }
}