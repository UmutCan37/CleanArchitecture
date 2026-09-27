using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Context;

public sealed class AppDbContext : IdentityDbContext<User,IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();

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