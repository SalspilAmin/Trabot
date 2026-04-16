using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaData",
                table: "ProductVariants");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductVariants");

            migrationBuilder.AddColumn<string>(
                name: "MetaData",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
