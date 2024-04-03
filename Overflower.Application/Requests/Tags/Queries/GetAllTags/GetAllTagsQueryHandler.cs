using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Overflower.Application.Extensions;
using Overflower.Application.Paging;
using Overflower.Application.Services.StackOverflow;
using Overflower.Persistence;
using Overflower.Persistence.Entities.Tags;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, PageResult<TagDto>> {
    private readonly ApplicationDbContext _context;
    private readonly IStackOverflowClient _stackOverflowClient;

    public GetAllTagsQueryHandler(ApplicationDbContext context, IStackOverflowClient stackOverflowClient) {
        _context = context;
        _stackOverflowClient = stackOverflowClient;
    }
    public async Task<PageResult<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken) {
        var tagsCount = await _context.Tags.CountAsync(cancellationToken);
        if (tagsCount < 1_000) {
            var newTags = await GetTagsFromStackOverflowAsync();
            _context.Tags.AddRange(newTags);
            await _context.SaveChangesAsync(cancellationToken);
            tagsCount += newTags.Count;
        }
        
        var totalPages = GetTotalPages(request, tagsCount);

        IQueryable<TagEntity> query = _context.Tags;
        if (request.TagSortBy != TagSortBy.None) {
            query = OrderBy(request, query);
        }

        var totalTagCount = await _context.Tags.SumAsync(x => (long) x.Count, cancellationToken);
        var tags = await query
            .GetPage(request.Page, request.PageSize)
            .Select(t => t.ToDto(totalTagCount))
            .ToListAsync(cancellationToken);
        
        return new PageResult<TagDto> {
            Items = tags,
            CurrentPage = request.Page,
            TotalPages = totalPages
        };
    }

    private async Task<List<TagEntity>> GetTagsFromStackOverflowAsync() {
        var tagsFromApi = await _stackOverflowClient.GetTagsAsync(1_000);
        var newTags = tagsFromApi.Select(t => new TagEntity {
            Id = Guid.NewGuid(),
            Name = t.Name,
            Count = t.Count,
            HasSynonyms = t.HasSynonyms,
            IsRequired = t.IsRequired,
            IsModeratorOnly = t.IsModeratorOnly
        }).ToList();
        return newTags;
    }

    private static int GetTotalPages(GetAllTagsQuery request, int tagsCount) {
        var totalPages = tagsCount / request.PageSize;
        if (tagsCount % request.PageSize != 0) totalPages++;
        return totalPages;
    }

    private static IQueryable<TagEntity> OrderBy(GetAllTagsQuery request, IQueryable<TagEntity> query) {
        if (request.SortingOrder == SortingOrder.Ascending) 
            query = query.OrderBy(SortBy(request.TagSortBy));
        else 
            query = query.OrderByDescending(SortBy(request.TagSortBy));
        return query;
    }

    private static Expression<Func<TagEntity, object>> SortBy(TagSortBy sortBy) {
        switch (sortBy) {
            case TagSortBy.None:
                return o => o;
            case TagSortBy.Name:
                return o => o.Name;
            default:
                throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, null);
        }
    }
}
