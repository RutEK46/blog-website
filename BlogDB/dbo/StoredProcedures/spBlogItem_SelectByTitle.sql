CREATE PROCEDURE [dbo].[spBlogItem_SelectByTitle]
	@Title NVARCHAR(100)
AS
	SET NOCOUNT ON;
	SELECT * from [dbo].[BlogItem] bi
	WHERE [bi].[Title] = @Title
RETURN 0
