using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_WebSite.Migrations
{
    public partial class added_favorite_threads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteThreads",
                columns: table => new
                {
                    ThreadId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteThreads", x => new { x.ThreadId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavoriteThreads_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteThreads_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteThreads_UserId",
                table: "FavoriteThreads",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteThreads");
        }
    }
}
