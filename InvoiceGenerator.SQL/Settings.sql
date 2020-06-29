CREATE TABLE [dbo].[Settings]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Key] NVARCHAR(MAX) NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    [Group] NVARCHAR(MAX) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL
)
