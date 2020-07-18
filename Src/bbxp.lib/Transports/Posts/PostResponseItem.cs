using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bbxp.lib.Transports.Posts
{
    public class PostResponseItem
    {
        public PostResponseItem() { }

        public PostResponseItem(PostResponseItem post)
        {
            Title = post.Title;
            RelativeURL = post.RelativeURL;
            Body = post.Body;
            Tags = post.Tags;
            PostDate = post.PostDate;
        }

        public string ToUpper(string str) => str.ToUpper();

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("RelativeURL")]
        public string RelativeURL { get; set; }

        [JsonProperty("Body")]
        public string Body { get; set; }

        [JsonProperty("Tags")]
        public List<TagResponseItem> Tags { get; set; }

        [JsonProperty("PostDate")]
        public DateTime PostDate { get; set; }
    }
}