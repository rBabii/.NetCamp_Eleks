using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Migrations
{
    public partial class AddBlockedVisibleColums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Blogs");
        }
    }
}
