using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class restart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bank_Boutique_BoutiqueId",
                table: "Bank");

            migrationBuilder.DropForeignKey(
                name: "FK_Boutique_AspNetUsers_UserId",
                table: "Boutique");

            migrationBuilder.DropIndex(
                name: "IX_Boutique_UserId",
                table: "Boutique");

            migrationBuilder.DropIndex(
                name: "IX_Bank_BoutiqueId",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Boutique");

            migrationBuilder.DropColumn(
                name: "BoutiqueId",
                table: "Bank");

            migrationBuilder.AddColumn<int>(
                name: "BankeId",
                table: "Boutique",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoutiqueId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boutique_BankeId",
                table: "Boutique",
                column: "BankeId",
                unique: true,
                filter: "[BankeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BoutiqueId",
                table: "AspNetUsers",
                column: "BoutiqueId",
                unique: true,
                filter: "[BoutiqueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Boutique_BoutiqueId",
                table: "AspNetUsers",
                column: "BoutiqueId",
                principalTable: "Boutique",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boutique_Bank_BankeId",
                table: "Boutique",
                column: "BankeId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Boutique_BoutiqueId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Boutique_Bank_BankeId",
                table: "Boutique");

            migrationBuilder.DropIndex(
                name: "IX_Boutique_BankeId",
                table: "Boutique");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BoutiqueId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BankeId",
                table: "Boutique");

            migrationBuilder.DropColumn(
                name: "BoutiqueId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Boutique",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoutiqueId",
                table: "Bank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Boutique_UserId",
                table: "Boutique",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_BoutiqueId",
                table: "Bank",
                column: "BoutiqueId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bank_Boutique_BoutiqueId",
                table: "Bank",
                column: "BoutiqueId",
                principalTable: "Boutique",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boutique_AspNetUsers_UserId",
                table: "Boutique",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
