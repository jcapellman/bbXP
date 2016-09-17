using System.Collections.Generic;
using System.Threading.Tasks;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Posts;

namespace bbxp.CommonLibrary.Handlers {
    public class SearchHandler : BaseHandler {
        public SearchHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "Search";

        public async Task<ReturnSet<List<PostResponseItem>>> SearchPosts(string query) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"query={query}");
    }
}