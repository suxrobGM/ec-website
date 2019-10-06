using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_Website.Migrations
{
    public partial class DropWikiCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WikiArticleCategory");

            migrationBuilder.DropTable(
                name: "ArticlesCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticlesCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WikiArticleCategory",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WikiArticleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiArticleCategory", x => new { x.CategoryId, x.WikiArticleId });
                    table.ForeignKey(
                        name: "FK_WikiArticleCategory_ArticlesCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ArticlesCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WikiArticleCategory_WikiArticles_WikiArticleId",
                        column: x => x.WikiArticleId,
                        principalTable: "WikiArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WikiArticleCategory_WikiArticleId",
                table: "WikiArticleCategory",
                column: "WikiArticleId");
        }
    }
}
