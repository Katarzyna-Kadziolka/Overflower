using Overflower.Application.Services.StackOverflow.Data;

namespace Overflower.Application.Services.StackOverflow;

public interface IStackOverflowClient {
    Task<ICollection<TagResponse>> GetTagsAsync(int tagAmount);
}
