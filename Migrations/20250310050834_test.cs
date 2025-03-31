using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMIM.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28f82346-b228-446b-8d0e-517ea8f148ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f99db01-e1e8-4429-8899-f49e6c24f80e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e73ef5b9-f255-41b8-b5d2-048dfcee27b3", "ec27a110-352a-49e3-9bbd-8810baef51d2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e73ef5b9-f255-41b8-b5d2-048dfcee27b3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ec27a110-352a-49e3-9bbd-8810baef51d2");

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7fda2eb-0ed6-45e0-9083-0058c5bc8c13", "652356d8-1721-4654-b911-6413bec29fb3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "28f82346-b228-446b-8d0e-517ea8f148ee", null, "Vendor", "VENDOR" },
                    { "6f99db01-e1e8-4429-8899-f49e6c24f80e", null, "Customer", "CUSTOMER" },
                    { "e73ef5b9-f255-41b8-b5d2-048dfcee27b3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ec27a110-352a-49e3-9bbd-8810baef51d2", 0, "820f7a89-2728-408f-ac57-8abddedc1d81", new DateTime(2025, 3, 7, 23, 21, 12, 190, DateTimeKind.Utc).AddTicks(8474), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 7, 23, 21, 12, 190, DateTimeKind.Utc).AddTicks(8477), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEJ/m21UGh1CHzPqSfdgvsF1cdB3RKYK01y90YEV69Amw80yx2+vS97mcJkAgT0x9cw==", null, false, "72b237f7-11a2-4985-ac28-2f97797b7cb2", "Active", false, "admin@emim.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e73ef5b9-f255-41b8-b5d2-048dfcee27b3", "ec27a110-352a-49e3-9bbd-8810baef51d2" });
        }
    }
}
