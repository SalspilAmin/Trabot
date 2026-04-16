using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Price] - ([Price] * ([Discount] / 100))",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[Price] - ([Price] * [Discount] / 100)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Price] - ([Price] * [Discount] / 100)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[Price] - ([Price] * ([Discount] / 100))");
        }
    }
}
