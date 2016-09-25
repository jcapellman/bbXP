using System.Collections.Generic;
using System.Threading.Tasks;

using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.Posts;

namespace bbxp.PCL.Handlers {
    public class SearchHandler : BaseHandler {
        public SearchHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "Search";

        public async Task<ReturnSet<List<PostResponseItem>>> SearchPosts(string query) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"/{query}");
    }
}