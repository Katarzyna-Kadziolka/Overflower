using Overflower.Application.Paging;
using Overflower.Tests.Shared.Seed;

namespace Overflower.IntegrationTests.Controllers;

public class TagsControllerTests : BaseTest {
    private const string Route = "api/tags";

    [Test]
    public async Task GetAll_WhenDataIsCorrect_ShouldBeOk() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        var tags = await ApplicationDbContext.Tags.ToListAsync();
        // Act
        var response = await HttpClient.GetAsync(Route);
        // Assert
        response.Should().Be200Ok();
        var result = await response.Content.DeserializeContentAsync<PageResult>();
        result.Items.Should().BeEquivalentTo(tags);
    }

    [Test]
    public async Task Update_WhenDataIsCorrect_ShouldBeOk() {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<TagSeed>();
        // Act
        var response = await HttpClient.PutAsync(Route, null);
        // Assert
        response.Should().Be200Ok();
    }
}
