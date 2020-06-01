using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingBalance",
                table: "Customers");

            migrationBuilder.AddColumn<double>(
                name: "DebtBalance",
                table: "Customers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ReceivableBalance",
                table: "Customers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtBalance",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ReceivableBalance",
                table: "Customers");

            migrationBuilder.AddColumn<double>(
                name: "RemainingBalance",
                table: "Customers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
