using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _1311.Migrations
{
    public partial class FACTURV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: true),
                    Frais = table.Column<int>(type: "int", nullable: true),
                    Net = table.Column<int>(type: "int", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LivreurId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facture_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facture_Livreur_LivreurId",
                        column: x => x.LivreurId,
                        principalTable: "Livreur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facture_LivreurId",
                table: "Facture",
                column: "LivreurId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_UserId",
                table: "Facture",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facture");
        }
    }
}
