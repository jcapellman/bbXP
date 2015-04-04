using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Data.Entity;

using bbXP.dataLayer.Models;

namespace bbXP.dataLayer.Context {
	public class BaseDbContext : DbContext {
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
			var changeSet = ChangeTracker.Entries<IEditModel>();

			if (changeSet != null) {
				foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged)) {
					entry.Entity.Modified = DateTime.Now;

					if (entry.State == EntityState.Added) {
						entry.Entity.Created = DateTime.Now;
					}

					entry.Entity.Active = entry.State != EntityState.Deleted;
				}
			}
		
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}