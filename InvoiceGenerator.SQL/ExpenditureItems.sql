CREATE TABLE [dbo].[ExpenditureItems]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ExpenditureId] INT NOT NULL, 
	Foreign key ([ExpenditureId]) REFERENCES Expenditure(Id), 
    [Name] NVARCHAR(100) NOT NULL, 
    [Quantity] INT NOT NULL, 
	[UnitPrice] NVARCHAR(50) NOT NULL,
	[TotalPrice] NVARCHAR(50) NOT NULL 
)
