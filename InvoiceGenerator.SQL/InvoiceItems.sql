CREATE TABLE [dbo].[InvoiceItems]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [InvoiceId] INT NULL, 
	Foreign key (InvoiceId) REFERENCES Invoice(Id), 
    [ItemId] INT NULL
	Foreign key (ItemId) REFERENCES Items(Id), 
)
