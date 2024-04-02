using MediatR;
using Microsoft.EntityFrameworkCore;
using Overflower.Application.Paging;
using Overflower.Application.Services.StackOverflow;
using Overflower.Persistence;
using Overflower.Persistence.Entities.Tags;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, PageResult> {
    private readonly ApplicationDbContext _context;
    private readonly IStackOverflowClient _stackOverflowClient;

    public GetAllTagsQueryHandler(ApplicationDbContext context, IStackOverflowClient stackOverflowClient) {
        _context = context;
        _stackOverflowClient = stackOverflowClient;
    }
    public async Task<PageResult> Handle(GetAllTagsQuery request, CancellationToken cancellationToken) {
        var tagsCount = _context.Tags.Count();
        if (tagsCount < 1_000) {
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
            tagsCount = _context.Tags.Count();
        }
        
        var totalPages = tagsCount / request.PageSize;
        if (tagsCount % request.PageSize != 0) totalPages++;

        if (request.Page > totalPages)
            return new PageResult {
                CurrentPage = request.Page,
                TotalPages = totalPages,
                Items = new List<TagDto>()
            };

        IQueryable<TagEntity> query = _context.Tags;
        if (request.TagSortBy == TagSortBy.Name) {
            if (request.SortingOrder == SortingOrder.Ascending) query = query.OrderBy(o => o.Name);
            else query = query.OrderByDescending(o => o.Name);
        }

        var tags = await query
            .Skip(request.PageSize * request.Page - 1)
            .Take(request.PageSize)
            .Select(t => t.ToDto())
            .ToListAsync(cancellationToken);


        return new PageResult {
            Items = tags,
            CurrentPage = request.Page,
            TotalPages = totalPages
        };
    }
}
