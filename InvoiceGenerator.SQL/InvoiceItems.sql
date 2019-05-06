CREATE TABLE [dbo].[InvoiceItems]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvoiceId] INT NULL, 
	Foreign key (InvoiceId) REFERENCES Invoice(Id), 
    [ItemId] INT NULL
	Foreign key (ItemId) REFERENCES Items(Id), 
	[Cost] NVARCHAR(50) NULL, 
    [Quantity] INT NOT NULL
)
