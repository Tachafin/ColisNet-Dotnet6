using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class addbase64string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "base64String",
                table: "CodeBarre",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "base64String",
                table: "CodeBarre");
        }
    }
}
