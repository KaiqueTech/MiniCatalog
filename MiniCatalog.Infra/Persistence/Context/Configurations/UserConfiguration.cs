using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Infra.Persistence.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("profiles");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.IdentityId)
            .IsRequired()
            .HasMaxLength(450);
        
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.DateOfBirth)
            .IsRequired()
            .HasColumnType("date");
    }
}