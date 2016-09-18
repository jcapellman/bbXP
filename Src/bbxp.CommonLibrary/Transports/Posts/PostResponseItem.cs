using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace bbxp.CommonLibrary.Transports.Posts {    
    [DataContract]
    public class PostResponseItem {        
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public string Body { get; set; }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Auto, TypeNameHandling = TypeNameHandling.Auto)]
        public List<TagResponseItem> Tags { get; set; }

        [DataMember]
        public DateTime PostDate { get; set; } 
    }
}