using System.Runtime.Serialization;

namespace bbXP.pcl.Transports.Tags {
	[DataContract]
	public class TagCloudResponseItem {
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string URLSafeName { get; set; }

		[DataMember]
		public int Count { get; set; }

		[DataMember]
		public string CSSClassName { get; set; }

		public TagCloudResponseItem(string name, string urlSafeName, int count) {
			Name = name;
			Count = count;
			URLSafeName = urlSafeName;
		}
	}
}