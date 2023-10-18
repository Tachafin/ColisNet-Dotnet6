using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class modifycolis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Colis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ouverture_Colis",
                table: "Colis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Produit",
                table: "Colis",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Colis");

            migrationBuilder.DropColumn(
                name: "Ouverture_Colis",
                table: "Colis");

            migrationBuilder.DropColumn(
                name: "Produit",
                table: "Colis");
        }
    }
}
