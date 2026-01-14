using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TestSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "Id", "Ativa", "CreatedAt", "Descricao", "Nome", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(3616), "Produtos eletrônicos", "Eletrônicos", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(4241), "Todos os tipos de livros", "Livros", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(4242), "Serviços diversos", "Serviços", null }
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "Id", "Ativo", "CategoriaId", "CreatedAt", "Descricao", "Nome", "Preco", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), true, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(8161), "Dell 16GB", "Notebook", 4500m, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), true, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(8787), "iPhone 14", "Smartphone", 8000m, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), true, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(8789), "Padrões de projeto", "Livro C# Avançado", 120m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
