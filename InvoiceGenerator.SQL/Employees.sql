CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeId] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [AadharNo] NVARCHAR(50) NULL,
    [JoinedOn] DATETIME NULL, 
    [ReleavedOn] DATETIME NULL, 
    [IsExists] BIT NOT NULL DEFAULT 1, 
    [Photo] VARBINARY(MAX) NULL, 
    [AadharImage] VARBINARY(MAX) NULL, 
    [Address] VARCHAR(MAX) NULL, 
    [EmailId] VARCHAR(150) NULL 
)
