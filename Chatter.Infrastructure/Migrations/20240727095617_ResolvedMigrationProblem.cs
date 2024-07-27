using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chatter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResolvedMigrationProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a633278b-8ec1-429e-9cfc-855201a41632"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac0e5941-4f8c-4067-9986-6c99b2a8b462"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1a45d953-2e14-4ab8-97ca-6e247c8dbd6d"), null, "User", "USER" },
                    { new Guid("cac3f521-a571-45c9-8097-c129318bb10e"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a45d953-2e14-4ab8-97ca-6e247c8dbd6d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cac3f521-a571-45c9-8097-c129318bb10e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a633278b-8ec1-429e-9cfc-855201a41632"), null, "Admin", "ADMIN" },
                    { new Guid("ac0e5941-4f8c-4067-9986-6c99b2a8b462"), null, "User", "USER" }
                });
        }
    }
}
