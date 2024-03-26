using Newtonsoft.Json;
using Overflower.Api.Configuration.JsonSerilizer;

namespace Overflower.Tests.Shared.Extensions;

public static class HttpContentExtensions
{
    private static readonly JsonSerializerSettings JsonSerializerSettings;
    
    static HttpContentExtensions()
    {
        JsonSerializerSettings = new JsonSerializerSettings();
        JsonSerializerSettings.AddJsonSettings();
    }
    
    public static async Task<T?> DeserializeContentAsync<T>(this HttpContent content)
    {
        var stringContent = await content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(stringContent, JsonSerializerSettings);
    }
}