using System.Collections.Generic;
using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Settings;
using bbxp.lib.Transports.Posts;

namespace bbxp.lib.Handlers {
    public class SearchHandler : BaseHandler {
        public SearchHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "Search";

        public async Task<ReturnSet<List<PostResponseItem>>> SearchPosts(string query) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"/{query}");
    }
}