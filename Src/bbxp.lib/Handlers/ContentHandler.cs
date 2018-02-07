using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Settings;
using bbxp.lib.Transports.Content;

namespace bbxp.lib.Handlers
{
    public class ContentHandler : BaseHandler {
        public ContentHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "Content";

        public async Task<ReturnSet<ContentResponseItem>> GetContent(string urlArg) => await GetAsync<ReturnSet<ContentResponseItem>>($"/{urlArg}");        
    }
}