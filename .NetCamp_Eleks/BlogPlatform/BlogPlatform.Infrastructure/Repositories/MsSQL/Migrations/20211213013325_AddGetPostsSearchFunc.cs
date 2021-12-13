using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    public partial class AddGetPostsSearchFunc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"IF EXISTS (SELECT *
								   FROM   sys.objects
								   WHERE  object_id = OBJECT_ID(N'[dbo].[GetPosts]')
										  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
						  DROP FUNCTION [dbo].[GetPosts]
						GO  
						CREATE FUNCTION [dbo].[GetPosts] (
									@PageNumber AS INT,
									@RowsOfPage AS INT,
									@BlogUrl as NVARCHAR(4000),
									@SearchText as NVARCHAR(4000)
						)
						RETURNS @Posts TABLE (
							[p_PostId] INT NOT NULL,
							[p_BlogId] INT NOT NULL, 
							[p_DateCreated] DATETIME2(7) NOT NULL, 
							[p_DatePosted] DATETIME2(7) NOT NULL,  
							[p_Blocked] BIT NOT NULL,
							[p_Visible] BIT NOT NULL,
							[p_Title] NVARCHAR(150) NULL,
							[p_SubTitle] NVARCHAR(150) NULL, 
							[p_HeaderContent] NVARCHAR(MAX) NULL, 
							[p_MainContent] NVARCHAR(MAX) NULL,
							[p_FooterContent] NVARCHAR(MAX) NULL,
							[p_ImageName] NVARCHAR(150) NULL,
							[p_PreviewText] NVARCHAR(150) NULL
						)
						AS
						BEGIN
	
							IF(@SearchText IS NULL OR @SearchText = '')
								set @SearchText = '''';

							INSERT INTO @Posts
							select 
								[p].[p_PostId],
								[p].[p_BlogId],
								[p].[p_DateCreated],
								[p].[p_DatePosted], 
								[p].[p_Blocked],
								[p].[p_Visible],
								[p].[p_Title],
								[p].[p_SubTitle],
								[p].[p_HeaderContent],
								[p].[p_MainContent],
								[p].[p_FooterContent],
								[p].[p_ImageName],
								[p].[p_PreviewText]
							from Posts as p
								JOIN PostSearches as ps
								on p.p_PostId = ps.ps_PostId
								JOIN Blogs as b
								on b.b_BlogId = p.p_BlogId
							where 
							p.p_Visible = 1 
							and p.p_Blocked = 0 
							and (@BlogUrl is NULL or @BlogUrl = '' or b.b_BlogUrl = @BlogUrl)
							and (@SearchText is NULL or @SearchText = '''' or CONTAINS(ps.ps_FullPostText, @SearchText) )

							order by p.p_PostId
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
								   WHERE  object_id = OBJECT_ID(N'[dbo].[GetPosts]')
										  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
						  DROP FUNCTION [dbo].[GetPosts]
						GO";

			migrationBuilder.Sql(sp);
		}
    }
}
