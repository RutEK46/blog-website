CREATE PROCEDURE [dbo].[spUser_SelectByEmail]
	@Email nchar(50)
AS
	SET NOCOUNT ON; 
	SELECT Id, UserName, Email, Salt, PasswordHash FROM [dbo].[User]
	WHERE Email = @Email
RETURN 0
