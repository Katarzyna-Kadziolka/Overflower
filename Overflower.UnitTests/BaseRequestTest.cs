using Microsoft.Data.Sqlite;
using Overflower.Persistence;
using Overflower.UnitTests.Factories;

namespace Overflower.UnitTests; 
public abstract class BaseRequestTest {
	protected ApplicationDbContext ApplicationDbContext { get; private set; } = null!;
	private SqliteConnection? _connection;

	[SetUp]
	public void BaseSetup()
	{
		(ApplicationDbContext, _connection) = DbContextFactory.Create();
	}

	[TearDown]
	public virtual async Task BaseTearDown()
	{
		await _connection!.DisposeAsync();
		await ApplicationDbContext.DisposeAsync();
	}
}
