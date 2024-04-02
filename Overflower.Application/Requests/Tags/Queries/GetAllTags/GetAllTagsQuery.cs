using MediatR;
using Overflower.Application.Paging;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQuery : IRequest<PageResult<TagDto>>, IPaging {
    public required int Page { get; set; }
    public required int PageSize { get; set; }
    public TagSortBy TagSortBy { get; set; } = TagSortBy.None;
    public SortingOrder SortingOrder { get; set; } = SortingOrder.Ascending;
}
