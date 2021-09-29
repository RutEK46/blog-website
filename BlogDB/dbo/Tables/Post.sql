CREATE TABLE [dbo].[Post]
(
	[Id] INT NOT NULL PRIMARY KEY,
    [Body] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [FK_Post_BlogItem_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[BlogItem] ([Id])
)
