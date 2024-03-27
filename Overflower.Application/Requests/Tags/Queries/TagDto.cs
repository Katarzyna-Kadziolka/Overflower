namespace Overflower.Application.Requests.Tags.Queries;

public class TagDto {
    public string Name { get; set; }
    public int Count { get; set; }
    public bool IsRequired { get; set; }
    public bool IsModeratorOnly { get; set; }
    public bool HasSynonyms { get; set; }
}
