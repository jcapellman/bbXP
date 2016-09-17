using System.Threading.Tasks;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Content;

namespace bbxp.CommonLibrary.Handlers {
    public class ContentHandler : BaseHandler {
        public ContentHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "Content";

        public async Task<ReturnSet<ContentResponseItem>> GetContent(string urlArg) => await GetAsync<ReturnSet<ContentResponseItem>>($"urlArg={urlArg}");        
    }
}