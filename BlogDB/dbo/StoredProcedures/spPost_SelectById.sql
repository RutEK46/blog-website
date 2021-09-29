CREATE PROCEDURE [dbo].[spPost_SelectById]
	@Id INT
AS
	SET NOCOUNT ON;
	SELECT * from [dbo].[BlogItem] bi
	left join [dbo].[Post] p
	ON [p].[Id] = [bi].[Id]
	WHERE [p].[Id] = @Id
RETURN 0
