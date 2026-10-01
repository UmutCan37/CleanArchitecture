using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Configurations
{
    public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => new { p.UserId, p.RoleId }).IsUnique();

            builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Role).WithMany().HasForeignKey(p => p.RoleId);
        }
    }
}
