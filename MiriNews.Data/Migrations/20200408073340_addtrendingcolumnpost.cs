using Microsoft.EntityFrameworkCore.Migrations;

namespace MiriNews.Data.Migrations
{
    public partial class addtrendingcolumnpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "onTrending",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "ButtomTrending",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RightContent",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TopTrending",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtomTrending",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RightContent",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TopTrending",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "onTrending",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
