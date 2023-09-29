using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ourWinch.Migrations
{
    /// <inheritdoc />
    public partial class Kunde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kunde",
                columns: table => new
                {
                    KundeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etternavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobilNo = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostNummer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunde", x => x.KundeId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MottattDato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Årsmodel = table.Column<int>(type: "int", nullable: false),
                    KommentarFelte = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    KundeNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvtaltLevering = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServisStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServisSkjema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTimer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServisId);
                });

            migrationBuilder.CreateTable(
                name: "ServisType",
                columns: table => new
                {
                    ServisTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Garanti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServisTid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reperasjon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServisType", x => x.ServisTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SjekkListe",
                columns: table => new
                {
                    SjekkListeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MekanikSjekkId = table.Column<int>(type: "int", nullable: false),
                    MekanikSjekkListe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MekanikSjekkStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MekanikKommentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HydrolikSjekkId = table.Column<int>(type: "int", nullable: false),
                    HydrolikSjekkListe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HydrolikSjekkStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HydrolikKommentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElektroSjekkId = table.Column<int>(type: "int", nullable: false),
                    ElektroSjekkListe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElektroSjekkStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElektroKommentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FonksjonSjekkId = table.Column<int>(type: "int", nullable: false),
                    FonksjonSjekkListe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FonksjonSjekkStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FonksjonKommentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrykkSjekkId = table.Column<int>(type: "int", nullable: false),
                    TrykkSjekkListe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrykkSjekkStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrykkKommentar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SjekkListe", x => x.SjekkListeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kunde");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "ServisType");

            migrationBuilder.DropTable(
                name: "SjekkListe");
        }
    }
}
