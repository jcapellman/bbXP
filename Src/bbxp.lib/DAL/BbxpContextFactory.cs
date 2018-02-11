using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace bbxp.lib.DAL
{
    public class BbxpContextFactory : IDesignTimeDbContextFactory<BbxpDbContext>
    {
        public BbxpDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BbxpDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=XX;user id=XX;password=123456");

            return new BbxpDbContext(optionsBuilder.Options);
        }
    }
}