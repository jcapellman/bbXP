using bbxp.lib.Database.Tables;

using Microsoft.EntityFrameworkCore;

namespace bbxp.lib.Database
{
    public class bbxpDbContext : DbContext
    {
        public DbSet<Posts> Posts { get; set; }

        public bbxpDbContext(DbContextOptions<bbxpDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;database=postgres;user id=postgres;password=devdevdev");
        }
    }
}