using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationsWithMongo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Tag",
                value: "Windows");

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "ItemId", "Tag" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), "Intel" });

            migrationBuilder.InsertData(
                table: "item_tags",
                columns: new[] { "Id", "CreatedAt", "ItemId", "Tag", "UpdatedAt" },
                values: new object[] { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("66666666-6666-6666-6666-666666666666"), "Avançado", null });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CategoriaId", "Descricao", "Nome", "Preco" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "iPhone 14", "Smartphone", 8000m });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "Id", "Ativo", "CategoriaId", "CreatedAt", "Descricao", "Nome", "Preco", "UpdatedAt" },
                values: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), true, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Padrões de projeto", "Livro C# Avançado", 120m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Tag",
                value: "Intel");

            migrationBuilder.UpdateData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "ItemId", "Tag" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "iOS" });

            migrationBuilder.InsertData(
                table: "item_tags",
                columns: new[] { "Id", "CreatedAt", "ItemId", "Tag", "UpdatedAt" },
                values: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("44444444-4444-4444-4444-444444444444"), "Windows", null });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CategoriaId", "Descricao", "Nome", "Preco" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Padrões de projeto", "Livro C# Avançado", 120m });
        }
    }
}
