using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_WebSite.Migrations
{
    public partial class changed_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skills_SkillId",
                table: "UserSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Users_UserId",
                table: "UserSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill");

            migrationBuilder.RenameTable(
                name: "UserSkill",
                newName: "UserSkills");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_UserId",
                table: "UserSkills",
                newName: "IX_UserSkills_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills",
                columns: new[] { "SkillId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_Skills_SkillId",
                table: "UserSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_Users_UserId",
                table: "UserSkills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_Skills_SkillId",
                table: "UserSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_Users_UserId",
                table: "UserSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills");

            migrationBuilder.RenameTable(
                name: "UserSkills",
                newName: "UserSkill");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkill",
                newName: "IX_UserSkill_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill",
                columns: new[] { "SkillId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skills_SkillId",
                table: "UserSkill",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Users_UserId",
                table: "UserSkill",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
