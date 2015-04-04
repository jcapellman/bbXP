using bbXP.dataLayer.Models;

using Microsoft.Data.Entity;

namespace bbXP.dataLayer.Context {
    public class UserDataContext : BaseDbContext {
		public DbSet<User> Users { get; set; } 
    }
}