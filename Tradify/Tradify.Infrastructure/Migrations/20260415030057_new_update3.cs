using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Sellers_SellersId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Stores_StoresId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellersId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellersId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SellersId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_StoresId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SellersId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellersId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "StoresId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_StoreId",
                table: "Categories",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Stores_StoreId",
                table: "Categories",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Stores_StoreId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_StoreId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "SellersId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SellersId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoresId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellersId",
                table: "Products",
                column: "SellersId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SellersId",
                table: "Categories",
                column: "SellersId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_StoresId",
                table: "Categories",
                column: "StoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Sellers_SellersId",
                table: "Categories",
                column: "SellersId",
                principalTable: "Sellers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Stores_StoresId",
                table: "Categories",
                column: "StoresId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_SellersId",
                table: "Products",
                column: "SellersId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }
    }
}
