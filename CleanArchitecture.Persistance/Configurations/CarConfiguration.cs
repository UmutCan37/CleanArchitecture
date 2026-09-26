using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Model).HasMaxLength(100).IsRequired();

        builder.HasIndex(p => p.Name);
    }
}