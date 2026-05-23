using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeloCity.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialVeloCityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ZoneId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ZoneLimit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SideNumber = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LineId = table.Column<int>(type: "integer", nullable: false),
                    StopId = table.Column<int>(type: "integer", nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteStops_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStops_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TicketTypeId = table.Column<int>(type: "integer", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsValidated = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StartStopId = table.Column<int>(type: "integer", nullable: false),
                    EndStopId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "DurationInMinutes", "IsActive", "Name", "Price", "ZoneLimit" },
                values: new object[,]
                {
                    { 1, 0, true, "Normalny - Miejski", 4.00m, 0 },
                    { 2, 0, true, "Ustawowy Ulgowy - Miejski", 2.00m, 0 },
                    { 3, 0, true, "Lokalny Ulgowy - Miejski", 2.35m, 0 },
                    { 4, 0, true, "Normalny - Strefa 1", 4.80m, 1 },
                    { 5, 0, true, "Ustawowy Ulgowy - Strefa 1", 2.40m, 1 },
                    { 6, 0, true, "Lokalny Ulgowy - Strefa 1", 2.65m, 1 },
                    { 7, 0, true, "Normalny - Strefa 2", 5.90m, 2 },
                    { 8, 0, true, "Ustawowy Ulgowy - Strefa 2", 2.95m, 2 },
                    { 9, 0, true, "Lokalny Ulgowy - Strefa 2", 3.10m, 2 },
                    { 10, 0, true, "Gminny Normalny", 3.00m, 1 },
                    { 11, 0, true, "Gminny Ustawowy Ulgowy", 1.50m, 1 },
                    { 12, 0, true, "Gminny Lokalny Ulgowy", 1.80m, 1 },
                    { 13, 60, true, "Przesiadkowy 60 min - Normalny", 7.00m, 99 },
                    { 14, 60, true, "Przesiadkowy 60 min - Ustawowy Ulgowy", 3.50m, 99 },
                    { 15, 60, true, "Przesiadkowy 60 min - Lokalny Ulgowy", 3.90m, 99 },
                    { 16, 240, true, "Przesiadkowy 4h - Normalny", 12.60m, 99 },
                    { 17, 240, true, "Przesiadkowy 4h - Ustawowy Ulgowy", 6.30m, 99 },
                    { 18, 240, true, "Przesiadkowy 4h - Lokalny Ulgowy", 7.20m, 99 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "IsActive", "Name", "PasswordHash", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, 0.00m, "driver@velocity.pl", true, "Piotr", "$2a$12$mVyskPgOLm8Ih5RumTF8xeXH1.B20XqSVu8SxcIOEV0F6EFmdNKMq", 2, "Kierowca" },
                    { 2, 50.00m, "piboloz@student.wsb-nlu.edu.pl", true, "Piotr", "$2a$12$3s7iX0hZX00hn6JFwKJ06elVcg0A5mw9LVx4QHvaZW.Q3MWEyrPA2", 1, "Bołoz" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "IsActive", "Model", "SideNumber" },
                values: new object[,]
                {
                    { 1, true, "Solaris Urbino 12", "101" },
                    { 2, true, "Solaris Urbino 12", "102" },
                    { 3, true, "Solaris Urbino 12", "103" },
                    { 4, true, "Solaris Urbino 12", "104" },
                    { 5, true, "Solaris Urbino 12", "105" },
                    { 6, true, "Solaris Urbino 18", "201" },
                    { 7, true, "Solaris Urbino 18", "202" },
                    { 8, true, "Mercedes-Benz Citaro", "301" },
                    { 9, true, "Mercedes-Benz Citaro", "302" },
                    { 10, true, "Mercedes-Benz Citaro", "303" },
                    { 11, true, "Autosan SanCity 9LE", "401" },
                    { 12, true, "Autosan SanCity 9LE", "402" },
                    { 13, true, "Autosan SanCity 12LF", "403" },
                    { 14, true, "MAN Lion's City", "501" },
                    { 15, true, "MAN Lion's City", "502" },
                    { 16, true, "Iveco Daily 70C", "601" },
                    { 17, true, "Iveco Daily 70C", "602" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_LineId",
                table: "RouteStops",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_StopId",
                table: "RouteStops",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_ExternalId",
                table: "Stops",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_SideNumber",
                table: "Vehicles",
                column: "SideNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteStops");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
