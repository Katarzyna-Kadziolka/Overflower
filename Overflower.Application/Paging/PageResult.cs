namespace Overflower.Application.Paging;

public class PageResult<T> where T : class {
    public ICollection<T> Items { get; set; } = Array.Empty<T>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
