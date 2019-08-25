using Microsoft.EntityFrameworkCore.Migrations;

namespace EatToday.Web.Migrations
{
    public partial class Users3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
