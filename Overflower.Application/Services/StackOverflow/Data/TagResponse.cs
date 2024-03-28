using Newtonsoft.Json;

namespace Overflower.Application.Services.StackOverflow.Data;

public class TagResponse {
    [JsonProperty("name")]
    public required string Name { get; set; }
    [JsonProperty("count")]
    public int Count { get; set; }
    [JsonProperty("is_required")] 
    public bool IsRequired { get; set; }
    [JsonProperty("is_moderator_only")]
    public bool IsModeratorOnly { get; set; }
    [JsonProperty("has_synonyms")]
    public bool HasSynonyms { get; set; }
}
