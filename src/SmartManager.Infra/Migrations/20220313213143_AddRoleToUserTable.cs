using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartManager.Infra.Migrations
{
    public partial class AddRoleToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "User",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "User")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "User");
        }
    }
}
