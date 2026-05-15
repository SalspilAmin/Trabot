using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateInShipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShipmentTracking_ShipmentTrackingId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shipments_ShipmentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Orders_OrderId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubOrders_ShipmentTracking_ShipmentTrackingId",
                table: "SubOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SubOrders_Shipments_ShipmentId",
                table: "SubOrders");

            migrationBuilder.DropIndex(
                name: "IX_SubOrders_ShipmentId",
                table: "SubOrders");

            migrationBuilder.DropIndex(
                name: "IX_SubOrders_ShipmentTrackingId",
                table: "SubOrders");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipmentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipmentTrackingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "SubOrders");

            migrationBuilder.DropColumn(
                name: "ShipmentTrackingId",
                table: "SubOrders");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentTrackingId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Shipments",
                newName: "SubOrderId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SubOrderId",
                table: "Shipments",
                column: "SubOrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_SubOrders_SubOrderId",
                table: "Shipments",
                column: "SubOrderId",
                principalTable: "SubOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_SubOrders_SubOrderId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_SubOrderId",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "SubOrderId",
                table: "Shipments",
                newName: "OrderId");

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "SubOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentTrackingId",
                table: "SubOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentTrackingId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_SubOrders_ShipmentId",
                table: "SubOrders",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubOrders_ShipmentTrackingId",
                table: "SubOrders",
                column: "ShipmentTrackingId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipmentId",
                table: "Orders",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipmentTrackingId",
                table: "Orders",
                column: "ShipmentTrackingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShipmentTracking_ShipmentTrackingId",
                table: "Orders",
                column: "ShipmentTrackingId",
                principalTable: "ShipmentTracking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shipments_ShipmentId",
                table: "Orders",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Orders_OrderId",
                table: "Shipments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrders_ShipmentTracking_ShipmentTrackingId",
                table: "SubOrders",
                column: "ShipmentTrackingId",
                principalTable: "ShipmentTracking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrders_Shipments_ShipmentId",
                table: "SubOrders",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id");
        }
    }
}
