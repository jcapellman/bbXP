using bbxp.cli.migration.SQLServer.Objects;

using Microsoft.EntityFrameworkCore;

namespace bbxp.cli.migration.SQLServer
{
    public class bbxpSSContext : DbContext
    {
        public DbSet<Posts> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=bbxp_jarredcapellman;user id=bbxp;password=devdevdev;TrustServerCertificate=true");
        }
    }
}