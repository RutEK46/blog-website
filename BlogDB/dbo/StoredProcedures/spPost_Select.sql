CREATE PROCEDURE [dbo].[spPost_Select]
AS
	SET NOCOUNT ON;
	SELECT * from [dbo].[BlogItem] bi
	left join [dbo].[Post] p
	ON [bi].[Id] = [p].[Id]
RETURN 0

