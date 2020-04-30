using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Dal.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerOperation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    IsReceivable = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerOperation_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOperation_ParameterTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ParameterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOperation_CustomerId",
                table: "CustomerOperation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOperation_TypeId",
                table: "CustomerOperation",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOperation");
        }
    }
}
