using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SparePartsServiceData.Migrations
{
    /// <inheritdoc />
    public partial class updateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModelCar",
                table: "SpareParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "SpareParts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelCar",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "SpareParts");
        }
    }
}
