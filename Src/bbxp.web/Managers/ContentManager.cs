using System.Linq;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.Transports.Content;

using bbxp.web.DAL;

namespace bbxp.web.Managers {
    public class ContentManager : BaseManager {
        public ContentManager(ManagerContainer container) : base(container) { }

        public ReturnSet<ContentResponseItem> GetContent(string urlSafeName)
        {
            var (isFound, cachedResult) = GetCachedItem<ContentResponseItem>(urlSafeName);

            if (isFound)
            {
                return new ReturnSet<ContentResponseItem>(cachedResult);
            }

            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var content = eFactory.Content.FirstOrDefault(a => a.URLSafename == urlSafeName && a.Active);

                if (content == null) {
                    return new ReturnSet<ContentResponseItem>(exception: $"{urlSafeName} cannot be found");
                }

                var response = new ReturnSet<ContentResponseItem>(new ContentResponseItem { Body = content.Body, Title = content.Title });

                AddCachedItem(content.URLSafename, response);

                return response;
            }
        }
    }
}