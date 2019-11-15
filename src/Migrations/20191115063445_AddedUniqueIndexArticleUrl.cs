using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_Website.Migrations
{
    public partial class AddedUniqueIndexArticleUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "WikiArticles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "BlogArticles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WikiArticles_Url",
                table: "WikiArticles");

            migrationBuilder.DropIndex(
                name: "IX_BlogArticles_Url",
                table: "BlogArticles");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "WikiArticles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "BlogArticles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
