using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_StoreImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "StoreImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "StoreImages");
        }
    }
}
