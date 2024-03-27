using MediatR;
using Microsoft.EntityFrameworkCore;
using Overflower.Persistence;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, TagDto[]> {
    private readonly ApplicationDbContext _context;

    public GetAllTagsQueryHandler(ApplicationDbContext context) {
        _context = context;
    }
    public async Task<TagDto[]> Handle(GetAllTagsQuery request, CancellationToken cancellationToken) {
        var tags = await _context.Tags.ToArrayAsync(cancellationToken);
        return tags.Select(t => t.ToDto()).ToArray();
    }
}
