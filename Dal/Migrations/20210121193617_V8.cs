using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("402e9a22-8b21-11ea-bc55-0242ac130003"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AuthorizedPersonName", "CreatedAt", "DebtBalance", "PhoneNumber", "ReceivableBalance", "Title", "UserId" },
                values: new object[] { 1, "Esra Korkmaz", new DateTime(2020, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, null, 0.0, "Akcam Ltd. ", new Guid("87622649-96c8-40b5-bcef-8351b0883b49") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Settings", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("402e9a22-8b21-11ea-bc55-0242ac130003"), 0, "024e1046-752c-4943-9373-5ac78ab5601a", new DateTime(2020, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "mustafakorkmazdev@gmail.com", true, false, null, "Mustafa Korkmaz", "MUSTAFAKORKMAZDEV@GMAIL.COM", "MUSTAFAKORKMAZDEV@GMAIL.COM", "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==", null, false, "951a4c00-20d0-4d65-9d4a-7db4001c834c", null, "Korkmaz Ltd.", false, "mustafakorkmazdev@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("402e9a22-8b21-11ea-bc55-0242ac130003"));

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "IsDeleted", "Name", "Order", "ParameterTypeId", "UserId" },
                values: new object[,]
                {
                    { 1, false, "Cariye Borç", (byte)1, 2, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 2, false, "Cariye Alacak", (byte)2, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 3, false, "Tahsilat", (byte)3, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 4, false, "Ödeme", (byte)4, 2, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 5, false, "Customer Debt", (byte)5, 2, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 6, false, "Customer Receivable", (byte)6, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 7, false, "Collection", (byte)7, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 8, false, "Payment", (byte)8, 2, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") }
                });
        }
    }
}
