using Microsoft.EntityFrameworkCore.Migrations;

namespace MiriNews.Data.Migrations
{
    public partial class addnewcolumnPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "onTrending",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "onTrending",
                table: "Posts");
        }
    }
}
