using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Infra.Persistence.Context.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<ItemModel>
{
    public void Configure(EntityTypeBuilder<ItemModel> builder)
    {
        builder.ToTable("items");


        builder.HasKey(i => i.Id);


        builder.Property(i => i.Nome)
            .IsRequired()
            .HasMaxLength(200);


        builder.HasIndex(i => i.Nome);


        builder.Property(i => i.Preco)
            .HasPrecision(18, 2);


        builder.HasOne(i => i.Categoria)
            .WithMany()
            .HasForeignKey(i => i.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasMany(i => i.Tags)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}