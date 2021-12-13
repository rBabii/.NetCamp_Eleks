using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    public partial class AddGetBlogsSearchFunc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sp = @"IF EXISTS (SELECT *
								   FROM   sys.objects
								   WHERE  object_id = OBJECT_ID(N'[dbo].[GetBlogs]')
										  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
						  DROP FUNCTION [dbo].[GetBlogs]
						GO  
						CREATE FUNCTION [dbo].[GetBlogs] (
									@PageNumber AS INT,
									@RowsOfPage AS INT,
									@BlogId AS INT,
									@BlogUrl as NVARCHAR(4000),
									@SearchText as NVARCHAR(4000)
						)
						RETURNS @Blogs TABLE (
							[b_BlogId] INT NOT NULL,
							[b_UserId] INT NOT NULL,
							[b_BlogUrl] NVARCHAR(150) NULL,
							[b_DateCreated] DATETIME2(7) NOT NULL,
							[b_Blocked] BIT NOT NULL,
							[b_Visible] BIT NOT NULL,
							[b_Title] NVARCHAR(150) NULL,
							[b_SubTitle] NVARCHAR(150) NULL,
							[b_ImageName] NVARCHAR(150) NULL,
							[b_PreviewText] NVARCHAR(150) NULL
						)
						AS
						BEGIN
	
							IF(@SearchText IS NULL OR @SearchText = '')
								set @SearchText = '''';

							INSERT INTO @Blogs
							select 
								[b].[b_BlogId],
								[b].[b_UserId],
								[b].[b_BlogUrl],
								[b].[b_DateCreated],
								[b].[b_Blocked],
								[b].[b_Visible],
								[b].[b_Title],
								[b].[b_SubTitle],
								[b].[b_ImageName],
								[b].[b_PreviewText]
							from Blogs as b
								JOIN BlogSearches as bs
								on b.b_BlogId = bs.bs_BlogId
							where 
							b.b_Visible = 1 
							and b.b_Blocked = 0
							and (@BlogId is NULL or @BlogId = 0 or b.b_BlogId = @BlogId)
							and (@BlogUrl is NULL or @BlogUrl = '' or b.b_BlogUrl = @BlogUrl)
							and (@SearchText is NULL or @SearchText = '''' or CONTAINS(bs.bs_FullBlogText, @SearchText) )

							order by b.b_BlogId
							OFFSET (@PageNumber-1)*@RowsOfPage ROWS
							FETCH NEXT @RowsOfPage ROWS ONLY

							RETURN;
						END;
						GO";

			migrationBuilder.Sql(sp);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var sp = @"IF EXISTS (SELECT *
								   FROM   sys.objects
								   WHERE  object_id = OBJECT_ID(N'[dbo].[GetBlogs]')
										  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
						  DROP FUNCTION [dbo].[GetBlogs]
						GO";

			migrationBuilder.Sql(sp);
		}
    }
}
