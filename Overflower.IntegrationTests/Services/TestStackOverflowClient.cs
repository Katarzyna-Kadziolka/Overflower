using Overflower.Application.Services.StackOverflow;
using Overflower.Application.Services.StackOverflow.Data;
using Overflower.Tests.Shared.Generators;

namespace Overflower.IntegrationTests.Services;

public class TestStackOverflowClient : IStackOverflowClient {
    public Task<ICollection<TagResponse>> GetTagsAsync(int tagAmount) {
        var generator = new TagGenerator();
        var tags = generator.Generate(tagAmount).Select(x => new TagResponse {
            Name = x.Name,
            Count = x.Count,
            HasSynonyms = x.HasSynonyms,
            IsRequired = x.IsRequired,
            IsModeratorOnly = x.IsModeratorOnly
        }).ToList();
        return Task.FromResult<ICollection<TagResponse>>(tags);
    }
}
