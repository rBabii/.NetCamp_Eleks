using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    public partial class AddInsertUpdateTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Post_Inserted')
	                        DROP TRIGGER Post_Inserted;
                        GO
                        CREATE TRIGGER Post_Inserted
                        ON Posts
                        AFTER INSERT
                        AS
                        INSERT INTO PostSearches (ps_PostId, ps_FullPostText)
                        SELECT p_PostId, p_Title + ' ' + p_SubTitle + ' ' + p_HeaderContent + ' ' + p_MainContent + ' ' + p_FooterContent + ' ' + p_PreviewText + ' ' + p_ImageName
                        FROM INSERTED
                        GO


                        IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Post_Updated')
	                        DROP TRIGGER Post_Updated;
                        GO
                        CREATE TRIGGER Post_Updated
                        ON Posts
                        AFTER UPDATE
                        AS
                        update PostSearches
	                        set ps_FullPostText = INSERTED.p_Title + ' ' + INSERTED.p_SubTitle + ' ' + INSERTED.p_HeaderContent + ' ' + INSERTED.p_MainContent + ' ' + INSERTED.p_FooterContent + ' ' + INSERTED.p_PreviewText + ' ' + INSERTED.p_ImageName
	                        FROM INSERTED
	                        WHERE ps_PostId = INSERTED.p_PostId
                        GO


                        IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Blog_Inserted')
	                        DROP TRIGGER Blog_Inserted;
                        GO
                        CREATE TRIGGER Blog_Inserted
                        ON Blogs
                        AFTER INSERT
                        AS
                        INSERT INTO BlogSearches(bs_BlogId, bs_FullBlogText)
                        SELECT b_BlogId, b_Title + ' ' + b_SubTitle + ' ' + b_PreviewText + ' ' + b_ImageName
                        FROM INSERTED
                        GO


                        IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Blog_Updated')
	                        DROP TRIGGER Blog_Updated;
                        GO
                        CREATE TRIGGER Blog_Updated
                        ON Blogs
                        AFTER UPDATE
                        AS
                        update BlogSearches
	                        set bs_FullBlogText = INSERTED.b_Title + ' ' + INSERTED.b_SubTitle + ' ' + INSERTED.b_PreviewText + ' ' + INSERTED.b_ImageName
	                        FROM INSERTED
	                        WHERE bs_BlogId = INSERTED.b_BlogId
                        GO";

            migrationBuilder.Sql(sp);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Post_Inserted')
	                        DROP TRIGGER Post_Inserted;
                        GO
                        IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Post_Updated')
	                        DROP TRIGGER Post_Updated;
                        GO
                        IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Blog_Inserted')
	                        DROP TRIGGER Blog_Inserted;
                        GO
                        IF EXISTS (SELECT 1 FROM sys.triggers 
                                   WHERE Name = 'Blog_Updated')
	                        DROP TRIGGER Blog_Updated;
                        GO";

            migrationBuilder.Sql(sp);
        }
    }
}
