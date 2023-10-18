using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class Change1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListeRamassage_AspNetUsers_UserId1",
                table: "ListeRamassage");

            migrationBuilder.DropIndex(
                name: "IX_ListeRamassage_UserId1",
                table: "ListeRamassage");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ListeRamassage");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ListeRamassage",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListeRamassage_UserId",
                table: "ListeRamassage",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListeRamassage_AspNetUsers_UserId",
                table: "ListeRamassage",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListeRamassage_AspNetUsers_UserId",
                table: "ListeRamassage");

            migrationBuilder.DropIndex(
                name: "IX_ListeRamassage_UserId",
                table: "ListeRamassage");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ListeRamassage",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ListeRamassage",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListeRamassage_UserId1",
                table: "ListeRamassage",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ListeRamassage_AspNetUsers_UserId1",
                table: "ListeRamassage",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
