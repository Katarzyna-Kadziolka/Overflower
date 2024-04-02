using Overflower.Application.Requests.Tags;

namespace Overflower.Application.Paging;

public class PageResult {
    public ICollection<TagDto> Items { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
