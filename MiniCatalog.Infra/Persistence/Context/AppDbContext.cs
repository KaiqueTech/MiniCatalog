using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context.Seed;

namespace MiniCatalog.Infra.Persistence.Context;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<UserModel> Profiles { get; set; }
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