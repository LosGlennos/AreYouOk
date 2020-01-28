using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.PostgreSQL.Migrations
{
    public partial class AddedEndpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_health_data",
                table: "health_data");

            migrationBuilder.AddPrimaryKey(
                name: "pk_health_data",
                table: "health_data",
                column: "timestamp");

            migrationBuilder.CreateTable(
                name: "endpoints",
                columns: table => new
                {
                    endpoint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endpoints");

            migrationBuilder.DropPrimaryKey(
                name: "pk_health_data",
                table: "health_data");

            migrationBuilder.AddPrimaryKey(
                name: "PK_health_data",
                table: "health_data",
                column: "timestamp");
        }
    }
}
