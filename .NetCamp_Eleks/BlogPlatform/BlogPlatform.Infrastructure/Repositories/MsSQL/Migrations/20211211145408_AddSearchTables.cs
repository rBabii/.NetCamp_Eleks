using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    public partial class AddSearchTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogSearches",
                columns: table => new
                {
                    BlogSearchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    FullBlogText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSearches", x => x.BlogSearchId);
                    table.ForeignKey(
                        name: "FK_BlogSearches_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostSearches",
                columns: table => new
                {
                    PostSearchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    FullPostText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSearches", x => x.PostSearchId);
                    table.ForeignKey(
                        name: "FK_PostSearches_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogSearches_BlogId",
                table: "BlogSearches",
                column: "BlogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostSearches_PostId",
                table: "PostSearches",
                column: "PostId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogSearches");

            migrationBuilder.DropTable(
                name: "PostSearches");
        }
    }
}
