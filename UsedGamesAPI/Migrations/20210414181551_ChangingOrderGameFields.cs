using Microsoft.EntityFrameworkCore.Migrations;

namespace UsedGamesAPI.Migrations
{
    public partial class ChangingOrderGameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Game",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
