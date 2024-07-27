using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chatter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSentimentFieldTypeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("2ba96ca9-b337-46ae-898f-0e527ad72a05"), null, "User", "USER" },
                    { new Guid("e445cc08-646e-4d0d-aef7-82231892bc7a"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("1a45d953-2e14-4ab8-97ca-6e247c8dbd6d"), null, "User", "USER" },
                    { new Guid("cac3f521-a571-45c9-8097-c129318bb10e"), null, "Admin", "ADMIN" }
                });
        }
    }
}
