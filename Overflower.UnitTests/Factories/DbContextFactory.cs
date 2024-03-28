using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Overflower.Persistence;

namespace Overflower.UnitTests.Factories;

public static class DbContextFactory
{
	public static (ApplicationDbContext, SqliteConnection) Create()
	{
		var connection = new SqliteConnection("DataSource=:memory:");  
		connection.Open();
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.EnableSensitiveDataLogging()
			.UseSqlite(connection);
		var applicationDbContext = new ApplicationDbContext(options.Options);

		applicationDbContext.Database.EnsureDeleted();
		applicationDbContext.Database.EnsureCreated();
		
		return (applicationDbContext, connection);
	}
}
