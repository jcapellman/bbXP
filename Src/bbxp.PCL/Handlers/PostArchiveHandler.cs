using System.Collections.Generic;
using System.Threading.Tasks;
using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.PostArchive;
using bbxp.PCL.Transports.Posts;

namespace bbxp.PCL.Handlers {
    public class PostArchiveHandler : BaseHandler {
        public PostArchiveHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PostArchive";

        public async Task<ReturnSet<List<PostArchiveListingResponseItem>>> GetArchiveList()
            => await GetAsync<ReturnSet<List<PostArchiveListingResponseItem>>>();

        public async Task<ReturnSet<List<PostResponseItem>>> GetPostsFromMonth(int year, int month) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"/{year}/{month}");
    }
}