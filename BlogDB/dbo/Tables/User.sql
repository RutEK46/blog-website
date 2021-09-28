CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Email] NCHAR(50) NOT NULL, 
    [Salt] NCHAR(50) NOT NULL,
    [PasswordHash] NCHAR(50) NOT NULL
)
