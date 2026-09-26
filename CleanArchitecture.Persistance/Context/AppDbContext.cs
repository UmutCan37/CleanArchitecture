using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Added)
                entry.Property(p => p.CreatedDate).CurrentValue = DateTime.UtcNow;

            if (entry.State == EntityState.Modified)
                entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}