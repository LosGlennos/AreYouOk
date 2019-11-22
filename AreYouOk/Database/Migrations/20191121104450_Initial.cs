using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AreYouOk.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.PrimaryKey("PK_health_data", x => x.timestamp);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "health_data");
        }
    }
}
