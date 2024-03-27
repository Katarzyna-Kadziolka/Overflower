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
        //tu chce pobrać 1000 tagów zze stackobverfloww jeśli nie ma i wrzucić do bazki i to pewnie będzie srwis
        var tags = await _context.Tags.ToArrayAsync(cancellationToken);
        return tags.Select(t => t.ToDto()).ToArray();
    }
}
