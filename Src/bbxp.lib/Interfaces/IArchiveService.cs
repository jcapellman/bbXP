using bbxp.lib.DAL;
using bbxp.lib.Transports.Archives;

using System.Collections.Generic;

namespace bbxp.lib.Interfaces
{
    public interface IArchiveService
    {
        List<ArchiveListingResponseItem> GetArchiveList(BbxpDbContext context);
    }
}