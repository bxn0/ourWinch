using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ourWinch.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceOrders",
                columns: table => new
                {
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etternavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feilbeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    Produkttype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serienummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MottattDato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Årsmodell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Garanti = table.Column<bool>(type: "bit", nullable: false),
                    Servis = table.Column<bool>(type: "bit", nullable: false),
                    Reperasjon = table.Column<bool>(type: "bit", nullable: false),
                    KommentarFraKunde = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrders", x => x.ServiceOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Mechanicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    ChecklistItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OK = table.Column<bool>(type: "bit", nullable: false),
                    BorSkiftes = table.Column<bool>(type: "bit", nullable: false),
                    Defekt = table.Column<bool>(type: "bit", nullable: false),
                    Kommentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mechanicals_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mechanicals_ServiceOrderId",
                table: "Mechanicals",
                column: "ServiceOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mechanicals");

            migrationBuilder.DropTable(
                name: "ServiceOrders");
        }
    }
}
