using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMIM.Migrations
{
    /// <inheritdoc />
    public partial class newchangestomerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Products_IdProducto",
                table: "Questions");

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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Questions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_UserId",
                table: "Questions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Products_IdProducto",
                table: "Questions",
                column: "IdProducto",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_UserId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Products_IdProducto",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_UserId",
                table: "Questions");

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

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Questions");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Products_IdProducto",
                table: "Questions",
                column: "IdProducto",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
