CREATE TABLE [dbo].[Stock]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ItemId] INT NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [Quantity] INT NULL, 
    [GivenOn] DATETIME NULL, 
    [IsPaid] BIT NOT NULL DEFAULT 0, 
    [TotalAmount] MONEY NULL
	
	FOREIGN KEY (ItemId) REFERENCES Items(Id)
	FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
)
