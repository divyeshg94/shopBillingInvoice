CREATE TABLE [dbo].[InvoiceItems]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvoiceId] INT NOT NULL, 
	Foreign key (InvoiceId) REFERENCES Invoice(Id), 
    [ItemId] INT NULL
	Foreign key (ItemId) REFERENCES Items(Id), 
    [Quantity] INT NOT NULL, 
	[DiscountPercent] INT NULL, 
    [DiscountAmount] NVARCHAR(50) NULL, 
	[ServicedBy] INT NOT NULL,
    Foreign Key (ServicedBy) References Employees(id),
	[UnitPrice] NVARCHAR(50) NOT NULL,
	[TotalPrice] NVARCHAR(50) NOT NULL 
)
