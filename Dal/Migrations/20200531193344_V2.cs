using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7f9fcc26-c38c-46bd-86a7-b7b3d5959b78"), "f0e32c74-6510-484d-a6f4-db6a79eb82e4", "owner", "OWNER" },
                    { new Guid("e964fe31-ba9a-4ee6-98c1-7fa84767868d"), "a620a23a-3760-4d4f-a095-79ffe5fd9a39", "admin", "ADMIN" },
                    { new Guid("0967d456-60a8-43de-9ac8-5f15dfaa1909"), "7473ba90-3e20-491d-a4c2-ecbc7f22ec5f", "user", "USER" },
                    { new Guid("8a158f67-b9aa-4dec-9e8f-53d29aeb1905"), "8a51070d-4ad9-4b7a-84af-c7b4bfb7aa41", "demo_user", "DEMO_USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0967d456-60a8-43de-9ac8-5f15dfaa1909"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f9fcc26-c38c-46bd-86a7-b7b3d5959b78"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8a158f67-b9aa-4dec-9e8f-53d29aeb1905"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e964fe31-ba9a-4ee6-98c1-7fa84767868d"));
        }
    }
}
