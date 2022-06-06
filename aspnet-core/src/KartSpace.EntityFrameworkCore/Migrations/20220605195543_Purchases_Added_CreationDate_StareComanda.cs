using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartSpace.Migrations
{
    public partial class Purchases_Added_CreationDate_StareComanda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StareComanda",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "StareComanda",
                table: "Purchases");
        }
    }
}
