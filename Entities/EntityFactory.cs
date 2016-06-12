using bbxp.MVC.Entities.Objects.Table;

using Microsoft.EntityFrameworkCore;

namespace bbxp.MVC.Entities {
    public class EntityFactory : DbContext {
        public DbSet<Posts> Posts { get; set; }

        public DbSet<Content> Content { get; set; }

        public DbSet<DGT_Posts> DGT_Posts { get; set; }

        public DbSet<DGT_Archives> DGT_Archives { get; set; } 

        public DbSet<Requests> Requests { get; set; }
         
        private readonly string _connectionString;

        public EntityFactory(string connectionString) {
            _connectionString = connectionString;
        }

        public EntityFactory() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}