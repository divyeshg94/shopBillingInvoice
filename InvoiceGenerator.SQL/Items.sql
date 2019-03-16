CREATE TABLE [dbo].[Items]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Category] NVARCHAR(50) NULL, 
    [Price] NVARCHAR(50) NULL, 
    [IsAvailable] BIT NOT NULL DEFAULT 1
)
