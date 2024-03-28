using Newtonsoft.Json;

namespace Overflower.Application.Services.StackOverflow.Data;

public class GetTagResponse {
    [JsonProperty("items")] 
    public required ICollection<TagResponse> Tags { get; set; }
}
