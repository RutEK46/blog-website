CREATE PROCEDURE [dbo].[spBlogItem_Insert]
	@Title NVARCHAR(100),
	@UserId INT,
	@Created DATETIME
AS
	BEGIN
		INSERT INTO [dbo].[BlogItem] (Title, UserId, Created)
		VALUES (@Title, @UserId, @Created)
	END
RETURN 0
