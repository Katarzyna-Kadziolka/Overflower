using MediatR;
using Overflower.Application.Paging;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQuery : IRequest<PageResult<TagDto>>, IPaging {
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public TagSortBy TagSortBy { get; set; } = TagSortBy.None;
    public SortingOrder SortingOrder { get; set; } = SortingOrder.Ascending;
}
