using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddingConfigurationsForItemTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_item_tags_items_ItemModelId",
                table: "item_tags");

            migrationBuilder.DropIndex(
                name: "IX_item_tags_ItemModelId",
                table: "item_tags");

            migrationBuilder.DropColumn(
                name: "ItemModelId",
                table: "item_tags");

            migrationBuilder.CreateIndex(
                name: "IX_item_tags_ItemId",
                table: "item_tags",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_item_tags_items_ItemId",
                table: "item_tags",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_item_tags_items_ItemId",
                table: "item_tags");

            migrationBuilder.DropIndex(
                name: "IX_item_tags_ItemId",
                table: "item_tags");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemModelId",
                table: "item_tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_item_tags_ItemModelId",
                table: "item_tags",
                column: "ItemModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_item_tags_items_ItemModelId",
                table: "item_tags",
                column: "ItemModelId",
                principalTable: "items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
