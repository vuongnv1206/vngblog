
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VngBlog.Domain.Abstractions;
using VngBlog.Domain.Abstractions.Interfaces;
using VngBlog.Domain.Entities;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Infrastructure.EntityFrameworkCore
{
    public class VngBlogDbContext : IdentityDbContext<AppUser>
	{

		public DbSet<Post> Posts { get; set; }
		public DbSet<PostCategory> PostCategories { get; set; }
		public DbSet<PostTag> PostTags { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<PostActivityLog> PostActivityLogs { get; set; }
		public DbSet<Series> Series { get; set; }
		public DbSet<PostInSeries> PostInSeries { get; set; }
		public DbSet<PostComment> PostComments { get; set; }
		public VngBlogDbContext(DbContextOptions<VngBlogDbContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			base.OnConfiguring(builder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{

			base.OnModelCreating(builder);
			

			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}
		}

        public virtual async Task<int> SaveChangesAsync(string username = "SYSTEM")
        {
            foreach (var entry in base.ChangeTracker.Entries()
    .Where(entry => entry.Entity is IAuditable && (entry.State == EntityState.Added || entry.State == EntityState.Modified)))
            {
                var entity = (IAuditable)entry.Entity;
                entity.LastModifiedTime = DateTimeOffset.UtcNow;
                entity.LastModifiedBy = username;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedTime = DateTimeOffset.UtcNow;
                    entity.CreatedBy = username;
                }
            }


            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
