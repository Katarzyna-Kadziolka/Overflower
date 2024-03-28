using Overflower.Persistence;
using Overflower.Tests.Shared.Generators;

namespace Overflower.Tests.Shared.Seed;

public class TagSeed : BaseSeed {
    public TagSeed(ApplicationDbContext context) : base(context) { }

    public override async Task SeedAsync() {
        var tags = new TagGenerator()
            .Generate(1000);
        Context.Tags.AddRange(tags);
        await Context.SaveChangesAsync();
    }
}
