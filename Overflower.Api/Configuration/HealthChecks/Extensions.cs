using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Overflower.Api.Configuration.HealthChecks;

public static class Extensions {
	public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration,
	                                                 IWebHostEnvironment webHostEnvironment) {
		services
			.AddHealthChecks()
			.AddNpgSql(configuration.GetConnectionString("Default")!);

		return services;
	}

	public static IEndpointRouteBuilder MapHealthChecks(this IEndpointRouteBuilder builder) {
		builder.MapHealthChecks("/health-check", new HealthCheckOptions {
			Predicate = _ => true,
			ResponseWriter = (context, result) => {
				context.Response.ContentType = "text/plain";
				var entries = string.Join(Environment.NewLine,
					result.Entries.Select(e => $"Name: {e.Key}, Status: {e.Value.Status}"));
				return context.Response.WriteAsync(
					$"{result.Status.ToString()}{Environment.NewLine}{entries}");
			}
		});

		return builder;
	}
}