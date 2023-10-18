using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class AddRecuPaiement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Recu_Paiement",
                table: "Facture",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recu_Paiement",
                table: "Facture");
        }
    }
}
