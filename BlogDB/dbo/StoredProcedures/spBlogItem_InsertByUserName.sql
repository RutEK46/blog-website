CREATE PROCEDURE [dbo].[spBlogItem_InsertByUserName]
	@Title NVARCHAR(100),
	@UserName NVARCHAR(50),
	@Created DATETIME
AS
	INSERT INTO [dbo].[BlogItem] (Title, UserId, Created)
	VALUES (@Title, (SELECT Id FROM [dbo].[User] WHERE UserName = @UserName), @Created)
RETURN 0
