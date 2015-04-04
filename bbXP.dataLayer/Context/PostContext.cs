using System;
using bbXP.dataLayer.Models;
using Microsoft.Data.Entity;

namespace bbXP.dataLayer.Context {
    public class PostDataContext : DbContext {
		public DbSet<Post> Posts { get; set; }
    }
}