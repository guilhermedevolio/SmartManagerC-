using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartManager.Infra.Migrations
{
    public partial class AddAttempsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "access_attempts",
                table: "User",
                type: "BIGINT",
                maxLength: 255,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "unlock_date",
                table: "User",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "access_attempts",
                table: "User");

            migrationBuilder.DropColumn(
                name: "unlock_date",
                table: "User");
        }
    }
}
