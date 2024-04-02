using FluentAssertions;
using Overflower.Application.Requests.Tags;
using Overflower.Persistence.Entities.Tags;
using Overflower.Tests.Shared.Generators;

namespace Overflower.UnitTests.Requests.Tags;

public class TagMapperTests {
    [Test]
    [TestCase(10)]
    [TestCase(100)]
    [TestCase(1000)]
    public void ToDto_ShouldReturnTagDto(long totalCount) {
        // Arrange
        TagEntity tagEntity = new TagGenerator().Generate();
        var expectedPercentageCount = (double)tagEntity.Count / totalCount * 100;
        // Act
        var tagDto = tagEntity.ToDto(totalCount);
        // Assert
        tagDto.Id.Should().Be(tagEntity.Id);
        tagDto.Name.Should().Be(tagEntity.Name);
        tagDto.Count.Should().Be(tagEntity.Count);
        tagDto.HasSynonyms.Should().Be(tagEntity.HasSynonyms);
        tagDto.IsRequired.Should().Be(tagEntity.IsRequired);
        tagDto.IsModeratorOnly.Should().Be(tagEntity.IsModeratorOnly);
        tagDto.PercentageShare.Should().Be(expectedPercentageCount);
    }
    
    [Test]
    public void ToDto_TotalCountEqualZero_ShouldReturnTagDtoWithPercentageShareEqualZero() {
        // Arrange
        TagEntity tagEntity = new TagGenerator().Generate();
        var expectedPercentageCount = 0;
        // Act
        var tagDto = tagEntity.ToDto(0);
        // Assert
        tagDto.Id.Should().Be(tagEntity.Id);
        tagDto.Name.Should().Be(tagEntity.Name);
        tagDto.Count.Should().Be(tagEntity.Count);
        tagDto.HasSynonyms.Should().Be(tagEntity.HasSynonyms);
        tagDto.IsRequired.Should().Be(tagEntity.IsRequired);
        tagDto.IsModeratorOnly.Should().Be(tagEntity.IsModeratorOnly);
        tagDto.PercentageShare.Should().Be(expectedPercentageCount);
    }
}
