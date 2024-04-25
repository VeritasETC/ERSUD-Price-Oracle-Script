using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class addNewAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "contractLatestRate",
                table: "Rates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contractLatestRate",
                table: "Rates");
        }
    }
}
