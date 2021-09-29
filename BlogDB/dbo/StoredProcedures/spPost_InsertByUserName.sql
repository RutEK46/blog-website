CREATE PROCEDURE [dbo].[spPost_InsertByUserName]
	@Title NVARCHAR(100),
	@UserName NVARCHAR(50),
	@Created DATETIME,
	@Body NVARCHAR(MAX)
AS
	BEGIN
		EXEC [dbo].[spBlogItem_InsertByUserName] @Title, @UserName, @Created

		INSERT INTO [dbo].[Post] (Id, Body)
		VALUES ((SELECT Id FROM [dbo].[BlogItem] WHERE Title = @Title), @Body)
	END
RETURN 0
