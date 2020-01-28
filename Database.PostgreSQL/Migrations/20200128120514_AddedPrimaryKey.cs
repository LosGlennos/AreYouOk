using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.PostgreSQL.Migrations
{
    public partial class AddedPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "endpoint",
                table: "endpoints",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_endpoints",
                table: "endpoints",
                column: "endpoint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_endpoints",
                table: "endpoints");

            migrationBuilder.AlterColumn<string>(
                name: "endpoint",
                table: "endpoints",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
