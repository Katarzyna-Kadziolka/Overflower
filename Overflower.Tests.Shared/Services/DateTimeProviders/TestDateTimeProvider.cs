
using Overflower.Shared.Services.DateTimeProviders;

namespace Overflower.Tests.Shared.Services.DateTimeProviders;

public class TestDateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow { get; } = new DateTime(2022, 11, 1, 1, 0, 0).ToUniversalTime();
	public DateTime Now { get; } = new DateTime(2022, 11, 1, 2, 0, 0);
}