using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeloCity.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Payment_PaymentMethod",
                table: "Payments",
                sql: "\"PaymentMethod\" IN (1, 2, 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Payment_PaymentMethod",
                table: "Payments");
        }
    }
}
