using System.Runtime.Serialization;

namespace bbXP.pcl.Transports.Tags {
	[DataContract]
	public class TagResponseItem {
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string SafeName { get; set; }

		public TagResponseItem(string name, string safeName) {
			Name = name;
			SafeName = safeName.Trim();
		}
	}
}