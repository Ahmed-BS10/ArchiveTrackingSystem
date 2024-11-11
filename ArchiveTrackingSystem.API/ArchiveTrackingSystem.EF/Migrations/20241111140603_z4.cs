using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchiveTrackingSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class z4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Archives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Archives");
        }
    }
}
