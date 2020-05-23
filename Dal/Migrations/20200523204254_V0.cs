using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Dal.Migrations
{
    public partial class V0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "varchar(20)", maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "varchar(50)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "varchar(50)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(8000)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(36)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(12)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    NameSurname = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(50)", nullable: false),
                    ClaimValue = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(50)", nullable: false),
                    ClaimValue = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(128)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ExpiresAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Value = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: true),
                    AuthorizedPersonName = table.Column<string>(maxLength: 50, nullable: true),
                    RemainingBalance = table.Column<double>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParameterTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Order = table.Column<byte>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_ParameterTypes_ParameterTypeId",
                        column: x => x.ParameterTypeId,
                        principalTable: "ParameterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parameters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsDebt = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Parameters_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("402e9a22-8b21-11ea-bc55-0242ac130003"), 0, "024e1046-752c-4943-9373-5ac78ab5601a", new DateTime(2020, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "mustafakorkmazdev@gmail.com", true, false, null, "Mustafa Korkmaz", "MUSTAFAKORKMAZDEV@GMAIL.COM", "MUSTAFAKORKMAZDEV@GMAIL.COM", "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==", null, false, "951a4c00-20d0-4d65-9d4a-7db4001c834c", "Korkmaz Ltd.", false, "mustafakorkmazdev@gmail.com" });

            migrationBuilder.InsertData(
                table: "ParameterTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "TransactionType" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AuthorizedPersonName", "CreatedAt", "PhoneNumber", "RemainingBalance", "Title", "UserId" },
                values: new object[] { 1, "Esra Korkmaz", new DateTime(2020, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.0, "Akcam Ltd. ", new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "IsDeleted", "Name", "Order", "ParameterTypeId", "UserId" },
                values: new object[,]
                {
                    { 1, false, "B-Cariye Borç", (byte)1, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 2, false, "A-Cariye Alacak", (byte)2, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 3, false, "A-Tahsilat", (byte)3, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 4, false, "B-Ödeme", (byte)4, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 5, false, "B-Customer Debt", (byte)5, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 6, false, "A-Customer Receivable", (byte)6, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 7, false, "A-Collection", (byte)7, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") },
                    { 8, false, "B-Payment", (byte)8, 1, new Guid("402e9a22-8b21-11ea-bc55-0242ac130003") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ParameterTypeId",
                table: "Parameters",
                column: "ParameterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_UserId",
                table: "Parameters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "ParameterTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
