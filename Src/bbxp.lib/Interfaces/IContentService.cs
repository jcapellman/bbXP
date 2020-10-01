using bbxp.lib.DAL;
using bbxp.lib.Transports.Content;

namespace bbxp.lib.Interfaces
{
    public interface IContentService
    {
        ContentResponseItem GetContent(BbxpDbContext context, string contentUrl);
    }
}