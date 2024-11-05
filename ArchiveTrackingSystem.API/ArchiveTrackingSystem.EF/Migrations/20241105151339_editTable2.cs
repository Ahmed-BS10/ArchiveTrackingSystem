using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchiveTrackingSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class editTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Activte_Id",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Addreess_addreesId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_addreesId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "addreesId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentID",
                table: "Activte",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ActiveID",
                table: "Files",
                column: "ActiveID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Activte_ActiveID",
                table: "Files",
                column: "ActiveID",
                principalTable: "Activte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Addreess_Id",
                table: "Files",
                column: "Id",
                principalTable: "Addreess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Activte_ActiveID",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Addreess_Id",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ActiveID",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "addreesId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentID",
                table: "Activte",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_addreesId",
                table: "Files",
                column: "addreesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Activte_Id",
                table: "Files",
                column: "Id",
                principalTable: "Activte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Addreess_addreesId",
                table: "Files",
                column: "addreesId",
                principalTable: "Addreess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
