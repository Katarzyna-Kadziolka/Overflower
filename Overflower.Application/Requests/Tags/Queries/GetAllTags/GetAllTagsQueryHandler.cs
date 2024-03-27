using MediatR;
using Microsoft.EntityFrameworkCore;
using Overflower.Application.Services.StackOverflow;
using Overflower.Persistence;
using Overflower.Persistence.Entities.Tags;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, TagDto[]> {
    private readonly ApplicationDbContext _context;
    private readonly IStackOverflowClient _stackOverflowClient;

    public GetAllTagsQueryHandler(ApplicationDbContext context, IStackOverflowClient stackOverflowClient) {
        _context = context;
        _stackOverflowClient = stackOverflowClient;
    }
    public async Task<TagDto[]> Handle(GetAllTagsQuery request, CancellationToken cancellationToken) {
        var tags = await _context.Tags.ToListAsync(cancellationToken);
        if (tags.Count < 1_000) {
            var tagsFromApi = await _stackOverflowClient.GetTagsAsync(1_000);
            var newTags = tagsFromApi.Select(t => new TagEntity {
                Id = Guid.NewGuid(),
                Name = t.Name,
                Count = t.Count,
                HasSynonyms = t.HasSynonyms,
                IsRequired = t.IsRequired,
                IsModeratorOnly = t.IsModeratorOnly
            }).ToList();
            _context.Tags.AddRange(newTags);
            await _context.SaveChangesAsync(cancellationToken);
            tags = newTags;
        }
        
        return tags.Select(t => t.ToDto()).ToArray();
    }
}
