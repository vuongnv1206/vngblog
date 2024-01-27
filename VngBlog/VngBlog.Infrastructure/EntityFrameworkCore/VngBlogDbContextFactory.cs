using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Infrastructure.EntityFrameworkCore
{
	public class VngBlogextFactory : IDesignTimeDbContextFactory<VngBlogDbContext>
	{
		public VngBlogDbContext CreateDbContext(string[] args)
		{

			var configuration = BuildConfiguration();

			var builder = new DbContextOptionsBuilder<VngBlogDbContext>()
				.UseSqlServer(configuration.GetConnectionString("Default"));

			return new VngBlogDbContext(builder.Options);
		}

		private static IConfigurationRoot BuildConfiguration()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
				.AddJsonFile("appsettings.json", optional: false);


			return builder.Build();

		}
	}
}
