﻿CREATE TABLE [USR].[User]
(
	[UserId] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(30) NOT NULL, 
    [LastName] NVARCHAR(30) NOT NULL, 
    [ProfileImage] VARBINARY(MAX) NULL
)
