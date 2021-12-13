using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    public partial class ChangeColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogSearches_Blogs_BlogId",
                table: "BlogSearches");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostSearches_Posts_PostId",
                table: "PostSearches");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogUrl",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "u_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "u_LastName");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "Users",
                newName: "u_IsVerified");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Users",
                newName: "u_ImageName");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "u_Gender");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "u_FirstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "u_Email");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Users",
                newName: "u_BlogId");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Users",
                newName: "u_BirthDate");

            migrationBuilder.RenameColumn(
                name: "AuthResourceUserId",
                table: "Users",
                newName: "u_AuthResourceUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "u_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AuthResourceUserId",
                table: "Users",
                newName: "IX_Users_u_AuthResourceUserId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostSearches",
                newName: "ps_PostId");

            migrationBuilder.RenameColumn(
                name: "FullPostText",
                table: "PostSearches",
                newName: "ps_FullPostText");

            migrationBuilder.RenameColumn(
                name: "PostSearchId",
                table: "PostSearches",
                newName: "ps_PostSearchId");

            migrationBuilder.RenameIndex(
                name: "IX_PostSearches_PostId",
                table: "PostSearches",
                newName: "IX_PostSearches_ps_PostId");

            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "Posts",
                newName: "p_Visible");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "p_Title");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "Posts",
                newName: "p_SubTitle");

            migrationBuilder.RenameColumn(
                name: "PreviewText",
                table: "Posts",
                newName: "p_PreviewText");

            migrationBuilder.RenameColumn(
                name: "MainContent",
                table: "Posts",
                newName: "p_MainContent");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Posts",
                newName: "p_ImageName");

            migrationBuilder.RenameColumn(
                name: "HeaderContent",
                table: "Posts",
                newName: "p_HeaderContent");

            migrationBuilder.RenameColumn(
                name: "FooterContent",
                table: "Posts",
                newName: "p_FooterContent");

            migrationBuilder.RenameColumn(
                name: "DatePosted",
                table: "Posts",
                newName: "p_DatePosted");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Posts",
                newName: "p_DateCreated");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Posts",
                newName: "p_BlogId");

            migrationBuilder.RenameColumn(
                name: "Blocked",
                table: "Posts",
                newName: "p_Blocked");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "p_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                newName: "IX_Posts_p_BlogId");

            migrationBuilder.RenameColumn(
                name: "FullBlogText",
                table: "BlogSearches",
                newName: "bs_FullBlogText");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "BlogSearches",
                newName: "bs_BlogId");

            migrationBuilder.RenameColumn(
                name: "BlogSearchId",
                table: "BlogSearches",
                newName: "bs_BlogSearchId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogSearches_BlogId",
                table: "BlogSearches",
                newName: "IX_BlogSearches_bs_BlogId");

            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "Blogs",
                newName: "b_Visible");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Blogs",
                newName: "b_UserId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Blogs",
                newName: "b_Title");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "Blogs",
                newName: "b_SubTitle");

            migrationBuilder.RenameColumn(
                name: "PreviewText",
                table: "Blogs",
                newName: "b_PreviewText");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Blogs",
                newName: "b_ImageName");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Blogs",
                newName: "b_DateCreated");

            migrationBuilder.RenameColumn(
                name: "BlogUrl",
                table: "Blogs",
                newName: "b_BlogUrl");

            migrationBuilder.RenameColumn(
                name: "Blocked",
                table: "Blogs",
                newName: "b_Blocked");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Blogs",
                newName: "b_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                newName: "IX_Blogs_b_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_u_Email",
                table: "Users",
                column: "u_Email",
                unique: true,
                filter: "[u_Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_u_PhoneNumber",
                table: "Users",
                column: "u_PhoneNumber",
                unique: true,
                filter: "[u_PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_b_BlogUrl",
                table: "Blogs",
                column: "b_BlogUrl",
                unique: true,
                filter: "[b_BlogUrl] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_b_UserId",
                table: "Blogs",
                column: "b_UserId",
                principalTable: "Users",
                principalColumn: "u_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogSearches_Blogs_bs_BlogId",
                table: "BlogSearches",
                column: "bs_BlogId",
                principalTable: "Blogs",
                principalColumn: "b_BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_p_BlogId",
                table: "Posts",
                column: "p_BlogId",
                principalTable: "Blogs",
                principalColumn: "b_BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostSearches_Posts_ps_PostId",
                table: "PostSearches",
                column: "ps_PostId",
                principalTable: "Posts",
                principalColumn: "p_PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_b_UserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogSearches_Blogs_bs_BlogId",
                table: "BlogSearches");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_p_BlogId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostSearches_Posts_ps_PostId",
                table: "PostSearches");

            migrationBuilder.DropIndex(
                name: "IX_Users_u_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_u_PhoneNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_b_BlogUrl",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "u_PhoneNumber",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "u_LastName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "u_IsVerified",
                table: "Users",
                newName: "IsVerified");

            migrationBuilder.RenameColumn(
                name: "u_ImageName",
                table: "Users",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "u_Gender",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "u_FirstName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "u_Email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "u_BlogId",
                table: "Users",
                newName: "BlogId");

            migrationBuilder.RenameColumn(
                name: "u_BirthDate",
                table: "Users",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "u_AuthResourceUserId",
                table: "Users",
                newName: "AuthResourceUserId");

            migrationBuilder.RenameColumn(
                name: "u_Id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_u_AuthResourceUserId",
                table: "Users",
                newName: "IX_Users_AuthResourceUserId");

            migrationBuilder.RenameColumn(
                name: "ps_PostId",
                table: "PostSearches",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "ps_FullPostText",
                table: "PostSearches",
                newName: "FullPostText");

            migrationBuilder.RenameColumn(
                name: "ps_PostSearchId",
                table: "PostSearches",
                newName: "PostSearchId");

            migrationBuilder.RenameIndex(
                name: "IX_PostSearches_ps_PostId",
                table: "PostSearches",
                newName: "IX_PostSearches_PostId");

            migrationBuilder.RenameColumn(
                name: "p_Visible",
                table: "Posts",
                newName: "Visible");

            migrationBuilder.RenameColumn(
                name: "p_Title",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "p_SubTitle",
                table: "Posts",
                newName: "SubTitle");

            migrationBuilder.RenameColumn(
                name: "p_PreviewText",
                table: "Posts",
                newName: "PreviewText");

            migrationBuilder.RenameColumn(
                name: "p_MainContent",
                table: "Posts",
                newName: "MainContent");

            migrationBuilder.RenameColumn(
                name: "p_ImageName",
                table: "Posts",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "p_HeaderContent",
                table: "Posts",
                newName: "HeaderContent");

            migrationBuilder.RenameColumn(
                name: "p_FooterContent",
                table: "Posts",
                newName: "FooterContent");

            migrationBuilder.RenameColumn(
                name: "p_DatePosted",
                table: "Posts",
                newName: "DatePosted");

            migrationBuilder.RenameColumn(
                name: "p_DateCreated",
                table: "Posts",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "p_BlogId",
                table: "Posts",
                newName: "BlogId");

            migrationBuilder.RenameColumn(
                name: "p_Blocked",
                table: "Posts",
                newName: "Blocked");

            migrationBuilder.RenameColumn(
                name: "p_PostId",
                table: "Posts",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_p_BlogId",
                table: "Posts",
                newName: "IX_Posts_BlogId");

            migrationBuilder.RenameColumn(
                name: "bs_FullBlogText",
                table: "BlogSearches",
                newName: "FullBlogText");

            migrationBuilder.RenameColumn(
                name: "bs_BlogId",
                table: "BlogSearches",
                newName: "BlogId");

            migrationBuilder.RenameColumn(
                name: "bs_BlogSearchId",
                table: "BlogSearches",
                newName: "BlogSearchId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogSearches_bs_BlogId",
                table: "BlogSearches",
                newName: "IX_BlogSearches_BlogId");

            migrationBuilder.RenameColumn(
                name: "b_Visible",
                table: "Blogs",
                newName: "Visible");

            migrationBuilder.RenameColumn(
                name: "b_UserId",
                table: "Blogs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "b_Title",
                table: "Blogs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "b_SubTitle",
                table: "Blogs",
                newName: "SubTitle");

            migrationBuilder.RenameColumn(
                name: "b_PreviewText",
                table: "Blogs",
                newName: "PreviewText");

            migrationBuilder.RenameColumn(
                name: "b_ImageName",
                table: "Blogs",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "b_DateCreated",
                table: "Blogs",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "b_BlogUrl",
                table: "Blogs",
                newName: "BlogUrl");

            migrationBuilder.RenameColumn(
                name: "b_Blocked",
                table: "Blogs",
                newName: "Blocked");

            migrationBuilder.RenameColumn(
                name: "b_BlogId",
                table: "Blogs",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_b_UserId",
                table: "Blogs",
                newName: "IX_Blogs_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogUrl",
                table: "Blogs",
                column: "BlogUrl",
                unique: true,
                filter: "[BlogUrl] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogSearches_Blogs_BlogId",
                table: "BlogSearches",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostSearches_Posts_PostId",
                table: "PostSearches",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
