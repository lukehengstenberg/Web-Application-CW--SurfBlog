using Microsoft.EntityFrameworkCore.Migrations;

namespace _878876.Migrations
{
    public partial class FixedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Comment",
                nullable: false,
                defaultValue: "");
        }
    }
}
