using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_WebSite.Migrations
{
    public partial class added_cover_photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPhotoId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CoverPhotoId",
                table: "Articles",
                column: "CoverPhotoId",
                unique: true,
                filter: "[CoverPhotoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Medias_CoverPhotoId",
                table: "Articles",
                column: "CoverPhotoId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Medias_CoverPhotoId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CoverPhotoId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CoverPhotoId",
                table: "Articles");
        }
    }
}
