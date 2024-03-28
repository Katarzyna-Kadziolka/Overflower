using Overflower.Persistence.Entities.Tags;

namespace Overflower.Application.Requests.Tags.Queries;

public static class TagMapper {
    public static TagDto ToDto(this TagEntity tag) {
        return new TagDto {
            Id = tag.Id,
            Name = tag.Name,
            Count = tag.Count,
            HasSynonyms = tag.HasSynonyms,
            IsRequired = tag.IsRequired,
            IsModeratorOnly = tag.IsModeratorOnly
        };
    }
}
