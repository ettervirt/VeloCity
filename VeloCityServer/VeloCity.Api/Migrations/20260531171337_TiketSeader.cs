using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeloCity.Api.Migrations
{
    /// <inheritdoc />
    public partial class TiketSeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "EndStopId", "IsValidated", "Price", "PurchasedAt", "StartStopId", "TicketTypeId", "UserId", "ValidFrom", "ValidTo", "VehicleId" },
                values: new object[,]
                {
                    { 1, null, false, 4.00m, new DateTime(2026, 5, 30, 15, 0, 0, 0, DateTimeKind.Utc), null, 1, 2, null, null, null },
                    { 2, null, true, 7.00m, new DateTime(2026, 5, 31, 18, 0, 0, 0, DateTimeKind.Utc), null, 13, 2, new DateTime(2026, 5, 31, 18, 5, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 31, 19, 5, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, null, true, 7.00m, new DateTime(2026, 5, 25, 8, 0, 0, 0, DateTimeKind.Utc), null, 13, 2, new DateTime(2026, 5, 25, 8, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 25, 9, 10, 0, 0, DateTimeKind.Utc), 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
