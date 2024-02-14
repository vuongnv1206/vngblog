
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace VngBlog.Application
{
	public static class ApplicationServiceRegistration
	{

		public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly())
				.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())

				;



			return services;

		}
	}
}
