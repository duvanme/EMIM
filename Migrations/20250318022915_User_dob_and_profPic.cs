using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMIM.Migrations
{
    /// <inheritdoc />
    public partial class User_dob_and_profPic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09314d70-0177-41a6-9760-3cd2a43901eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62949aec-2126-44ca-ad9f-2a26e99195e2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1c96968-386e-436b-b849-58b4b3a05fe3", "75211436-9ce6-4bab-8c9e-3d07159d2842" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1c96968-386e-436b-b849-58b4b3a05fe3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75211436-9ce6-4bab-8c9e-3d07159d2842");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09314d70-0177-41a6-9760-3cd2a43901eb", null, "Customer", "CUSTOMER" },
                    { "62949aec-2126-44ca-ad9f-2a26e99195e2", null, "Vendor", "VENDOR" },
                    { "d1c96968-386e-436b-b849-58b4b3a05fe3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "75211436-9ce6-4bab-8c9e-3d07159d2842", 0, "d2b8be8a-69d1-469a-8ef8-52478afb09a1", new DateTime(2025, 3, 17, 22, 53, 55, 316, DateTimeKind.Utc).AddTicks(1553), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 17, 22, 53, 55, 316, DateTimeKind.Utc).AddTicks(1557), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEL1RRcTdM8pS0GdWaNIaUO3xH3yQITx9tPseIuPT84c/PxNxZCnn70g363K4R+d6/g==", null, false, "e644c4ef-e4e1-4b4f-8cc4-9a11f4770cfd", "Active", false, "admin@emim.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1c96968-386e-436b-b849-58b4b3a05fe3", "75211436-9ce6-4bab-8c9e-3d07159d2842" });
        }
    }
}
