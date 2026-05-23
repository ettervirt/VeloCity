using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeloCity.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentsAndTicketsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<int>(type: "integer", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountInBaseCurrency = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.CheckConstraint("CK_Payment_PaymentMethod", "\"PaymentMethod\" IN (1, 2, 3)");
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "AmountInBaseCurrency", "CreatedAt", "Currency", "ExchangeRate", "PaymentMethod", "Status", "TransactionId", "UserId" },
                values: new object[,]
                {
                    { 1, 20.00m, 20.00m, new DateTime(2026, 5, 20, 10, 0, 0, 0, DateTimeKind.Utc), 1, 1.00m, 1, 1, "TNX-20260520-A1B2C3D4", 2 },
                    { 2, 30.00m, 30.00m, new DateTime(2026, 5, 21, 14, 30, 0, 0, DateTimeKind.Utc), 1, 1.00m, 1, 1, "TNX-20260521-E5F6G7H8", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "IsActive", "Name", "PasswordHash", "Role", "Surname" },
                values: new object[] { 3, 0.00m, "dkflorek@student.wsb-nlu.edu.pl", true, "Dominik", "$2a$12$3s7iX0hZX00hn6JFwKJ06elVcg0A5mw9LVx4QHvaZW.Q3MWEyrPA2", 3, "Florek" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
