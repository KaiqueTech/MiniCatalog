using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCatalog.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterNameTableCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_categories_CategoriaId",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "categorias");

            migrationBuilder.RenameIndex(
                name: "IX_categories_Nome",
                table: "categorias",
                newName: "IX_categorias_Nome");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categorias",
                table: "categorias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_categorias_CategoriaId",
                table: "items",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_categorias_CategoriaId",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categorias",
                table: "categorias");

            migrationBuilder.RenameTable(
                name: "categorias",
                newName: "categories");

            migrationBuilder.RenameIndex(
                name: "IX_categorias_Nome",
                table: "categories",
                newName: "IX_categories_Nome");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_categories_CategoriaId",
                table: "items",
                column: "CategoriaId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
