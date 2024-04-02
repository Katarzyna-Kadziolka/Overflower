using Microsoft.AspNetCore.Http;
using Overflower.Application.Paging;
using Overflower.Application.Requests.Tags;
using Overflower.Application.Requests.Tags.Commands.UpdateTags;
using Overflower.Application.Requests.Tags.Queries.GetAllTags;
using Overflower.Tests.Shared.Seed;

namespace Overflower.IntegrationTests.Controllers;

public class TagsControllerTests : BaseTest {
    private const string Route = "api/tags";

    [Test]
    public async Task GetAll_WhenDataIsCorrect_ShouldBeOk() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        var query = new QueryString()
            .Add(nameof(GetAllTagsQuery.Page), 2.ToString())
            .Add(nameof(GetAllTagsQuery.PageSize), 50.ToString())
            .Add(nameof(GetAllTagsQuery.SortingOrder), SortingOrder.Descending.ToString())
            .Add(nameof(GetAllTagsQuery.TagSortBy), TagSortBy.Name.ToString())
            .ToString();
        // Act
        var response = await HttpClient.GetAsync(Route + query);
        // Assert
        response.Should().Be200Ok();
        var result = await response.Content.DeserializeContentAsync<PageResult<TagDto>>();
        result!.Items.Should().BeInDescendingOrder(i => i.Name);
        result.CurrentPage.Should().Be(2);
    }

    [Test]
    public async Task Update_WhenDataIsCorrect_ShouldBeOk() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        var request = new UpdateTagsCommand();
        // Act
        var response = await HttpClient.PutAsJsonAsync(Route, request);
        // Assert
        response.Should().Be200Ok();
    }
}
