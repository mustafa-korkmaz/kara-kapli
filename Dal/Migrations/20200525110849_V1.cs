using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParameterTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TransactionType.Receivable");

            migrationBuilder.InsertData(
                table: "ParameterTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "TransactionType.Debt" });

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Cariye Alacak");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Tahsilat");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Customer Receivable");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Collection");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ParameterTypeId" },
                values: new object[] { "Cariye Borç", 2 });

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ParameterTypeId" },
                values: new object[] { "Ödeme", 2 });

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ParameterTypeId" },
                values: new object[] { "Customer Debt", 2 });

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "ParameterTypeId" },
                values: new object[] { "Payment", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ParameterTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "ParameterTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TransactionType");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "A-Cariye Alacak");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "A-Tahsilat");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "A-Customer Receivable");

            migrationBuilder.UpdateData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "A-Collection");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "IsDeleted", "Name", "Order", "ParameterTypeId", "UserId" },
                values: new object[,]
                {
                    { 4, false, "B-Ödeme", (byte)4, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 1, false, "B-Cariye Borç", (byte)1, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 8, false, "B-Payment", (byte)8, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 5, false, "B-Customer Debt", (byte)5, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") }
                });
        }
    }
}
