using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overflower.Shared.Services.DateTimeProviders;

namespace Overflower.Shared.Extensions; 

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
	public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<IDateTimeProvider, DateTimeProvider>();
		return services;
	}
}