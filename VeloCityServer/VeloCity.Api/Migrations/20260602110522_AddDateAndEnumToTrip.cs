using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeloCity.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDateAndEnumToTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Trips");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Trips",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Timetables",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Timetables");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
