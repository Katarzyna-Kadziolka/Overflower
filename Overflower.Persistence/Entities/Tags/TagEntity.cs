using System.Text.Json.Serialization;

namespace Overflower.Persistence.Entities.Tags;

public class TagEntity {
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("is_required")] 
    public bool IsRequired { get; set; }
    [JsonPropertyName("is_moderator_only")]
    public bool IsModeratorOnly { get; set; }
    [JsonPropertyName("has_synonyms")]
    public bool HasSynonyms { get; set; }
}
