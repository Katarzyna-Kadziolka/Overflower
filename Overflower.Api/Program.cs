using Serilog;
using Overflower.Api.Configuration.ApiVersioning;
using Overflower.Api.Configuration.HealthChecks;
using Overflower.Api.Configuration.JsonSerilizer;
using Overflower.Api.Configuration.Logging;
using Overflower.Api.Configuration.ServicesValidation;
using Overflower.Api.Configuration.Swagger;
using Overflower.Application.Extensions;
using Overflower.Infrastructure.Extensions;
using Overflower.Shared.Extensions;

Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .CreateLogger();

Log.Information("Starting up");

try {
	RunApplication();
}


catch (Exception ex) {
	Log.Fatal(ex, "Unhandled exception");
}
finally {
	Log.Information("Shut down complete");
	Log.CloseAndFlush();
}


void RunApplication() {
	var builder = WebApplication.CreateBuilder(args);
	// Logging
	builder.Host.UseSerilog((ctx, lc) => lc
	                                     .Enrich.FromLogContext()
	                                     .WriteTo.Console()
	                                     .ReadFrom.Configuration(ctx.Configuration));
	// Add services to the container.
	builder.Services.AddShared(builder.Configuration);
	builder.Services.AddApplication(builder.Configuration);
	builder.Services.AddInfrastructure(builder.Configuration);
	builder.Services.AddHealthChecks(builder.Configuration, builder.Environment);
	builder.Services.AddControllers().AddJsonSerializer();
	builder.Services.AddDefaultApiVersioning();
	builder.Services.AddCors();
	builder.Services.AddSwagger();
	builder.Host.AddServicesValidationOnStart();

	var app = builder.Build();
	app.UseCors(policyBuilder =>
	{
		policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
	app.UseSerilogRequestLogging(options =>
	{
		options.GetLevel = LogHelper.ExcludeHealthChecks;
	});
	app.UseApplication();
	// Configure the HTTP request pipeline.
	if (!app.Environment.IsProduction()) {
		app.UseSwaggerUi();
	}
	app.MapHealthChecks();
	app.MapControllers();
	app.Run();
}