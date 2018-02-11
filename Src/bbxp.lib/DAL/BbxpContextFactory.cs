namespace bbxp.lib.DAL
{
    public class BbxpContextFactory : IDesignTimeDbContextFactory<BbxpDbContext>
    {
        public BbxpDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BbxpDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=bbxp_jarredcapellman;user id=sa;password=bbxp4ever;");

            return new BbxpDbContext(optionsBuilder.Options);
        }
    }
}