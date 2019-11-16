using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EC_Website.Migrations
{
    public partial class AddedSomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanPeriod",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "WikiCategories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BanExpirationDate",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "WikiCategories");

            migrationBuilder.DropColumn(
                name: "BanExpirationDate",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "BanPeriod",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
