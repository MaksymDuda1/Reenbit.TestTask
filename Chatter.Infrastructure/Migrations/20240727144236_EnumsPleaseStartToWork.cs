using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chatter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnumsPleaseStartToWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2ba96ca9-b337-46ae-898f-0e527ad72a05"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e445cc08-646e-4d0d-aef7-82231892bc7a"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0a2bca51-02ff-4fc7-b051-ead9ef017b55"), null, "User", "USER" },
                    { new Guid("8431070f-a445-4bbf-bd3e-32e03f66734e"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("2ba96ca9-b337-46ae-898f-0e527ad72a05"), null, "User", "USER" },
                    { new Guid("e445cc08-646e-4d0d-aef7-82231892bc7a"), null, "Admin", "ADMIN" }
                });
        }
    }
}
