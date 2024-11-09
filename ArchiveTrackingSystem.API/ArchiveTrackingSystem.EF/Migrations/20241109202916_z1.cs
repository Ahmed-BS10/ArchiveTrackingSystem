using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchiveTrackingSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class z1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "TypePayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TypePayments_Slug",
                table: "TypePayments",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TypePayments_Slug",
                table: "TypePayments");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "TypePayments");
        }
    }
}
