using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(3616));

            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(4241));

            migrationBuilder.UpdateData(
                table: "categorias",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(4242));

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(8161));

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(8787));

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 21, 27, 9, 764, DateTimeKind.Utc).AddTicks(8789));
        }
    }
}
