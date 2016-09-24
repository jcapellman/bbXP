using System.Threading.Tasks;

using bbxp.PCL.Settings;

namespace bbxp.PCL.Handlers {
    public class CSSContentHandler : BaseHandler {
        public CSSContentHandler() : base(new GlobalSettings()) { }

        protected override string BaseControllerName() => "CSSContent";

        public async Task<string> GetCSSContent(string url) => await GetBaseAsync<string>(string.Empty, url);
    }
}