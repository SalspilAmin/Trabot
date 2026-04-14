using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStoreImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoreImages_StoreId",
                table: "StoreImages");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "StoreImages");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "StoreImages");

            migrationBuilder.CreateIndex(
                name: "IX_StoreImages_StoreId",
                table: "StoreImages",
                column: "StoreId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoreImages_StoreId",
                table: "StoreImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "StoreImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "StoreImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoreImages_StoreId",
                table: "StoreImages",
                column: "StoreId");
        }
    }
}
