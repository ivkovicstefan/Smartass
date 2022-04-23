CREATE TABLE [GRP].[Group]
(
	[GroupId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GroupName] NVARCHAR(40) NOT NULL, 
    [GroupDescription] NVARCHAR(400) NULL, 
    [IsActive] BIT NOT NULL, 
    [IsDeleted] BIT NOT NULL, 
    [ProfileImageId] INT NULL, 
    [CreatedByUserId] INT NOT NULL , 
    [LastModifiedByUserId] INT NULL, 
    [CreatedDateUTC] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateUTC] DATETIME NULL, 
    CONSTRAINT [FK_Group_User_CreatedBy] FOREIGN KEY ([CreatedByUserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_Group_User_LastModifiedBy] FOREIGN KEY ([LastModifiedByUserId]) REFERENCES [USR].[User]([UserId])
)
