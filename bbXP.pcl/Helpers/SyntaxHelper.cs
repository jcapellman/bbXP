using System;

namespace bbXP.pcl.Helpers {
	public class SyntaxHelper {
		public SyntaxHelper(string value) {
			FullTagName = value;
		}

		public string NameOnly => FullTagName.Replace("[/", "").Replace("[", "").Replace("]", "");

		public bool IsClosingTag {
			get {
				return FullTagName.StartsWith("[/");
			}
		}

		public string Value {
			get {
				if (FullTagName.Length < 2) {
					return String.Empty;
				}

				var leftIdx = FullTagName.IndexOf(']') + 1;
				var rightIdx = FullTagName.IndexOf('[', 1);

				return FullTagName.Substring(leftIdx, rightIdx - leftIdx);
			}
		}

		public string FullTagName { get; private set; }

		private enum SYNTAXTAGS {
			csharp,
			xml,
			sql,
			css,
			php,
			c,
			bash,
			shell,
			cpp,
			js,
			java,
			ps,
			plain
		}

		public bool IsParseable {
			get {
				SYNTAXTAGS tag;

				return Enum.TryParse(NameOnly, out tag);
			}
		}
	}
}
