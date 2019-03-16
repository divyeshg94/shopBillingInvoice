CREATE TABLE [dbo].[Invoice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] int NULL 
	Foreign key (CustomerId) REFERENCES Customers(Id), 
    [EmployeeId] INT NOT NULL
	Foreign key ([EmployeeId]) REFERENCES Employees(Id), 
    [TotalAmount] NVARCHAR(50) NOT NULL, 
	[SaleDate] Date NOT NULL
)
