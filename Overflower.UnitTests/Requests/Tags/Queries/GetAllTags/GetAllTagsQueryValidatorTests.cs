using FluentAssertions;
using FluentValidation.TestHelper;
using Overflower.Application.Paging;
using Overflower.Application.Requests.Tags;
using Overflower.Application.Requests.Tags.Queries.GetAllTags;

namespace Overflower.UnitTests.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQueryValidatorTests {
    private readonly GetAllTagsQueryValidator _validator = new();
    
    [Test]
    public void Validate_ValidQuery_ShouldReturnValidValidationResult() {
        // Arrange
        var request = new GetAllTagsQuery {
            PageSize = 1,
            Page = 1,
            TagSortBy = TagSortBy.Name,
            SortingOrder = SortingOrder.Ascending
        };
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Test]
    [TestCase(0)]
    [TestCase(-10)]
    public void Validate_Page_ShouldReturnInvalidValidationResult(int page) {
        // Arrange
        var request = new GetAllTagsQuery {
            PageSize = 1,
            Page = page,
            TagSortBy = TagSortBy.Name,
            SortingOrder = SortingOrder.Ascending
        };
        // Act
        var result = _validator.TestValidate(request);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Page);
    }
    
    [Test]
    [TestCase(0)]
    [TestCase(-10)]
    [TestCase(1_001)]
    [TestCase(2_000)]
    public void Validate_PageSize_ShouldReturnInvalidValidationResult(int pageSize) {
        // Arrange
        var request = new GetAllTagsQuery {
            PageSize = pageSize,
            Page = 1,
            TagSortBy = TagSortBy.Name,
            SortingOrder = SortingOrder.Ascending
        };
        // Act
        var result = _validator.TestValidate(request);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }
}
