using Microsoft.EntityFrameworkCore.Migrations;

namespace UsedGamesAPI.Migrations
{
    public partial class AddingAssociationsWithContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_ClientContact_ClientContactId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Seller_SellerContact_SellerContactId",
                table: "Seller");

            migrationBuilder.DropIndex(
                name: "IX_Seller_SellerContactId",
                table: "Seller");

            migrationBuilder.DropIndex(
                name: "IX_Client_ClientContactId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "SellerContactId",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "ClientContactId",
                table: "Client");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "SellerContact",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ClientContact",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SellerContact_SellerId",
                table: "SellerContact",
                column: "SellerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_ClientId",
                table: "ClientContact",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_Client_ClientId",
                table: "ClientContact",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerContact_Seller_SellerId",
                table: "SellerContact",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_Client_ClientId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerContact_Seller_SellerId",
                table: "SellerContact");

            migrationBuilder.DropIndex(
                name: "IX_SellerContact_SellerId",
                table: "SellerContact");

            migrationBuilder.DropIndex(
                name: "IX_ClientContact_ClientId",
                table: "ClientContact");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "SellerContact");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientContact");

            migrationBuilder.AddColumn<int>(
                name: "SellerContactId",
                table: "Seller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientContactId",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seller_SellerContactId",
                table: "Seller",
                column: "SellerContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientContactId",
                table: "Client",
                column: "ClientContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_ClientContact_ClientContactId",
                table: "Client",
                column: "ClientContactId",
                principalTable: "ClientContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_SellerContact_SellerContactId",
                table: "Seller",
                column: "SellerContactId",
                principalTable: "SellerContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
