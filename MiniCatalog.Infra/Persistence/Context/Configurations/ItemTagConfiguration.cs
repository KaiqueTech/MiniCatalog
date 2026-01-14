using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Infra.Persistence.Context.Configurations;

public class ItemTagConfiguration : IEntityTypeConfiguration<ItemTagModel>
{
    public void Configure(EntityTypeBuilder<ItemTagModel> builder)
    {
        builder.ToTable("item_tags");


        builder.HasKey(t => t.Id);


        builder.Property(t => t.Tag)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(it => it.Item).WithMany(i => i.Tags)
            .HasForeignKey("ItemId");


        builder.HasIndex(t => t.Tag);
    }
}