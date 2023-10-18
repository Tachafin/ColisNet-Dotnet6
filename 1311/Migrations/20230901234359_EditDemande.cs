using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class EditDemande : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Ville_VilleId",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Demande",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Demande",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroTelephone",
                table: "Demande",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "adresse",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VilleId",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomComplet",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Ville_VilleId",
                table: "Client",
                column: "VilleId",
                principalTable: "Ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Ville_VilleId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Demande");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Demande");

            migrationBuilder.DropColumn(
                name: "NumeroTelephone",
                table: "Demande");

            migrationBuilder.AlterColumn<string>(
                name: "adresse",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "VilleId",
                table: "Client",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NomComplet",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Ville_VilleId",
                table: "Client",
                column: "VilleId",
                principalTable: "Ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
