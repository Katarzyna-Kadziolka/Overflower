using Bogus;
using Overflower.Persistence.Entities.Tags;

namespace Overflower.Tests.Shared.Generators;

public sealed class TagGenerator : Faker<TagEntity> {
    public TagGenerator() {
        RuleFor(o => o.Id, Guid.NewGuid);
        RuleFor(o => o.Name, f => f.Lorem.Word());
        RuleFor(o => o.Count, f => f.Random.Int());
        RuleFor(o => o.HasSynonyms, f => f.Random.Bool());
        RuleFor(o => o.IsRequired, f => f.Random.Bool());
        RuleFor(o => o.IsModeratorOnly, f => f.Random.Bool());
    }
}
