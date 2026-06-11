using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fkSupOrderStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubOrders_StoreId",
                table: "SubOrders",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrders_Stores_StoreId",
                table: "SubOrders",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubOrders_Stores_StoreId",
                table: "SubOrders");

            migrationBuilder.DropIndex(
                name: "IX_SubOrders_StoreId",
                table: "SubOrders");
        }
    }
}
