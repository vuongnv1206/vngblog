using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Common;
using VngBlog.Infrastructure.Common;
using VngBlog.Infrastructure.Configurations;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddDbContext<VngBlogDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("Default")));

			services.Configure<SMTPEmailSettings>(configuration.GetSection("SMTPEmailSettings"));

			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			services.AddScoped(typeof(IEmailService), typeof(SmtpEmailService));

			return services;

		}
	}
}
