using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_WebSite.Migrations
{
    public partial class add_header_photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderPhotoId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HeaderPhotoId",
                table: "Users",
                column: "HeaderPhotoId",
                unique: true,
                filter: "[HeaderPhotoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Medias_HeaderPhotoId",
                table: "Users",
                column: "HeaderPhotoId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Medias_HeaderPhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HeaderPhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HeaderPhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");
        }
    }
}
