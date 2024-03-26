using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overflower.Application.Services.Emails;
using Overflower.Infrastructure.Services.Emails;

namespace Overflower.Infrastructure.Extensions; 

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<IEmailService, EmailService>();
		return services;
	}
}