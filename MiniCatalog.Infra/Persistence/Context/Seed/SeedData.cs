using Microsoft.EntityFrameworkCore;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Infra.Persistence.Context.Seed;

public static class SeedData
{
    public static void Seed(ModelBuilder builder)
    {
        
        var catDispositivosId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var catLivrosId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var catWomensClothingId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        
        var itemNotebookId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var itemSmartphoneId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var itemLivroId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        
        builder.Entity<CategoriaModel>().HasData(
            new
            {
                Id = catDispositivosId,
                Nome = "Dispositivos",
                Descricao = "Dispositivos eletrônicos",
                Ativa = true,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = catLivrosId,
                Nome = "Livros",
                Descricao = "Todos os tipos de livros",
                Ativa = true,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = catWomensClothingId,
                Nome = "women's clothing",
                Descricao = "Vestuário feminino importado",
                Ativa = true,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            }
        );
        
        builder.Entity<ItemModel>().HasData(
            new
            {
                Id = itemNotebookId,
                Nome = "Notebook",
                Descricao = "Dell 16GB",
                CategoriaId = catDispositivosId,
                Preco = 4500m,
                Ativo = true,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = itemSmartphoneId,
                Nome = "Smartphone",
                Descricao = "iPhone 14",
                CategoriaId = catDispositivosId,
                Preco = 8000m,
                Ativo = true,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = itemLivroId,
                Nome = "Livro C# Avançado",
                Descricao = "Padrões de projeto e arquitetura",
                CategoriaId = catLivrosId,
                Preco = 120m,
                Ativo = true,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            }
        );
        
        builder.Entity<ItemTagModel>().HasData(
            new
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), 
                Tag = "Windows",
                ItemId = itemNotebookId,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Tag = "Apple",
                ItemId = itemSmartphoneId,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Tag = "C#",
                ItemId = itemLivroId,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Tag = "Avançado",
                ItemId = itemLivroId,
                CreatedAt = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}