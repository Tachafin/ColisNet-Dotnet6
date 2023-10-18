using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class Fact2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FactureId",
                table: "Colis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colis_FactureId",
                table: "Colis",
                column: "FactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colis_Facture_FactureId",
                table: "Colis",
                column: "FactureId",
                principalTable: "Facture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colis_Facture_FactureId",
                table: "Colis");

            migrationBuilder.DropIndex(
                name: "IX_Colis_FactureId",
                table: "Colis");

            migrationBuilder.DropColumn(
                name: "FactureId",
                table: "Colis");
        }
    }
}
