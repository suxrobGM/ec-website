using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_Website.Infrastructure.Migrations
{
    public partial class AddIsLockedPropertyToBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Board",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Board");
        }
    }
}
