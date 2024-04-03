using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Overflower.Api;
using Overflower.Application.Services.StackOverflow;
using Overflower.IntegrationTests.Services;
using Overflower.Persistence;
using Overflower.Shared.Services.DateTimeProviders;
using Testcontainers.PostgreSql;

namespace Overflower.IntegrationTests;

public class OverflowerApiWebApplicationFactory : WebApplicationFactory<IApiMarker> {
	private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
	                                                             .WithDatabase("application")
	                                                             .WithUsername("postgres")
	                                                             .WithPassword("password")
	                                                             .Build();


	public HttpClient HttpClient { get; private set; } = default!;
	private DbConnection _dbConnection = default!;
	private Respawner _respawner = default!;

	public async Task ResetDatabase() {
		await _respawner.ResetAsync(_dbConnection);
	}

	public async Task InitializeAsync() {
		await _postgreSqlContainer.StartAsync();
		HttpClient = CreateClient();
		await InitializeRespawner();
	}

	// ReSharper disable once IdentifierTypo
	private async Task InitializeRespawner() {
		_dbConnection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
		await _dbConnection.OpenAsync();
		_respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions {
			DbAdapter = DbAdapter.Postgres,
			WithReseed = true,
			SchemasToInclude = new[] {
				"public"
			}
		});
	}

	public override async ValueTask DisposeAsync() {
		await base.DisposeAsync();
		await _dbConnection.CloseAsync();
		await _postgreSqlContainer.StopAsync();
	}


	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		base.ConfigureWebHost(builder);

		var config = new ConfigurationBuilder()
		             .AddInMemoryCollection(new Dictionary<string, string?> {
			             ["ConnectionStrings:Default"] = _postgreSqlContainer.GetConnectionString()
		             })
		             .Build();
		builder.UseConfiguration(config);
		
		base.ConfigureWebHost(builder);
		builder.ConfigureAppConfiguration(configBuilder => {
			configBuilder.AddInMemoryCollection(
				new Dictionary<string, string?> {
					["Serilog:MinimumLevel:Override:Microsoft"] = "Warning"
				});
		});

		builder.ConfigureServices(services => {
			var sp = services.BuildServiceProvider();
			
			using var scope = sp.CreateScope();
			var scopedServices = scope.ServiceProvider;
			var db = scopedServices.GetRequiredService<ApplicationDbContext>();
			db.Database.Migrate();
		});

		builder.ConfigureTestServices(services => {
			services.RemoveAll<IStackOverflowClient>();
			services.AddScoped<IStackOverflowClient, TestStackOverflowClient>();
		});
	}
}
