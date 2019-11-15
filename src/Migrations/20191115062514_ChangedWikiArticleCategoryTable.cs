using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_Website.Migrations
{
    public partial class ChangedWikiArticleCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_WikiArticles_ArticleId",
                table: "ArticleCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_WikiCategories_CategoryId",
                table: "ArticleCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCategory",
                table: "ArticleCategory");

            migrationBuilder.RenameTable(
                name: "ArticleCategory",
                newName: "WikiArticleCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleCategory_CategoryId",
                table: "WikiArticleCategory",
                newName: "IX_WikiArticleCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WikiArticleCategory",
                table: "WikiArticleCategory",
                columns: new[] { "ArticleId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WikiArticleCategory_WikiArticles_ArticleId",
                table: "WikiArticleCategory",
                column: "ArticleId",
                principalTable: "WikiArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WikiArticleCategory_WikiCategories_CategoryId",
                table: "WikiArticleCategory",
                column: "CategoryId",
                principalTable: "WikiCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WikiArticleCategory_WikiArticles_ArticleId",
                table: "WikiArticleCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_WikiArticleCategory_WikiCategories_CategoryId",
                table: "WikiArticleCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WikiArticleCategory",
                table: "WikiArticleCategory");

            migrationBuilder.RenameTable(
                name: "WikiArticleCategory",
                newName: "ArticleCategory");

            migrationBuilder.RenameIndex(
                name: "IX_WikiArticleCategory_CategoryId",
                table: "ArticleCategory",
                newName: "IX_ArticleCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCategory",
                table: "ArticleCategory",
                columns: new[] { "ArticleId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_WikiArticles_ArticleId",
                table: "ArticleCategory",
                column: "ArticleId",
                principalTable: "WikiArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_WikiCategories_CategoryId",
                table: "ArticleCategory",
                column: "CategoryId",
                principalTable: "WikiCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
