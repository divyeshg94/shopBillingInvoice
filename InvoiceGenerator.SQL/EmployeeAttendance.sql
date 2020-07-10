CREATE TABLE [dbo].[EmployeeAttendance]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeId] INT NOT NULL, 
	Foreign key ([EmployeeId]) REFERENCES Employees(Id), 
    [CheckIn] DATETIME NULL, 
    [CheckOut] DATETIME NULL, 
    [IsPresent] BIT NULL, 
    [Duration] FLOAT NULL
)
