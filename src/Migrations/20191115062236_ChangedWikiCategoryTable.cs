using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_Website.Migrations
{
    public partial class ChangedWikiCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_Categories_CategoryId",
                table: "ArticleCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "WikiCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WikiCategories",
                table: "WikiCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_WikiCategories_CategoryId",
                table: "ArticleCategory",
                column: "CategoryId",
                principalTable: "WikiCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_WikiCategories_CategoryId",
                table: "ArticleCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WikiCategories",
                table: "WikiCategories");

            migrationBuilder.RenameTable(
                name: "WikiCategories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_Categories_CategoryId",
                table: "ArticleCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
