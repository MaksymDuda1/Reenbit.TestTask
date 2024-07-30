using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chatter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Qwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("01010b1c-8fd1-4c0c-ac6f-aec5654b964d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("777451ef-6a47-4368-82be-2839a3eefecc"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4974e8c0-0f02-4dde-a43e-cdda546c018e"), null, "User", "USER" },
                    { new Guid("73c1639c-c953-436c-b0f7-6bb4f68fdf2f"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4974e8c0-0f02-4dde-a43e-cdda546c018e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("73c1639c-c953-436c-b0f7-6bb4f68fdf2f"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("01010b1c-8fd1-4c0c-ac6f-aec5654b964d"), null, "User", "USER" },
                    { new Guid("777451ef-6a47-4368-82be-2839a3eefecc"), null, "Admin", "ADMIN" }
                });
        }
    }
}
