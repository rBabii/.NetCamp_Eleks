using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    public partial class AddFullTextCatalogWithIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"if(COLUMNPROPERTY(OBJECT_ID('dbo.BlogSearches'), 'bs_FullBlogText', 'IsFulltextIndexed') = 1)
	                        DROP FULLTEXT INDEX ON dbo.BlogSearches; 
                        GO
                        if(COLUMNPROPERTY(OBJECT_ID('dbo.PostSearches'), 'ps_FullPostText', 'IsFulltextIndexed') = 1)
	                        DROP FULLTEXT INDEX ON dbo.PostSearches; 
                        GO
                        IF EXISTS (SELECT 1 FROM sys.fulltext_catalogs WHERE [name] = 'SearchTextCatalog')
	                        DROP FULLTEXT CATALOG SearchTextCatalog 
                        GO
                        CREATE FULLTEXT CATALOG SearchTextCatalog
                          WITH ACCENT_SENSITIVITY = ON
                          AS DEFAULT
                          AUTHORIZATION dbo
                        GO


                        if(COLUMNPROPERTY(OBJECT_ID('dbo.BlogSearches'), 'bs_FullBlogText', 'IsFulltextIndexed') = 1)
	                        DROP FULLTEXT INDEX ON dbo.BlogSearches; 
                        GO
                        CREATE FULLTEXT INDEX ON BlogSearches(bs_FullBlogText)
                          KEY INDEX PK_BlogSearches ON (SearchTextCatalog)
                          WITH (CHANGE_TRACKING AUTO)
                        GO


                        if(COLUMNPROPERTY(OBJECT_ID('dbo.PostSearches'), 'ps_FullPostText', 'IsFulltextIndexed') = 1)
	                        DROP FULLTEXT INDEX ON dbo.BlogSearches; 
                        GO
                        CREATE FULLTEXT INDEX ON PostSearches(ps_FullPostText)
                          KEY INDEX PK_PostSearches ON (SearchTextCatalog)
                          WITH (CHANGE_TRACKING AUTO)
                        GO";

            migrationBuilder.Sql(sp, suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"if(COLUMNPROPERTY(OBJECT_ID('dbo.BlogSearches'), 'bs_FullBlogText', 'IsFulltextIndexed') = 1)
	                        DROP FULLTEXT INDEX ON dbo.BlogSearches; 
                        GO
                        if(COLUMNPROPERTY(OBJECT_ID('dbo.PostSearches'), 'ps_FullPostText', 'IsFulltextIndexed') = 1)
	                        DROP FULLTEXT INDEX ON dbo.PostSearches; 
                        GO
                        IF EXISTS (SELECT 1 FROM sys.fulltext_catalogs WHERE [name] = 'SearchTextCatalog')
	                        DROP FULLTEXT CATALOG SearchTextCatalog 
                        GO";

            migrationBuilder.Sql(sp, suppressTransaction: true);
        }
    }
}
