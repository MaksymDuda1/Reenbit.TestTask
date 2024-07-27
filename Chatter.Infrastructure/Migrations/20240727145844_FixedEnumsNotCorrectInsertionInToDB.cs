using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chatter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedEnumsNotCorrectInsertionInToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0a2bca51-02ff-4fc7-b051-ead9ef017b55"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8431070f-a445-4bbf-bd3e-32e03f66734e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4bb00539-2302-4085-8e14-42c0e6ed6d1e"), null, "User", "USER" },
                    { new Guid("66141e1d-5094-44c3-931f-745280a5ac07"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("0a2bca51-02ff-4fc7-b051-ead9ef017b55"), null, "User", "USER" },
                    { new Guid("8431070f-a445-4bbf-bd3e-32e03f66734e"), null, "Admin", "ADMIN" }
                });
        }
    }
}
