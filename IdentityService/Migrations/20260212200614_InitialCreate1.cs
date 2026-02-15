using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12341234-1234-1234-1234-123412341234", "AQAAAAIAAYagAAAAEMGOE8cx9dbmvAVH0+On3Ykfc97mdgx6zsquWaSDqUn7evcx2Dpl/2N0FUCwoQfAfw==", "78909876-5432-1098-7654-321098765432" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5caee931-0194-4e98-949b-ad592f9bbd53", "AQAAAAIAAYagAAAAEFnKt+x3gjEBkLbqoyloeGSk+kb9j4oD+HjoTtOihdFXkza0oEgELdHL1Ppy2El5yw==", "6b7c8c34-05c0-47b3-85b2-a8391eb61794" });
        }
    }
}
