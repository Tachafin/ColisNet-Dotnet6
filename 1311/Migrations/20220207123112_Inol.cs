using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class Inol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Livreur",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livreur_UserId",
                table: "Livreur",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Livreur_AspNetUsers_UserId",
                table: "Livreur",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livreur_AspNetUsers_UserId",
                table: "Livreur");

            migrationBuilder.DropIndex(
                name: "IX_Livreur_UserId",
                table: "Livreur");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Livreur");
        }
    }
}
