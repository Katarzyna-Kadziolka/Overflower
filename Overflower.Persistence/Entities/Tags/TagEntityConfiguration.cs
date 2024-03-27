using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Overflower.Persistence.Entities.Tags;

public class TagEntityConfiguration : IEntityTypeConfiguration<TagEntity> {
    public void Configure(EntityTypeBuilder<TagEntity> builder) {
        builder.HasKey(x => x.Id);
    }
}
