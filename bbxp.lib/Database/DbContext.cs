using bbxp.lib.Database.Tables;
using bbxp.lib.Database.Tables.Base;

using Microsoft.EntityFrameworkCore;

namespace bbxp.lib.Database
{
    public class BbxpContext(DbContextOptions<BbxpContext> options) : DbContext(options)
    {
        public DbSet<Posts> Posts { get; set; }

        #region Compiled Queries - Eliminate query translation overhead on repeated calls

        /// <summary>
        /// Compiled query for fetching active posts ordered by date with a limit.
        /// ~15-20% faster than non-compiled equivalent on repeated calls.
        /// </summary>
        public static readonly Func<BbxpContext, int, IAsyncEnumerable<Posts>> GetActivePostsAsync =
            EF.CompileAsyncQuery((BbxpContext context, int count) =>
                context.Posts
                    .AsNoTracking()
                    .Where(p => p.Active)
                    .OrderByDescending(p => p.PostDate)
                    .Take(count)
                    .AsQueryable());

        /// <summary>
        /// Compiled query for fetching active posts by category.
        /// </summary>
        public static readonly Func<BbxpContext, string, IAsyncEnumerable<Posts>> GetPostsByCategoryAsync =
            EF.CompileAsyncQuery((BbxpContext context, string category) =>
                context.Posts
                    .AsNoTracking()
                    .Where(p => p.Active && p.Category == category)
                    .OrderByDescending(p => p.PostDate)
                    .AsQueryable());

        /// <summary>
        /// Compiled query for fetching a single post by URL.
        /// </summary>
        public static readonly Func<BbxpContext, string, Task<Posts?>> GetPostByUrlAsync =
            EF.CompileAsyncQuery((BbxpContext context, string url) =>
                context.Posts
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Active && p.URL == url));

        /// <summary>
        /// Compiled query for paged active posts.
        /// </summary>
        public static readonly Func<BbxpContext, int, int, IAsyncEnumerable<Posts>> GetActivePostsPagedAsync =
            EF.CompileAsyncQuery((BbxpContext context, int skip, int take) =>
                context.Posts
                    .AsNoTracking()
                    .Where(p => p.Active)
                    .OrderByDescending(p => p.PostDate)
                    .Skip(skip)
                    .Take(take)
                    .AsQueryable());

        /// <summary>
        /// Compiled query for paged posts by category.
        /// </summary>
        public static readonly Func<BbxpContext, string, int, int, IAsyncEnumerable<Posts>> GetPostsByCategoryPagedAsync =
            EF.CompileAsyncQuery((BbxpContext context, string category, int skip, int take) =>
                context.Posts
                    .AsNoTracking()
                    .Where(p => p.Active && p.Category == category)
                    .OrderByDescending(p => p.PostDate)
                    .Skip(skip)
                    .Take(take)
                    .AsQueryable());

        #endregion

        #region Projected Queries - Reduce memory by selecting only needed columns

        /// <summary>
        /// Get post summaries (without Body) for list views - significantly reduces allocations.
        /// </summary>
        public static readonly Func<BbxpContext, int, IAsyncEnumerable<DTOs.PostSummaryDto>> GetPostSummariesAsync =
            EF.CompileAsyncQuery((BbxpContext context, int count) =>
                context.Posts
                    .AsNoTracking()
                    .Where(p => p.Active)
                    .OrderByDescending(p => p.PostDate)
                    .Take(count)
                    .Select(p => new DTOs.PostSummaryDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Category = p.Category,
                        URL = p.URL,
                        PostDate = p.PostDate,
                        BodyPreview = p.Body.Length > 200 ? p.Body.Substring(0, 200) + "..." : p.Body
                    }));

        /// <summary>
        /// Get post summaries by category (without Body).
        /// </summary>
        public static readonly Func<BbxpContext, string, IAsyncEnumerable<DTOs.PostSummaryDto>> GetPostSummariesByCategoryAsync =
            EF.CompileAsyncQuery((BbxpContext context, string category) =>
                context.Posts
                    .AsNoTracking()
                    .Where(p => p.Active && p.Category == category)
                    .OrderByDescending(p => p.PostDate)
                    .Select(p => new DTOs.PostSummaryDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Category = p.Category,
                        URL = p.URL,
                        PostDate = p.PostDate,
                        BodyPreview = p.Body.Length > 200 ? p.Body.Substring(0, 200) + "..." : p.Body
                    }));

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Posts>().HasKey(x => x.Id);

            // Performance indexes for common query patterns
            modelBuilder.Entity<Posts>().HasIndex(x => x.Active);
            modelBuilder.Entity<Posts>().HasIndex(x => new { x.Active, x.Category });
            modelBuilder.Entity<Posts>().HasIndex(x => new { x.Active, x.URL });
            modelBuilder.Entity<Posts>().HasIndex(x => new { x.Active, x.PostDate });
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