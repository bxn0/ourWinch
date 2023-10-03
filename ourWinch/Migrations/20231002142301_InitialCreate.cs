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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etternavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feilbeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordrenummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ServiceOrders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceOrders");
        }
    }
}
