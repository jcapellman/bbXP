using System.Collections.Generic;
using System.Threading.Tasks;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.PostArchive;
using bbxp.CommonLibrary.Transports.Posts;

namespace bbxp.CommonLibrary.Handlers {
    public class PostArchiveHandler : BaseHandler {
        public PostArchiveHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PostArchive";

        public async Task<ReturnSet<List<PostArchiveListingResponseItem>>> GetArchiveList()
            => await GetAsync<ReturnSet<List<PostArchiveListingResponseItem>>>();

        public async Task<ReturnSet<List<PostResponseItem>>> GetPostsFromMonth(int year, int month) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"year={year}&month={month}");
    }
}