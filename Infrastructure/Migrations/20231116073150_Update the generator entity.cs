using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Updatethegeneratorentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Generators");

            migrationBuilder.AddColumn<double>(
                name: "FuelConsuming",
                table: "Generators",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Power",
                table: "Generators",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Tank",
                table: "Generators",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelConsuming",
                table: "Generators");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Generators");

            migrationBuilder.DropColumn(
                name: "Tank",
                table: "Generators");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Generators",
                type: "text",
                nullable: true);
        }
    }
}
