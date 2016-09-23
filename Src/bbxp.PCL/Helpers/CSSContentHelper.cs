using System.Threading.Tasks;

using bbxp.PCL.Handlers;
using bbxp.PCL.Settings;

namespace bbxp.PCL.Helpers {
    public class CSSContentHelper {
        public async Task<string> GetFullContent(GlobalSettings gSettings, string content) {
            var cssContentHandler = new CSSContentHandler(gSettings);

            var cssStr = await cssContentHandler.GetCSSContent();

            return $"<head><meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\" /><style type='text/css'>{cssStr}</style></head><body class=\"bodyMobile\"><div id=\"PostContainer\"><div id=\"ContentContainer\">{content}</div></div></body>";
        }
    }
}