using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.MSSQL.Migrations
{
    public partial class AddedPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Endpoints",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endpoints",
                table: "Endpoints",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Endpoints",
                table: "Endpoints");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Endpoints");
        }
    }
}
