using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class AddRecu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recu_Paiement",
                table: "Facture");

            migrationBuilder.AddColumn<int>(
                name: "Recu_PaiementId",
                table: "Facture",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Recu_Paiement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<int>(type: "int", nullable: false),
                    Chemin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recu_Paiement", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Recu_PaiementId",
                table: "Facture",
                column: "Recu_PaiementId",
                unique: true,
                filter: "[Recu_PaiementId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_Recu_Paiement_Recu_PaiementId",
                table: "Facture",
                column: "Recu_PaiementId",
                principalTable: "Recu_Paiement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facture_Recu_Paiement_Recu_PaiementId",
                table: "Facture");

            migrationBuilder.DropTable(
                name: "Recu_Paiement");

            migrationBuilder.DropIndex(
                name: "IX_Facture_Recu_PaiementId",
                table: "Facture");

            migrationBuilder.DropColumn(
                name: "Recu_PaiementId",
                table: "Facture");

            migrationBuilder.AddColumn<byte[]>(
                name: "Recu_Paiement",
                table: "Facture",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
