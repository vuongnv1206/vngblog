using Microsoft.EntityFrameworkCore;
using VngBlog.Infrastructure.EntityFrameworkCore;
using VngBlog.Infrastructure.Seedings;

namespace VngBlog.Api
{
	public static class MigrationManager
	{
		public static WebApplication MigrateDatabase(this WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				using (var context = scope.ServiceProvider.GetRequiredService<VngBlogDbContext>())
				{
					context.Database.Migrate();
					new SeedingDataIdentity().SeedData(context).Wait();
					new SeedingDataPostCategory().SeedData(context).Wait();

                }
			}
			return app;
		}
	}
}
