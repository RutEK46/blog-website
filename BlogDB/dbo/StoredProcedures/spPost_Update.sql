CREATE PROCEDURE [dbo].[spPost_Update]
	@Id INT,
	@Title NVARCHAR(100),
	@Body NVARCHAR(MAX)
AS
	EXEC [dbo].[spBlogItem_Update] @Id, @Title

	UPDATE dbo.Post
    set Body = @Body
    where Id = @Id;
RETURN 0
