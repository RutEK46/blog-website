CREATE PROCEDURE [dbo].[spBlogItem_SelectById]
	@Id INT
AS
	SET NOCOUNT ON;
	SELECT * from [dbo].[BlogItem] bi
	WHERE [bi].[Id] = @Id
RETURN 0
