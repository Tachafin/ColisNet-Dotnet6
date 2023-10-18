using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class CoddeBare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodeBarreId",
                table: "Colis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CodeBarre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chemin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeBarre", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colis_CodeBarreId",
                table: "Colis",
                column: "CodeBarreId",
                unique: true,
                filter: "[CodeBarreId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Colis_CodeBarre_CodeBarreId",
                table: "Colis",
                column: "CodeBarreId",
                principalTable: "CodeBarre",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colis_CodeBarre_CodeBarreId",
                table: "Colis");

            migrationBuilder.DropTable(
                name: "CodeBarre");

            migrationBuilder.DropIndex(
                name: "IX_Colis_CodeBarreId",
                table: "Colis");

            migrationBuilder.DropColumn(
                name: "CodeBarreId",
                table: "Colis");
        }
    }
}
