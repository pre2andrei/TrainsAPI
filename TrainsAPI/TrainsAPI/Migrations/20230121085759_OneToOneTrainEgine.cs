using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainsAPI.Migrations
{
    /// <inheritdoc />
    public partial class OneToOneTrainEgine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trains_EngineId",
                table: "Trains");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_EngineId",
                table: "Trains",
                column: "EngineId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trains_EngineId",
                table: "Trains");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_EngineId",
                table: "Trains",
                column: "EngineId");
        }
    }
}
