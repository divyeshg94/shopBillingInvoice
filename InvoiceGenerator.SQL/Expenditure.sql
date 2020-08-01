CREATE TABLE [dbo].[Expenditure]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NOT NULL, 
    [Type] NCHAR(50) NOT NULL, 
    [Amount] MONEY NOT NULL, 
    [BillDate] DATE NOT NULL, 
    [Notes] NVARCHAR(500) NULL
)