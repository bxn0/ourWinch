using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ourWinch.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedServiceOrderRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Electros_ServiceOrderId",
                table: "Electros",
                column: "ServiceOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Electros");
        }
    }
}
