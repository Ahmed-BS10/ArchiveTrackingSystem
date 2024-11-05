using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchiveTrackingSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class editTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activte_TypePayments_typePaymentId",
                table: "Activte");

            migrationBuilder.DropIndex(
                name: "IX_Activte_typePaymentId",
                table: "Activte");

            migrationBuilder.DropColumn(
                name: "typePaymentId",
                table: "Activte");

            migrationBuilder.CreateIndex(
                name: "IX_Activte_PaymentID",
                table: "Activte",
                column: "PaymentID",
                unique: true,
                filter: "[PaymentID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Activte_TypePayments_PaymentID",
                table: "Activte",
                column: "PaymentID",
                principalTable: "TypePayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activte_TypePayments_PaymentID",
                table: "Activte");

            migrationBuilder.DropIndex(
                name: "IX_Activte_PaymentID",
                table: "Activte");

            migrationBuilder.AddColumn<int>(
                name: "typePaymentId",
                table: "Activte",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activte_typePaymentId",
                table: "Activte",
                column: "typePaymentId",
                unique: true,
                filter: "[typePaymentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Activte_TypePayments_typePaymentId",
                table: "Activte",
                column: "typePaymentId",
                principalTable: "TypePayments",
                principalColumn: "Id");
        }
    }
}
