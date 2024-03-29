﻿using Microsoft.AspNetCore.Identity.UI.Services;
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
			{
				options.UseSqlServer(configuration.GetConnectionString("Default"),
					builder => builder.MigrationsAssembly(typeof(VngBlogDbContext).Assembly.FullName)
					);
			});
			//services.AddDbContext<VngBlogDbContext>(options =>
			//options.UseSqlServer(configuration.GetConnectionString("Default")));

			//Register services
			services
				.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
				//.AddScoped<ICustomerService, CustomerService>();

			services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
			services.AddScoped<EmailConfiguration>();
			services.AddScoped<IUnitOfWork,UnitOfWork>();
			services.AddScoped<IEmailSender, SendMailService>();
	
			return services;

		}
	}
}
