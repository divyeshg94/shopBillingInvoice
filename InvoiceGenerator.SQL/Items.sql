CREATE TABLE [dbo].[Items]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [Category] NVARCHAR(50) NULL, 
    [Size] NVARCHAR(50) NULL,
    [Type] int NULL,
    [Price] NVARCHAR(50) NULL, 
    [IsAvailable] BIT NOT NULL DEFAULT 1, 
    [IsProduced] BIT NOT NULL DEFAULT 1, 
    [DateOfPurchase] DATE NULL 
)
