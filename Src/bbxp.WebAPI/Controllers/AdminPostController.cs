using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Settings;
using bbxp.lib.Transports.AdminPost;

using bbxp.WebAPI.BusinessLayer.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.WebAPI.Controllers {
    public class AdminPostController : BaseController {
        [HttpPost]
        public async Task<ReturnSet<bool>> POST(AdminPostRequestItem requestItem) {
            if (requestItem.PostID.HasValue) {
                return await new AdminPostManager(MANAGER_CONTAINER).UpdatePost(requestItem);
            }

            return await new AdminPostManager(MANAGER_CONTAINER).CreatePost(requestItem);
        }

        public AdminPostController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
    }
}