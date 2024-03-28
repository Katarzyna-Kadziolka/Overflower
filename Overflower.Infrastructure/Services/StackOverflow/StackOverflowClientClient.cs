using Overflower.Application.Services.StackOverflow;
using Overflower.Application.Services.StackOverflow.Data;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Overflower.Infrastructure.Services.StackOverflow;

public class StackOverflowClientClient : IStackOverflowClient {
    private readonly RestClient _client;

    public StackOverflowClientClient() {
        _client = new RestClient(
            "https://api.stackexchange.com/2.3", 
            configureSerialization: s => s.UseNewtonsoftJson());
    }
    public async Task<ICollection<TagResponse>> GetTagsAsync(int tagAmount) {
        var responseTasks = new List<Task<GetTagResponse>>();
        var requestCount = tagAmount / 100;
        var lastPageSize = tagAmount % 100;
        if (lastPageSize != 0) requestCount++;
        for (int i = 0; i < requestCount; i++) {
            var amount = 100;
            if (i == requestCount - 1) amount = lastPageSize;
            var request = new RestRequest("tags")
                        .AddQueryParameter("page", i+1)
                        .AddQueryParameter("pagesize", amount)
                        .AddQueryParameter("order", "desc")
                        .AddQueryParameter("sort", "popular")
                        .AddQueryParameter("site", "stackoverflow");
            var task = _client.GetAsync<GetTagResponse>(request);
            responseTasks.Add(task!);
        }

        var response = await Task.WhenAll(responseTasks);

        return response.SelectMany(a => a.Tags).ToList();
    }
}
