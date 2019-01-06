using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_WebSite.Migrations
{
    public partial class added_locked_thread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Threads",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Threads");
        }
    }
}
