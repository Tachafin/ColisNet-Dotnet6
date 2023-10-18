using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class updatecomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sujet",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Sujet",
                table: "Comment");
        }
    }
}
