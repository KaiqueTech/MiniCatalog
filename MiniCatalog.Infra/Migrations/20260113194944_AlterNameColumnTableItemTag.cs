using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterNameColumnTableItemTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "item_tags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "item_tags");
        }
    }
}
