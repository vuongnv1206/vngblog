
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using VngBLog.Application.Common.Behaviors;

namespace VngBlog.Application
{
	public static class ApplicationServiceRegistration
	{

		public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly())
				.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
				.AddMediatR(Assembly.GetExecutingAssembly())
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
				;



			return services;

		}
	}
}
