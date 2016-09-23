using System.Threading.Tasks;

using bbxp.PCL.Settings;

namespace bbxp.PCL.Handlers {
    public class CSSContentHandler : BaseHandler {
        public CSSContentHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "CSSContent";

        public async Task<string> GetCSSContent() => await GetBaseAsync<string>(string.Empty);
    }
}