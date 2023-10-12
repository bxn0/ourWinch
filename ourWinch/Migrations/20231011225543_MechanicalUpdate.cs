using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ourWinch.Migrations
{
    /// <inheritdoc />
    public partial class MechanicalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChecklistData",
                table: "ServiceOrders");

            migrationBuilder.CreateTable(
                name: "Mechanicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SjekkPunkter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OK = table.Column<bool>(type: "bit", nullable: false),
                    BorSkiftes = table.Column<bool>(type: "bit", nullable: false),
                    Defekt = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanicals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mechanicals");

            migrationBuilder.AddColumn<string>(
                name: "ChecklistData",
                table: "ServiceOrders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
