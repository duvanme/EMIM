using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMIM.Migrations
{
    /// <inheritdoc />
    public partial class HighlightedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2c74d42-47a2-4290-bbce-c931c9ed4a1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d98e9f09-6401-49b2-b5c5-2cc8ec923a87");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f665f7f6-de39-42a5-9ccd-b2ef180e2ee0", "a02753d9-6f65-4920-b011-b10230020d74" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f665f7f6-de39-42a5-9ccd-b2ef180e2ee0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a02753d9-6f65-4920-b011-b10230020d74");

            migrationBuilder.AddColumn<bool>(
                name: "IsHighlighted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cc5b7f2a-4afd-459f-8c5b-013e05261170", null, "Customer", "CUSTOMER" },
                    { "d4982792-efc9-42ae-b35c-140334ee160d", null, "Admin", "ADMIN" },
                    { "f1817706-907c-4c6a-a715-8ae5e9c47fb7", null, "Vendor", "VENDOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "UserProfilePicture" },
                values: new object[] { "416a9145-a0ff-47cd-9247-ea255c9b0bef", 0, null, "7c64b9c8-aeb5-4a41-82d9-1b6db2e8391b", new DateTime(2025, 3, 31, 13, 15, 41, 407, DateTimeKind.Utc).AddTicks(3160), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 31, 13, 15, 41, 407, DateTimeKind.Utc).AddTicks(3162), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEOke3AajZ9bLQg/uA9KJMgcsgqRCrvv+EH3NXUNmfam4znCz4STZfphV7l9YuyaI9g==", null, false, "592992e3-3929-4d31-b72f-edc6894ca68e", "Active", false, "admin@emim.com", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d4982792-efc9-42ae-b35c-140334ee160d", "416a9145-a0ff-47cd-9247-ea255c9b0bef" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc5b7f2a-4afd-459f-8c5b-013e05261170");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1817706-907c-4c6a-a715-8ae5e9c47fb7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d4982792-efc9-42ae-b35c-140334ee160d", "416a9145-a0ff-47cd-9247-ea255c9b0bef" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4982792-efc9-42ae-b35c-140334ee160d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "416a9145-a0ff-47cd-9247-ea255c9b0bef");

            migrationBuilder.DropColumn(
                name: "IsHighlighted",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a2c74d42-47a2-4290-bbce-c931c9ed4a1a", null, "Customer", "CUSTOMER" },
                    { "d98e9f09-6401-49b2-b5c5-2cc8ec923a87", null, "Vendor", "VENDOR" },
                    { "f665f7f6-de39-42a5-9ccd-b2ef180e2ee0", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "UserProfilePicture" },
                values: new object[] { "a02753d9-6f65-4920-b011-b10230020d74", 0, null, "3dfff0d2-8e0f-4480-ac3d-28ee4c8fda50", new DateTime(2025, 3, 30, 2, 52, 8, 996, DateTimeKind.Utc).AddTicks(3723), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@emim.com", true, "Admin", "User", false, null, new DateTime(2025, 3, 30, 2, 52, 8, 996, DateTimeKind.Utc).AddTicks(3727), "ADMIN@EMIM.COM", "ADMIN@EMIM.COM", "AQAAAAIAAYagAAAAEPqd6yiytmFZutqniuqzyAITzkvPntPLqSiHrlLd9tjA4XvdMvQTh5N91qAnnYGTTg==", null, false, "ed88b936-6ce3-4fe1-8e5b-fd26e3773fe2", "Active", false, "admin@emim.com", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f665f7f6-de39-42a5-9ccd-b2ef180e2ee0", "a02753d9-6f65-4920-b011-b10230020d74" });
        }
    }
}
