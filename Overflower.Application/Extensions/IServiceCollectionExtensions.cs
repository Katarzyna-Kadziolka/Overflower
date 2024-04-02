using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overflower.Application.Behaviour;
using Overflower.Persistence;

namespace Overflower.Application.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {
		services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<IApplicationMarker>());
		services.AddFluentValidation();
		services.AddDbContext(configuration);
		return services;
	}

	public static IApplicationBuilder UseApplication(
		this IApplicationBuilder builder) {
		return builder.UseMiddleware<ApplicationExceptionMiddleware>();
	}

	private static void AddDbContext(this IServiceCollection services, IConfiguration configuration) {
		services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(configuration.GetConnectionString("Default")),
			        optionsLifetime: ServiceLifetime.Singleton)
		        .AddDbContextFactory<ApplicationDbContext>();
	}

	private static void AddFluentValidation(this IServiceCollection services) {
		services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(includeInternalTypes: true);
		services.AddFluentValidationAutoValidation();
	}
}
