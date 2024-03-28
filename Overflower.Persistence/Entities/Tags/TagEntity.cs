namespace Overflower.Persistence.Entities.Tags;

public class TagEntity {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Count { get; set; }
    public bool IsRequired { get; set; }
    public bool IsModeratorOnly { get; set; }
    public bool HasSynonyms { get; set; }
}
