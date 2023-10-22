using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ourWinch.Migrations
{
    /// <inheritdoc />
    public partial class Meckanical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveServices_ServiceOrders_ServiceOrderId",
                table: "ActiveServices");

            migrationBuilder.DropIndex(
                name: "IX_ActiveServices_ServiceOrderId",
                table: "ActiveServices");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ServiceOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ServiceOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveServices_ServiceOrderId",
                table: "ActiveServices",
                column: "ServiceOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveServices_ServiceOrders_ServiceOrderId",
                table: "ActiveServices",
                column: "ServiceOrderId",
                principalTable: "ServiceOrders",
                principalColumn: "ServiceOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
