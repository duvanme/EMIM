using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMIM.Migrations
{
    /// <inheritdoc />
    public partial class add_User_address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c1c546c-4e06-4d96-bcc2-e0e023766054");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58933c7e-3702-4836-9608-36361146871d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "19ac9c8e-ffb7-4da0-90a8-fd14259c7b7d", "ca378446-c3d1-4995-a707-979499381028" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19ac9c8e-ffb7-4da0-90a8-fd14259c7b7d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca378446-c3d1-4995-a707-979499381028");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89d0a7b1-e990-4730-9c68-5f79a855d6c1", null, "Vendor", "VENDOR" },
                    { "d1bf92c7-1641-4297-8098-7d6abe13743e", null, "Customer", "CUSTOMER" },
                    { "d8417678-fd3f-4f9a-9256-3c2efb9c9e3f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "UserProfilePicture" },
                values: new object[] { "b12f20d9-ffaa-485f-8167-c80212c30072", 0, null, "af7f86e4-fff8-4d1e-8bf7-4d75a008222c", new DateTime(2025, 3, 18, 2, 55, 43, 754, DateTimeKind.Utc).AddTicks(2087), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 18, 2, 55, 43, 754, DateTimeKind.Utc).AddTicks(2090), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEGBzpkXZyp+q/+Adg1nDsQ2jgnxZ2ukPxP+elnGgz88a4ADp6UQZxaZZS5rRJPAKbg==", null, false, "cbeb09f7-9217-4932-9dff-de23cd49e45f", "Active", false, "admin@emim.com", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d8417678-fd3f-4f9a-9256-3c2efb9c9e3f", "b12f20d9-ffaa-485f-8167-c80212c30072" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89d0a7b1-e990-4730-9c68-5f79a855d6c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1bf92c7-1641-4297-8098-7d6abe13743e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d8417678-fd3f-4f9a-9256-3c2efb9c9e3f", "b12f20d9-ffaa-485f-8167-c80212c30072" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8417678-fd3f-4f9a-9256-3c2efb9c9e3f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b12f20d9-ffaa-485f-8167-c80212c30072");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c1c546c-4e06-4d96-bcc2-e0e023766054", null, "Vendor", "VENDOR" },
                    { "19ac9c8e-ffb7-4da0-90a8-fd14259c7b7d", null, "Admin", "ADMIN" },
                    { "58933c7e-3702-4836-9608-36361146871d", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "UserProfilePicture" },
                values: new object[] { "ca378446-c3d1-4995-a707-979499381028", 0, "4ddbd152-19a6-4bea-87c7-6ca4c5da49e7", new DateTime(2025, 3, 18, 2, 29, 14, 655, DateTimeKind.Utc).AddTicks(5781), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 18, 2, 29, 14, 655, DateTimeKind.Utc).AddTicks(5784), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEA443MEFZQqGWFi1TwZXoxQLPtJBA5KC6bSUUGkda/3MU5WTqWTDqd3Yr97BK7bnWw==", null, false, "2478fbe5-48e5-415d-ab63-723163d2ccf7", "Active", false, "admin@emim.com", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "19ac9c8e-ffb7-4da0-90a8-fd14259c7b7d", "ca378446-c3d1-4995-a707-979499381028" });
        }
    }
}
