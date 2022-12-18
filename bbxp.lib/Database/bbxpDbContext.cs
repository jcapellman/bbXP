using bbxp.lib.Database.Tables;
using bbxp.lib.Database.Tables.Base;
using Microsoft.EntityFrameworkCore;

namespace bbxp.lib.Database
{
    public class bbxpDbContext : DbContext
    {
        public DbSet<Posts> Posts { get; set; }

        public bbxpDbContext(DbContextOptions<bbxpDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Posts>().HasKey(x => x.Id);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseTable && 
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        ((BaseTable)entityEntry.Entity).Created = DateTime.Now;
                        ((BaseTable)entityEntry.Entity).Active = true;

                        break;
                }

                ((BaseTable)entityEntry.Entity).Modified = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}