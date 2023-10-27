using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ourWinch.Migrations
{
    /// <inheritdoc />
    public partial class Null : Migration
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
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etternavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobilNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feilbeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    Produkttype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serienummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MottattDato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Årsmodell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Garanti = table.Column<bool>(type: "bit", nullable: false),
                    Servis = table.Column<bool>(type: "bit", nullable: false),
                    Reperasjon = table.Column<bool>(type: "bit", nullable: false),
                    KommentarFraKunde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrders", x => x.ServiceOrderId);
                });

            migrationBuilder.CreateTable(
                name: "ActiveServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    Produkttype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etternavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MottattDato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Feilbeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvtaltLevering = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceSkjema = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveServices_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Electros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    ChecklistItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OK = table.Column<bool>(type: "bit", nullable: false),
                    BorSkiftes = table.Column<bool>(type: "bit", nullable: false),
                    Defekt = table.Column<bool>(type: "bit", nullable: false),
                    Kommentar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Electros_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunksjonsTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    ChecklistItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OK = table.Column<bool>(type: "bit", nullable: false),
                    BorSkiftes = table.Column<bool>(type: "bit", nullable: false),
                    Defekt = table.Column<bool>(type: "bit", nullable: false),
                    Kommentar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunksjonsTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FunksjonsTests_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hydrolisks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    ChecklistItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OK = table.Column<bool>(type: "bit", nullable: false),
                    BorSkiftes = table.Column<bool>(type: "bit", nullable: false),
                    Defekt = table.Column<bool>(type: "bit", nullable: false),
                    Kommentar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hydrolisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hydrolisks_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mechanicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    ChecklistItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OK = table.Column<bool>(type: "bit", nullable: false),
                    BorSkiftes = table.Column<bool>(type: "bit", nullable: false),
                    Defekt = table.Column<bool>(type: "bit", nullable: false),
                    Kommentar = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Trykks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    Ordrenummer = table.Column<int>(type: "int", nullable: false),
                    ChecklistItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OK = table.Column<bool>(type: "bit", nullable: false),
                    BorSkiftes = table.Column<bool>(type: "bit", nullable: false),
                    Defekt = table.Column<bool>(type: "bit", nullable: false),
                    Kommentar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trykks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trykks_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveServices_ServiceOrderId",
                table: "ActiveServices",
                column: "ServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Electros_ServiceOrderId",
                table: "Electros",
                column: "ServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FunksjonsTests_ServiceOrderId",
                table: "FunksjonsTests",
                column: "ServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Hydrolisks_ServiceOrderId",
                table: "Hydrolisks",
                column: "ServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Mechanicals_ServiceOrderId",
                table: "Mechanicals",
                column: "ServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Trykks_ServiceOrderId",
                table: "Trykks",
                column: "ServiceOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveServices");

            migrationBuilder.DropTable(
                name: "Electros");

            migrationBuilder.DropTable(
                name: "FunksjonsTests");

            migrationBuilder.DropTable(
                name: "Hydrolisks");

            migrationBuilder.DropTable(
                name: "Mechanicals");

            migrationBuilder.DropTable(
                name: "Trykks");

            migrationBuilder.DropTable(
                name: "ServiceOrders");
        }
    }
}
