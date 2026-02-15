using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), 0, 100, "12341234-1234-1234-1234-123412341234", new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "admin@example.com", true, "Admin", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFOE74rm7I/7Kd1VGZle2q5TiSQ6nrTrsQ17+jYXEQtgaXoKob9uChHv8MH5DJfhHg==", null, false, "Admin", "78909876-5432-1098-7654-321098765432", false, "admin@example.com", "Admin" });
        }
    }
}
