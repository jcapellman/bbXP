using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

using bbXP.pcl.Helpers;
using bbXP.pcl.Transports.Tags;

namespace bbXP.pcl.Transports.Posts {
	[DataContract]
	public partial class PostResponseItem {
		[DataMember]		
		public int ID { get; set; }

		[DataMember]
		public DateTime Created { get; set; }

		[DataMember]
		public DateTime Modified { get; set; }

		[DataMember]		
		public string Title { get; set; }

		[DataMember]
		public string Body { get; set; }

		[DataMember]
		public string PostBy { get; set; }

		[DataMember]
		public string URLSafename { get; set; }


		[DataMember]
		public List<TagResponseItem> Tags { get; set; }

		public PostResponseItem(int id, DateTime postDate, string title, string body, string tags, string safeTags, string urlsafename, bool processPost = true) {
			ID = id;
			Created = postDate;
			Title = title;
			Body = (processPost ? parsePost(body) : body);
			URLSafename = urlsafename;

			if (String.IsNullOrEmpty(tags)) {
				Tags = new List<TagResponseItem>();
			} else {
				var safeTagsArray = safeTags.Split(',');
				var tagsArray = tags.Split(',');

				Tags = new List<TagResponseItem>();

				for (int x = 0; x < tagsArray.Length; x++) {
					Tags.Add(new TagResponseItem(tagsArray[x], safeTagsArray[x]));
				}
			}
		}

		private string parsePost(string content) {
			var matches = Regex.Matches(content, @"\[(.*[a-z])\]");

			foreach (Match match in matches) {
				var syntaxTag = new SyntaxHelper(match.Value);

				if (syntaxTag.FullTagName.StartsWith("[caption")) {
					content = content.Replace(syntaxTag.FullTagName, "<div style=\"text-align: center\">" + syntaxTag.Value.Replace("</a>", "</a><br/>") + "</div><br/>");
					continue;
				}

				if (!syntaxTag.IsParseable) {
					continue;
				}

				if (syntaxTag.IsClosingTag) {
					content = content.Replace(syntaxTag.FullTagName, "</pre>");
				} else {
					content = content.Replace(syntaxTag.FullTagName, "<pre class=\"brush: " + syntaxTag.NameOnly + ";\">");
				}
			}

			foreach (Match match in Regex.Matches(content, @"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>")) {
				if (!match.Value.Contains("<img")) {
					continue;
				}

				var tmp = match.Value.Replace("<a", "<a rel=\"lightbox[" + ID + "]\" ");

				content = content.Replace(match.Value, tmp);
			}

			return content;
		}
	}
}
