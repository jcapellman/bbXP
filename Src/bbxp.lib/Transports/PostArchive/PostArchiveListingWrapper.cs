using bbxp.lib.Transports.Posts;
using System.Collections.Generic;

namespace bbxp.lib.Transports.PostArchive
{
    public class PostArchiveListingWrapper
    {
        public string DateString { get; set; }

        public string RelativeURL { get; set; }

        public int Count { get; set; }

        public List<PostResponseItem> Posts { get; set; }

        public PostResponseItem SelectedPost { get; set; }
    }
}