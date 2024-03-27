using Bogus;
using Overflower.Persistence;
using Overflower.Persistence.Entities.Tags;

namespace Overflower.Tests.Shared.Seed;

public class TagSeed : BaseSeed {
    public TagSeed(ApplicationDbContext context) : base(context) { }

    public override async Task SeedAsync() {
        var tags = new Faker<TagEntity>()
            .RuleFor(o => o.Name, f => f.Lorem.Word())
            .RuleFor(o => o.Count, f => f.Random.Int())
            .RuleFor(o => o.HasSynonyms, f => f.Random.Bool())
            .RuleFor(o => o.IsRequired, f => f.Random.Bool())
            .RuleFor(o => o.IsModeratorOnly, f => f.Random.Bool())
            .Generate(10);
        Context.Tags.AddRange(tags);
        await Context.SaveChangesAsync();
    }
}
