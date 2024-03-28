using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Overflower.Application.Requests.Tags.Commands.UpdateTags;
using Overflower.Application.Services.StackOverflow;
using Overflower.Application.Services.StackOverflow.Data;
using Overflower.Tests.Shared.Generators;
using Overflower.Tests.Shared.Seed;

namespace Overflower.UnitTests.Requests.Tags.Commands.UpdateTags;

public class UpdateTagsCommandTests : BaseRequestTest {
    [Test]
    public async Task Handle_NoData_ShouldBeSuccess() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        var tags = await ApplicationDbContext.Tags.ToListAsync();
        var request = new UpdateTagsCommand();
        var stackOverflowClient = Substitute.For<IStackOverflowClient>();
        var newTags = new TagGenerator().Generate(1_000)
            .Select(x => new TagResponse {
                HasSynonyms = x.HasSynonyms,
                IsRequired = x.IsRequired,
                IsModeratorOnly = x.IsModeratorOnly,
                Count = x.Count,
                Name = x.Name
            }).ToList();
        stackOverflowClient.GetTagsAsync(1_000).Returns(newTags);
        var sut = new UpdateTagsCommandHandler(ApplicationDbContext, stackOverflowClient);
        // Act
        await sut.Handle(request, CancellationToken.None);
        // Assert
        await stackOverflowClient.Received(1).GetTagsAsync(1_000);
        var dbTags = await ApplicationDbContext.Tags.ToListAsync();
        dbTags.Should().HaveCount(1_000);
        dbTags.Should().NotBeEquivalentTo(tags);
    }
}
