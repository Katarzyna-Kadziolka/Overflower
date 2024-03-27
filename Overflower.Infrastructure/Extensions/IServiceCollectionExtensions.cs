using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overflower.Application.Services.StackOverflow;
using Overflower.Infrastructure.Services.StackOverflow;

namespace Overflower.Infrastructure.Extensions; 

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<IStackOverflowClient, StackOverflowClientClient>();
		return services;
	}
}
