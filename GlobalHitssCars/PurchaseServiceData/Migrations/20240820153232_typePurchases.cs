using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurchaseServiceData.Migrations
{
    /// <inheritdoc />
    public partial class typePurchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypePurchase",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypePurchase",
                table: "Purchases");
        }
    }
}
