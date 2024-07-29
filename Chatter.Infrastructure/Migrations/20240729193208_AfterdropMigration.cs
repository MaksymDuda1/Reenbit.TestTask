using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chatter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AfterdropMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4bb00539-2302-4085-8e14-42c0e6ed6d1e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("66141e1d-5094-44c3-931f-745280a5ac07"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b57fe867-d8a6-4a70-9a44-f82b87e24f59"), null, "Admin", "ADMIN" },
                    { new Guid("f780f695-fc00-4e5f-8f5d-851a9c3601cd"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b57fe867-d8a6-4a70-9a44-f82b87e24f59"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f780f695-fc00-4e5f-8f5d-851a9c3601cd"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4bb00539-2302-4085-8e14-42c0e6ed6d1e"), null, "User", "USER" },
                    { new Guid("66141e1d-5094-44c3-931f-745280a5ac07"), null, "Admin", "ADMIN" }
                });
        }
    }
}
