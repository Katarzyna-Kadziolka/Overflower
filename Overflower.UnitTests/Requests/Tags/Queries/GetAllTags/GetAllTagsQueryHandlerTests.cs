using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Overflower.Application.Paging;
using Overflower.Application.Requests.Tags;
using Overflower.Application.Requests.Tags.Queries.GetAllTags;
using Overflower.Application.Services.StackOverflow;
using Overflower.Application.Services.StackOverflow.Data;
using Overflower.Tests.Shared.Generators;
using Overflower.Tests.Shared.Seed;

namespace Overflower.UnitTests.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandlerTests : BaseRequestTest {
    [Test]
    public async Task Handle_ShouldReturnPageResult() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        const int expectedTotalPages = 20;
        const int expectedItemsCount = 50;
        const int expectedPage = 1;
        var request = new GetAllTagsQuery {
            Page = expectedPage,
            PageSize = expectedItemsCount
        };
        var stackOverflowClient = Substitute.For<IStackOverflowClient>();
        var sut = new GetAllTagsQueryHandler(ApplicationDbContext, stackOverflowClient);
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        // Assert
        result.CurrentPage.Should().Be(expectedPage);
        result.Items.Should().HaveCount(expectedItemsCount);
        result.TotalPages.Should().Be(expectedTotalPages);
    }
    
    [Test]
    public async Task Handle_NoDataInDatabase_ShouldReturnPageResult() {
        // Arrange
        const int expectedTotalPages = 20;
        const int expectedItemsCount = 50;
        const int expectedPage = 1;
        var request = new GetAllTagsQuery {
            Page = expectedPage,
            PageSize = expectedItemsCount
        };
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
        var sut = new GetAllTagsQueryHandler(ApplicationDbContext, stackOverflowClient);
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        // Assert
        result.CurrentPage.Should().Be(expectedPage);
        result.Items.Should().HaveCount(expectedItemsCount);
        result.TotalPages.Should().Be(expectedTotalPages);
    }
    
    [Test]
    public async Task Handle_SortAscending_ShouldReturnSortedPageResult() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        const int expectedTotalPages = 20;
        const int expectedItemsCount = 50;
        const int expectedPage = 1;
        var request = new GetAllTagsQuery {
            Page = expectedPage,
            PageSize = expectedItemsCount,
            TagSortBy = TagSortBy.Name,
            SortingOrder = SortingOrder.Ascending
        };
        var stackOverflowClient = Substitute.For<IStackOverflowClient>();
        var sut = new GetAllTagsQueryHandler(ApplicationDbContext, stackOverflowClient);
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        // Assert
        result.CurrentPage.Should().Be(expectedPage);
        result.Items.Should().HaveCount(expectedItemsCount);
        result.TotalPages.Should().Be(expectedTotalPages);
        result.Items.Should().BeInAscendingOrder(i => i.Name);
    }
    
    [Test]
    public async Task Handle_SortDescending_ShouldReturnSortedPageResult() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        const int expectedTotalPages = 20;
        const int expectedItemsCount = 50;
        const int expectedPage = 1;
        var request = new GetAllTagsQuery {
            Page = expectedPage,
            PageSize = expectedItemsCount,
            TagSortBy = TagSortBy.Name,
            SortingOrder = SortingOrder.Descending
        };
        var stackOverflowClient = Substitute.For<IStackOverflowClient>();
        var sut = new GetAllTagsQueryHandler(ApplicationDbContext, stackOverflowClient);
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        // Assert
        result.CurrentPage.Should().Be(expectedPage);
        result.Items.Should().HaveCount(expectedItemsCount);
        result.TotalPages.Should().Be(expectedTotalPages);
        result.Items.Should().BeInDescendingOrder(i => i.Name);
    }
    
    [Test]
    public async Task Handle_PageOutOfTotalPages_ShouldReturnEmptyItemsInPageResult() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        const int expectedTotalPages = 20;
        const int pageSize = 50;
        const int expectedPage = 21;
        var request = new GetAllTagsQuery {
            Page = expectedPage,
            PageSize = pageSize
        };
        var stackOverflowClient = Substitute.For<IStackOverflowClient>();
        var sut = new GetAllTagsQueryHandler(ApplicationDbContext, stackOverflowClient);
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        // Assert
        result.CurrentPage.Should().Be(expectedPage);
        result.Items.Should().BeEmpty();
        result.TotalPages.Should().Be(expectedTotalPages);
    }
    
    [Test]
    public async Task Handle_Percentage_ShouldReturnPageResult() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        const int expectedTotalPages = 20;
        const int expectedItemsCount = 50;
        const int expectedPage = 1;
        var request = new GetAllTagsQuery {
            Page = expectedPage,
            PageSize = expectedItemsCount
        };
        var stackOverflowClient = Substitute.For<IStackOverflowClient>();
        var sut = new GetAllTagsQueryHandler(ApplicationDbContext, stackOverflowClient);
        var totalCount = await ApplicationDbContext.Tags.SumAsync(x => (long) x.Count, CancellationToken.None);
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        // Assert
        result.CurrentPage.Should().Be(expectedPage);
        result.Items.Should().HaveCount(expectedItemsCount);
        result.Items.Should().Contain(x => Math.Abs(x.PercentageShare - (double) x.Count / totalCount * 100) < 0.01);
        result.TotalPages.Should().Be(expectedTotalPages);
    }
}
