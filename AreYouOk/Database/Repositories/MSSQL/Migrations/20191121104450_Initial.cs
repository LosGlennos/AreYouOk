﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AreYouOk.Database.Repositories.MSSQL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.PrimaryKey("PK_health_data", x => x.Timestamp);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthData");
        }
    }
}
