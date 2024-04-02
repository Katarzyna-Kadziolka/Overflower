using Overflower.Persistence.Entities.Tags;

namespace Overflower.Application.Requests.Tags;

public static class TagMapper {
    public static TagDto ToDto(this TagEntity tag, long totalCount) {
        double percentageShare = 0;
        if (totalCount != 0) percentageShare = (double)tag.Count / totalCount * 100;
        return new TagDto {
            Id = tag.Id,
            Name = tag.Name,
            Count = tag.Count,
            HasSynonyms = tag.HasSynonyms,
            IsRequired = tag.IsRequired,
            IsModeratorOnly = tag.IsModeratorOnly,
            PercentageShare = percentageShare
        };
    }
}
