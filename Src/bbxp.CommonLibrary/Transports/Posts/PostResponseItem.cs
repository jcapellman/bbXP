using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace bbxp.CommonLibrary.Transports.Posts {        
    public class PostResponseItem {        
        [JsonProperty("Title")]
        public string Title { get; set; }
        
        [JsonProperty("Body")]
        public string Body { get; set; }

        [JsonProperty("Tags")]
        public List<TagResponseItem> Tags { get; set; }

        [JsonProperty("PostDate")]
        public DateTime PostDate { get; set; } 
    }
}