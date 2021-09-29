CREATE PROCEDURE [dbo].[spPost_SelectByTitle]
	@Title NVARCHAR(100)
AS
	SET NOCOUNT ON;
	SELECT * from [dbo].[BlogItem] bi
	left join [dbo].[Post] p
	ON [bi].[Id] = [p].[Id]
	WHERE [bi].[Title] = @Title
RETURN 0
