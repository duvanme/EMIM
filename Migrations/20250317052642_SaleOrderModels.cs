using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMIM.Migrations
{
    /// <inheritdoc />
    public partial class SaleOrderModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6af21339-bd1e-444c-a914-f1ba72628af3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a00a021d-0546-43b6-a810-74399046c6d8");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2fad76b8-2b0c-49aa-92e9-b12262818523", "e61b74f5-3f3e-4e0e-8e32-51a30136f0c2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fad76b8-2b0c-49aa-92e9-b12262818523");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e61b74f5-3f3e-4e0e-8e32-51a30136f0c2");

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SaleOrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderLines_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderLines_SaleOrders_Id",
                        column: x => x.Id,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderStatuses_SaleOrders_Id",
                        column: x => x.Id,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "501cc220-4878-4f90-9957-2c0295571600", null, "Vendor", "VENDOR" },
                    { "7f411c80-46dc-45a1-b3e7-183a02e7d291", null, "Admin", "ADMIN" },
                    { "b1f8bcc9-341a-4575-984c-14b3b739df76", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ee959741-a6a6-45d5-8a22-e38214a38e90", 0, "3bf72160-1beb-4712-a4f3-2e1bdacf4deb", new DateTime(2025, 3, 17, 5, 26, 41, 911, DateTimeKind.Utc).AddTicks(2186), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 17, 5, 26, 41, 911, DateTimeKind.Utc).AddTicks(2191), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEAs2ZAtu2QFWMoSu1zdFPTBNeLHLyiEEH4KrLU4P02yJjWEEtuuK60CPg1bjcCtcvw==", null, false, "6583a889-1adb-4a83-a236-ea893da76e3f", "Active", false, "admin@emim.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7f411c80-46dc-45a1-b3e7-183a02e7d291", "ee959741-a6a6-45d5-8a22-e38214a38e90" });

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_UserId",
                table: "SaleOrders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleOrderLines");

            migrationBuilder.DropTable(
                name: "SaleOrderStatuses");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "501cc220-4878-4f90-9957-2c0295571600");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1f8bcc9-341a-4575-984c-14b3b739df76");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7f411c80-46dc-45a1-b3e7-183a02e7d291", "ee959741-a6a6-45d5-8a22-e38214a38e90" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f411c80-46dc-45a1-b3e7-183a02e7d291");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee959741-a6a6-45d5-8a22-e38214a38e90");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fad76b8-2b0c-49aa-92e9-b12262818523", null, "Admin", "ADMIN" },
                    { "6af21339-bd1e-444c-a914-f1ba72628af3", null, "Vendor", "VENDOR" },
                    { "a00a021d-0546-43b6-a810-74399046c6d8", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e61b74f5-3f3e-4e0e-8e32-51a30136f0c2", 0, "4929b5df-1810-4738-bb83-fbbf4eccc981", new DateTime(2025, 3, 10, 5, 10, 5, 771, DateTimeKind.Utc).AddTicks(3644), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 10, 5, 10, 5, 771, DateTimeKind.Utc).AddTicks(3647), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEA8f9AfMFiPbGX6kd9KFqx+VhQP7Qv8C/xW/W2LHH1uDFVCqnTCBGqpzNgGhLEFJZA==", null, false, "7def85a8-8b10-4ceb-a040-8d6cd0e683fe", "Active", false, "admin@emim.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2fad76b8-2b0c-49aa-92e9-b12262818523", "e61b74f5-3f3e-4e0e-8e32-51a30136f0c2" });
        }
    }
}
