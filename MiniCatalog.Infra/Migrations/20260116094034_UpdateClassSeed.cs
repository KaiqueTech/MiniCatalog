using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClassSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Dispositivos eletrônicos", "Dispositivos" });

            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Vestuário feminino importado", "women's clothing" });

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "ItemId", "Tag" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Apple" });

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ItemId", "Tag" },
                values: new object[] { new Guid("66666666-6666-6666-6666-666666666666"), "C#" });

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "Tag",
                value: "Avançado");

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CategoriaId", "Descricao", "Nome", "Preco" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Padrões de projeto e arquitetura", "Livro C# Avançado", 120m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Produtos eletrônicos", "Eletrônicos" });

            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Serviços diversos", "Serviços" });

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "ItemId", "Tag" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), "Intel" });

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ItemId", "Tag" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Apple" });

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "Tag",
                value: "C#");

            migrationBuilder.InsertData(
                table: "item_tags",
                columns: new[] { "Id", "CreatedAt", "ItemId", "Tag", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("66666666-6666-6666-6666-666666666666"), "Avançado", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("66666666-6666-6666-6666-666666666666"), "Avançado", null }
                });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CategoriaId", "Descricao", "Nome", "Preco" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "TV de tela plana 50 polegadas", "TV 50 polegadas", 10000m });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "Id", "Ativo", "CategoriaId", "CreatedAt", "Descricao", "Nome", "Preco", "UpdatedAt" },
                values: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), true, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Padrões de projeto", "Livro C# Avançado", 120m, null });
        }
    }
}
