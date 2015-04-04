using bbXP.dataLayer.Models;

using Microsoft.Data.Entity;

namespace bbXP.dataLayer.Context {
    public class PostDataContext : BaseDbContext {
		public DbSet<Post> Posts { get; set; }
    }
}