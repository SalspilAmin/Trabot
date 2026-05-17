using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Shipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_ShipmentTracking_ShipmentTrackingId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ShipmentTrackingId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "ShipmentTracking");

            migrationBuilder.DropColumn(
                name: "ShipmentTrackingId",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ShipmentTracking",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAT",
                table: "Shipments",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ShipmentTracking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "ShipmentTracking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Shipments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Shipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "CurrentStatus",
                table: "Shipments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentTracking_ShipmentId",
                table: "ShipmentTracking",
                column: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentTracking_Shipments_ShipmentId",
                table: "ShipmentTracking",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentTracking_Shipments_ShipmentId",
                table: "ShipmentTracking");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentTracking_ShipmentId",
                table: "ShipmentTracking");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ShipmentTracking");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "ShipmentTracking");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ShipmentTracking",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Shipments",
                newName: "UpdatedAT");

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                table: "ShipmentTracking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAT",
                table: "Shipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentTrackingId",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentTrackingId",
                table: "Shipments",
                column: "ShipmentTrackingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_ShipmentTracking_ShipmentTrackingId",
                table: "Shipments",
                column: "ShipmentTrackingId",
                principalTable: "ShipmentTracking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
