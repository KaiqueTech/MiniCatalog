using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "item_tags",
                columns: new[] { "Id", "CreatedAt", "ItemId", "Tag", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("44444444-4444-4444-4444-444444444444"), "Windows", null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("44444444-4444-4444-4444-444444444444"), "Intel", null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("55555555-5555-5555-5555-555555555555"), "iOS", null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("55555555-5555-5555-5555-555555555555"), "Apple", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("66666666-6666-6666-6666-666666666666"), "C#", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("66666666-6666-6666-6666-666666666666"), "Avançado", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "item_tags",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));
        }
    }
}
