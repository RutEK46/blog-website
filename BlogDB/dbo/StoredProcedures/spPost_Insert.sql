CREATE PROCEDURE [dbo].[spPost_Insert]
	@Title NVARCHAR(100),
	@UserId INT,
	@Created DATETIME,
	@Body NVARCHAR(MAX)
AS
	BEGIN
		EXEC [dbo].[spBlogItem_Insert] @Title, @UserId, @Created

		INSERT INTO [dbo].[Post] (Id, Body)
		VALUES ((SELECT Id FROM [dbo].[BlogItem] WHERE Title = @Title), @Body)
	END
RETURN 0
