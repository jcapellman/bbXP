using bbxp.web.DAL.Objects;

using Microsoft.EntityFrameworkCore;

namespace bbxp.web.DAL
{
    public class EntityFactory : DbContext
    {
        public DbSet<Posts> Posts { get; set; }

        public DbSet<Content> Content { get; set; }

        public DbSet<DGT_Posts> DGT_Posts { get; set; }

        public DbSet<DGT_Archives> DGT_Archives { get; set; }

        public DbSet<Requests> Requests { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<DGT_MostFrequentedPages> DGT_MostFrequentedPages { get; set; }

        public DbSet<DGT_MostFrequentedPagesHeader> DGT_MostFrequentedPagesHeader { get; set; }

        private readonly string _connectionString;

        public EntityFactory() {
            _connectionString = "Data Source=bbxp";
        }

        public EntityFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}