using System.Collections.Generic;
using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Settings;
using bbxp.lib.Transports.PostArchive;
using bbxp.lib.Transports.Posts;

namespace bbxp.lib.Handlers {
    public class PostArchiveHandler : BaseHandler {
        public PostArchiveHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PostArchive";

        public async Task<ReturnSet<List<PostArchiveListingResponseItem>>> GetArchiveList()
            => await GetAsync<ReturnSet<List<PostArchiveListingResponseItem>>>();

        public async Task<ReturnSet<List<PostResponseItem>>> GetPostsFromMonth(int year, int month) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"/{year}/{month}");
    }
}