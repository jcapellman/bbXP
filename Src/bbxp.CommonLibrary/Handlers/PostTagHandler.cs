using System.Collections.Generic;
using System.Threading.Tasks;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Posts;

namespace bbxp.CommonLibrary.Handlers {
    public class PostTagHandler : BaseHandler {
        public PostTagHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PostTag";

        public async Task<ReturnSet<List<PostResponseItem>>> GetPostsFromTag(string tag) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"tag={tag}");
    }
}