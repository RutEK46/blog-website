CREATE PROCEDURE [dbo].[spPost_Delete]
	@Id INT
AS
	DELETE FROM dbo.Post
	WHERE Id = @Id

	EXEC dbo.spBlogItem_Delete @Id

RETURN 0
