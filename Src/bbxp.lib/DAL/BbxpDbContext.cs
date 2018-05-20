using bbxp.lib.DAL.Objects;

using Microsoft.EntityFrameworkCore;

namespace bbxp.lib.DAL
{
    public class BbxpDbContext : DbContext
    {
        public DbSet<Posts> Posts { get; set; }

        public DbSet<Content> Content { get; set; }

        public DbSet<DGT_Posts> DGT_Posts { get; set; }

        public DbSet<DGT_Archives> DGT_Archives { get; set; }

        public DbSet<Tags> Tags { get; set; }

        public DbSet<Posts2Tags> Posts2Tags { get; set; }

        public DbSet<Requests> Requests { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<DGT_MostFrequentedPages> DGT_MostFrequentedPages { get; set; }

        public DbSet<DGT_MostFrequentedPagesHeader> DGT_MostFrequentedPagesHeader { get; set; }

        public BbxpDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}