using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.MSSQL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endpoints",
                columns: table => new
                {
                    Endpoint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HealthData",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Success = table.Column<bool>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false),
                    ElapsedMilliseconds = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthData", x => x.Timestamp);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endpoints");

            migrationBuilder.DropTable(
                name: "HealthData");
        }
    }
}
