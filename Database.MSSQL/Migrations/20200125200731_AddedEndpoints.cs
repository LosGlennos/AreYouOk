using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.MSSQL.Migrations
{
    public partial class AddedEndpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthData");

            migrationBuilder.CreateTable(
                name: "endpoints",
                columns: table => new
                {
                    endpoint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "health_data",
                columns: table => new
                {
                    timestamp = table.Column<DateTime>(nullable: false),
                    success = table.Column<bool>(nullable: false),
                    status_code = table.Column<int>(nullable: false),
                    elapsed_milliseconds = table.Column<int>(nullable: false),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_health_data", x => x.timestamp);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endpoints");

            migrationBuilder.DropTable(
                name: "health_data");

            migrationBuilder.CreateTable(
                name: "HealthData",
                columns: table => new
                {
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    elapsed_milliseconds = table.Column<int>(type: "integer", nullable: false),
                    status_code = table.Column<int>(type: "integer", nullable: false),
                    success = table.Column<bool>(type: "boolean", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthData", x => x.timestamp);
                });
        }
    }
}
