using bbxp.lib.DAL;
using bbxp.lib.Transports.Archives;

using System.Collections.Generic;
using System.Linq;

namespace bbxp.lib.Interfaces
{
    public class ArchiveService : IArchiveService
    {
        public List<ArchiveListingResponseItem> GetArchiveList(BbxpDbContext context)
        {
            return context.Posts.OrderByDescending(a => a.Created).Select(b => new ArchiveListingResponseItem
            {
                Date = b.Created.Date,
                Title = b.Title,
                URL = $"{b.Created.Year}/{b.Created.Month}/{b.Created.Day}/{b.URLSafename}"
            }).ToList();
        }
    }
}