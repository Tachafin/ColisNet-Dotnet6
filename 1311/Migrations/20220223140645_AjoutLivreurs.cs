using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class AjoutLivreurs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LivreurId",
                table: "Demande",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demande_LivreurId",
                table: "Demande",
                column: "LivreurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demande_Livreur_LivreurId",
                table: "Demande",
                column: "LivreurId",
                principalTable: "Livreur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demande_Livreur_LivreurId",
                table: "Demande");

            migrationBuilder.DropIndex(
                name: "IX_Demande_LivreurId",
                table: "Demande");

            migrationBuilder.DropColumn(
                name: "LivreurId",
                table: "Demande");
        }
    }
}
