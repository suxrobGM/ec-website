using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_WebSite.Migrations
{
    public partial class added_user_skills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Users_OwnerId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_OwnerId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SkillId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => new { x.SkillId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSkill_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkill_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_UserId",
                table: "UserSkill",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Skills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_OwnerId",
                table: "Skills",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Users_OwnerId",
                table: "Skills",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
