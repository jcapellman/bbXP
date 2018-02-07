using System.Collections.Generic;
using System.Threading.Tasks;
using bbxp.lib.Common;
using bbxp.lib.Settings;
using bbxp.lib.Transports.Posts;

namespace bbxp.lib.Handlers {
    public class PostHandler : BaseHandler {
        public PostHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "Posts";

        public async Task<ReturnSet<List<PostResponseItem>>> GetMainListing() => await GetAsync<ReturnSet<List<PostResponseItem>>>();

        public async Task<ReturnSet<PostResponseItem>> GetSinglePost(string urlArg) => await GetAsync<ReturnSet<PostResponseItem>>($"/{urlArg}");
    }
}