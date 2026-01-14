using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Infra.Persistence.Context.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<CategoriaModel>
{
    public void Configure(EntityTypeBuilder<CategoriaModel> builder)
    {
        builder.ToTable("categorias");


        builder.HasKey(c => c.Id);


        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(150);


        builder.HasIndex(c => c.Nome).IsUnique();


        builder.Property(c => c.Ativa)
            .HasDefaultValue(true);
    }
}