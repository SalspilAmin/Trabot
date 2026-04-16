using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVariantImages_ProductVariantId",
                table: "ProductVariantImages");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductVariantImages");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ProductVariantImages");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantImages_ProductVariantId",
                table: "ProductVariantImages",
                column: "ProductVariantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVariantImages_ProductVariantId",
                table: "ProductVariantImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductVariantImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ProductVariantImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantImages_ProductVariantId",
                table: "ProductVariantImages",
                column: "ProductVariantId");
        }
    }
}
