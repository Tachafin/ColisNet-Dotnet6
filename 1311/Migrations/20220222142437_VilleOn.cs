using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class VilleOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Statut",
                table: "Ville",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Ville");
        }
    }
}
