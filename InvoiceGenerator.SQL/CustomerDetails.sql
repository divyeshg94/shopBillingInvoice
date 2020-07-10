CREATE TABLE [dbo].[CustomerDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL,
	Foreign key ([CustomerId]) REFERENCES Customers(Id), 
    [SkinType] NVARCHAR(50) NULL, 
    [HairType] NVARCHAR(50) NULL, 
    [IsSensitiveSkin] bit NULL, 
    [MedicalHistory] NVARCHAR(MAX) NULL,
    [Allergies] NVARCHAR(MAX) NULL,
    [Problems] NVARCHAR(MAX) NULL,
    [CurrentProducts] NVARCHAR(MAX) NULL
)
