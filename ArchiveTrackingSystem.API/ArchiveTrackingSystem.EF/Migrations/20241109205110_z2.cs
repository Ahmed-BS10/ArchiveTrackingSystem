using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchiveTrackingSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class z2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Files",
                newName: "CreateAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Files",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Files",
                newName: "StartDate");
        }
    }
}
