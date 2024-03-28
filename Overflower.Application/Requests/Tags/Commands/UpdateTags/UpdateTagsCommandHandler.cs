using MediatR;
using Microsoft.EntityFrameworkCore;
using Overflower.Application.Services.StackOverflow;
using Overflower.Persistence;
using Overflower.Persistence.Entities.Tags;

namespace Overflower.Application.Requests.Tags.Commands.UpdateTags;

public class UpdateTagsCommandHandler : IRequestHandler<UpdateTagsCommand> {
    private readonly ApplicationDbContext _context;
    private readonly IStackOverflowClient _stackOverflowClient;

    public UpdateTagsCommandHandler(ApplicationDbContext context, IStackOverflowClient stackOverflowClient) {
        _context = context;
        _stackOverflowClient = stackOverflowClient;
    }
    public async Task Handle(UpdateTagsCommand request, CancellationToken cancellationToken) {
        await _context.Tags.ExecuteDeleteAsync(cancellationToken);
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
    }
}
