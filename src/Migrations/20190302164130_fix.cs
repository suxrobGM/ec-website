using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_WebSite.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_ForumHeads_ForumId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteThreads_Threads_ThreadId",
                table: "FavoriteThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteThreads_Users_UserId",
                table: "FavoriteThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Boards_BoardId",
                table: "Threads");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_ForumHeads_ForumId",
                table: "Boards",
                column: "ForumId",
                principalTable: "ForumHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteThreads_Threads_ThreadId",
                table: "FavoriteThreads",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteThreads_Users_UserId",
                table: "FavoriteThreads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Boards_BoardId",
                table: "Threads",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_ForumHeads_ForumId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteThreads_Threads_ThreadId",
                table: "FavoriteThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteThreads_Users_UserId",
                table: "FavoriteThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Boards_BoardId",
                table: "Threads");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_ForumHeads_ForumId",
                table: "Boards",
                column: "ForumId",
                principalTable: "ForumHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteThreads_Threads_ThreadId",
                table: "FavoriteThreads",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteThreads_Users_UserId",
                table: "FavoriteThreads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Boards_BoardId",
                table: "Threads",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
