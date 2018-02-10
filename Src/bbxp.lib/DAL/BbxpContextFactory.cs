using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace bbxp.lib.DAL
{
    public class BbxpContextFactory : IDesignTimeDbContextFactory<BbxpDbContext>
    {
        public BbxpDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BbxpDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Database=bbxp_jarredcapellman;user id=postgres;password=bbxp4ever;");

            return new BbxpDbContext(optionsBuilder.Options);
        }
    }
}