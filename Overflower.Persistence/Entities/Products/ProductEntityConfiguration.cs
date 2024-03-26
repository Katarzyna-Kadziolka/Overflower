using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Overflower.Persistence.Entities.Products; 

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity> {
	public void Configure(EntityTypeBuilder<ProductEntity> builder) {
		builder.HasKey(x => x.Id);
	}
}