using bbxp.lib.Database.Tables.Base;

namespace bbxp.lib.Database.Tables
{
    public class Posts : BaseTable
    {
        public required string Title { get; set; }

        public required string Body { get; set; }

        public required string Category { get; set; }
    }
}