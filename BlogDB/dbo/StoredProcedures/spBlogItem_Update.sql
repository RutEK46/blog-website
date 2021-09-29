CREATE PROCEDURE [dbo].[spBlogItem_Update]
	@Id INT,
	@Title NVARCHAR(100)
AS
	UPDATE dbo.BlogItem
	set Title = @Title
	where Id = @Id
RETURN 0
