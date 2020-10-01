using bbxp.lib.DAL;
using bbxp.lib.Transports.Content;

using System.Linq;

namespace bbxp.lib.Interfaces
{
    public class ContentService : IContentService
    {
        public ContentResponseItem GetContent(BbxpDbContext context, string contentUrl)
        {
            var content = context.Content.FirstOrDefault(a => a.URLSafename == contentUrl);

            return new ContentResponseItem { Body = content.Body, Title = content.Title };
        }
    }
}