using System.Linq;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Containers;
using bbxp.CommonLibrary.Transports.Content;

using bbxp.WebAPI.DataLayer.Entities;

namespace bbxp.WebAPI.BusinessLayer.Managers {
    public class ContentManager : BaseManager {
        public ContentManager(ManagerContainer container) : base(container) { }

        public ReturnSet<ContentResponseItem> GetContent(string urlSafeName) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var content = eFactory.Content.FirstOrDefault(a => a.URLSafename == urlSafeName && a.Active);

                return new ReturnSet<ContentResponseItem>(new ContentResponseItem { Body = content.Body, Title = content.Title });
            }
        }
    }
}