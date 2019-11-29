using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_Website.Migrations
{
    public partial class ChangedCategoryUrlColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WikiArticles_Url",
                table: "WikiArticles");

            migrationBuilder.DropIndex(
                name: "IX_BlogArticles_Url",
                table: "BlogArticles");

            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "WikiCategories");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "WikiCategories",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "WikiArticles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "BlogArticles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_WikiArticles_Url",
                table: "WikiArticles",
                column: "Url",
                unique: true,
                filter: "[Url] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogArticles_Url",
                table: "BlogArticles",
                column: "Url",
                unique: true,
                filter: "[Url] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WikiArticles_Url",
                table: "WikiArticles");

            migrationBuilder.DropIndex(
                name: "IX_BlogArticles_Url",
                table: "BlogArticles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "WikiCategories");

            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "WikiCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "WikiArticles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "BlogArticles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WikiArticles_Url",
                table: "WikiArticles",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogArticles_Url",
                table: "BlogArticles",
                column: "Url",
                unique: true);
        }
    }
}
