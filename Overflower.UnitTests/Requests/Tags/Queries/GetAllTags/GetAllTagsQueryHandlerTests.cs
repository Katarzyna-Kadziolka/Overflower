using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Overflower.Application.Requests.Tags.Queries.GetAllTags;
using Overflower.Application.Services.StackOverflow;
using Overflower.Tests.Shared.Seed;

namespace Overflower.UnitTests.Requests.Tags.Queries.GetAllTags;

[TestFixture]
public class GetAllTagsQueryHandlerTests : BaseRequestTest {
    [Test]
    public async Task Handle_NoData_ShouldBeSuccess() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        var tags = await ApplicationDbContext.Tags.ToListAsync();
        var request = new GetAllTagsQuery();
        var stackOverflowClient = Substitute.For<IStackOverflowClient>();
        var sut = new GetAllTagsQueryHandler(ApplicationDbContext, stackOverflowClient);
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        // Assert
        result.Should().HaveSameCount(tags);
        result.Should().BeEquivalentTo(tags);
    }
}
