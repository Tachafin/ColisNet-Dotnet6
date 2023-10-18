using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class Villeinboutique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VilleId",
                table: "Boutique",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boutique_VilleId",
                table: "Boutique",
                column: "VilleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boutique_Ville_VilleId",
                table: "Boutique",
                column: "VilleId",
                principalTable: "Ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boutique_Ville_VilleId",
                table: "Boutique");

            migrationBuilder.DropIndex(
                name: "IX_Boutique_VilleId",
                table: "Boutique");

            migrationBuilder.DropColumn(
                name: "VilleId",
                table: "Boutique");
        }
    }
}
