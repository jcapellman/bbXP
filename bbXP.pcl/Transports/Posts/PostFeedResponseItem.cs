using System;
using System.Runtime.Serialization;

namespace bbXP.pcl.Transports.Posts {
	[DataContract]
	public class PostFeedResponseItem {
		[DataMember]
		public DateTime Published { get; set; }

		[DataMember]
		public string Title { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public string URL { get; set; }

		public PostFeedResponseItem(DateTime published, string title, string description, string url) {
			Published = published;
			Title = title;
			Description = description;
			URL = url;
		}
	}
}