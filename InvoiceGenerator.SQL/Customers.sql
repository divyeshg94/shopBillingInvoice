﻿CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [RegisteredOn] DATETIME NULL
)
