using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Overflower.Persistence.Entities.Tags;

namespace Overflower.Persistence;

public class ApplicationDbContext : DbContext {
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<TagEntity> Tags => Set<TagEntity>();

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}
