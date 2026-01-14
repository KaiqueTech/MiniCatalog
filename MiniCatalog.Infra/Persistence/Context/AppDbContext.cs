using Microsoft.EntityFrameworkCore;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Infra.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<ItemModel>  Items { get; set; }
    public DbSet<ItemTagModel> ItemTags { get; set; }
    public DbSet<CategoriaModel>  Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        SeedData.Seed(modelBuilder);
    }
}