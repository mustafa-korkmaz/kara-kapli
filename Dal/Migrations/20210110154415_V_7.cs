using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class V_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("de522b4b-4a2a-4992-a1b3-3b27b93a0d5a"), "7c591b7b-0cfc-4de0-a216-a920779b7e7d", "premium_user", "PREMIUM_USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Settings", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("87622649-96c8-40b5-bcef-8351b0883b49"), 0, "024e1046-752c-4943-9373-5ac78ab5601a", new DateTime(2020, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "mustafa@koruq.com", true, false, null, "Mustafa Korkmaz", "MUSTAFA@KORUQ.COM", "MUSTAFA@KORUQ.COM", "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==", null, false, "951a4c00-20d0-4d65-9d4a-7db4001c834c", null, "System", false, "mustafa@koruq.com" });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "IsDeleted", "Name", "Order", "ParameterTypeId", "UserId" },
                values: new object[,]
                {
                    { 9, false, "-", (byte)0, 2, new Guid("87622649-96c8-40b5-bcef-8351b0883b49") },
                    { 10, false, "-", (byte)1, 1, new Guid("87622649-96c8-40b5-bcef-8351b0883b49") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("de522b4b-4a2a-4992-a1b3-3b27b93a0d5a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87622649-96c8-40b5-bcef-8351b0883b49"));
        }
    }
}
