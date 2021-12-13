using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    public partial class AddPreviewtextFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewText",
                table: "Posts",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviewText",
                table: "Blogs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PreviewText",
                table: "Blogs");
        }
    }
}
