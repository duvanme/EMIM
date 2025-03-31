using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMIM.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bf4eb19-20c5-4ddb-8ebf-72601c658ade");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a824ba4-2cc0-491c-9bfd-5b8497fb0a06");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c7fda2eb-0ed6-45e0-9083-0058c5bc8c13", "652356d8-1721-4654-b911-6413bec29fb3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7fda2eb-0ed6-45e0-9083-0058c5bc8c13");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "652356d8-1721-4654-b911-6413bec29fb3");

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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Tecnología");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Juguetes");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Deporte" },
                    { 5, "Maquillaje" },
                    { 6, "Hogar" },
                    { 7, "Libros" },
                    { 8, "Herramientas" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2fad76b8-2b0c-49aa-92e9-b12262818523", "e61b74f5-3f3e-4e0e-8e32-51a30136f0c2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fad76b8-2b0c-49aa-92e9-b12262818523");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e61b74f5-3f3e-4e0e-8e32-51a30136f0c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7bf4eb19-20c5-4ddb-8ebf-72601c658ade", null, "Vendor", "VENDOR" },
                    { "9a824ba4-2cc0-491c-9bfd-5b8497fb0a06", null, "Customer", "CUSTOMER" },
                    { "c7fda2eb-0ed6-45e0-9083-0058c5bc8c13", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "652356d8-1721-4654-b911-6413bec29fb3", 0, "21858e65-0859-4e64-bc0c-7b2a509c8713", new DateTime(2025, 3, 10, 5, 8, 34, 25, DateTimeKind.Utc).AddTicks(3006), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 10, 5, 8, 34, 25, DateTimeKind.Utc).AddTicks(3008), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEA4FwO7WhwEF3KskRFH+F8ZlpSwrNtfnbaN7T5x3klyPwyBO0ApS1dIx4AbASBSAaw==", null, false, "62549821-4736-4f60-961f-29079f01e7db", "Active", false, "admin@emim.com" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Comida");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Tecnología");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7fda2eb-0ed6-45e0-9083-0058c5bc8c13", "652356d8-1721-4654-b911-6413bec29fb3" });
        }
    }
}
