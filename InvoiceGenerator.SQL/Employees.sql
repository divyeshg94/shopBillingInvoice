CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [EmployeeId] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [JoinedOn] DATETIME NULL, 
    [ReleavedOn] DATETIME NULL, 
    [IsExists] BIT NOT NULL DEFAULT 1
)
