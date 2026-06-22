using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HungryMonster.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CompanyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NumberOfServings = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealRecords_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "ClientType", "CompanyNumber", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Contractor", "CRN001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "BuildRight Ltd", null },
                    { 2, "Contractor", "CRN002", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ConstructCo", null },
                    { 3, "Contractor", "CRN003", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SteelWorks Inc", null }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "ClientType", "CreatedAt", "Industry", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, "Partner", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Agriculture", "GreenLeaf Partners", null },
                    { 5, "Partner", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Technology", "TechBridge Corp", null }
                });

            migrationBuilder.InsertData(
                table: "MealRecords",
                columns: new[] { "Id", "ClientId", "CreatedAt", "NumberOfServings", "UpdatedAt", "Year" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 120, null, 2022 },
                    { 2, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 85, null, 2022 },
                    { 3, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 200, null, 2022 },
                    { 4, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 150, null, 2023 },
                    { 5, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 95, null, 2023 },
                    { 6, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 310, null, 2023 },
                    { 7, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 175, null, 2023 },
                    { 8, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 260, null, 2023 },
                    { 9, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 400, null, 2024 },
                    { 10, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 220, null, 2024 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealRecords_ClientId",
                table: "MealRecords",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealRecords");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
