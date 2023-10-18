using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class EditColisAddDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Livraison",
                table: "Colis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Ramassage",
                table: "Colis",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date_Livraison",
                table: "Colis");

            migrationBuilder.DropColumn(
                name: "Date_Ramassage",
                table: "Colis");
        }
    }
}
