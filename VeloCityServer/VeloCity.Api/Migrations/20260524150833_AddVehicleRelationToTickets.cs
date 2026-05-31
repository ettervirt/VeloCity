using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeloCity.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleRelationToTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartStopId",
                table: "Tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VehicleId",
                table: "Tickets",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Vehicles_VehicleId",
                table: "Tickets",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Vehicles_VehicleId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_VehicleId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "StartStopId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
