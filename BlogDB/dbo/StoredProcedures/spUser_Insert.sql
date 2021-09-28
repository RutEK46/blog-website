CREATE PROCEDURE [dbo].[spUser_Insert]
	@UserName nvarchar(50),
	@Email nchar(50),
	@Salt nchar(50),
	@PasswordHash nchar(50)
AS
	IF not exists (SELECT [u].UserName, [u].Email 
					FROM [dbo].[User] u
					WHERE [u].UserName = @UserName
					or [u].Email = @Email)
	BEGIN
		INSERT INTO [dbo].[User] (UserName, Email, Salt, PasswordHash)
		VALUES (@UserName, @Email, @Salt, @PasswordHash)
		RETURN 1
	END
RETURN 0