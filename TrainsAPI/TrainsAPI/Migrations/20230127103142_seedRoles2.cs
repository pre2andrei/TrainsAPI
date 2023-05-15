using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainsAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75793ba1-ddc6-475c-bac0-8bd55769b23a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5d50ddf-0be9-4712-a691-ffedd54ea448");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "248043ee-e103-418f-babe-22460fbeea4a", "2", "User", "User" },
                    { "ef2f2a87-73ce-4ba0-9f0d-87483a806f08", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "248043ee-e103-418f-babe-22460fbeea4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef2f2a87-73ce-4ba0-9f0d-87483a806f08");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75793ba1-ddc6-475c-bac0-8bd55769b23a", "1", "Admin", "Admin" },
                    { "d5d50ddf-0be9-4712-a691-ffedd54ea448", "2", "User", "User" }
                });
        }
    }
}
