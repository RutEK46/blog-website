CREATE PROCEDURE [dbo].[spBlogItem_Delete]
	@Id INT
AS
	DELETE FROM dbo.BlogItem
	WHERE Id = @Id
RETURN 0