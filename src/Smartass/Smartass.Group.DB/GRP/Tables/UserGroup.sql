CREATE TABLE [GRP].[UserGroup]
(
	[UserGroupId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [GroupId] INT NOT NULL, 
    [IsAdmin] BIT NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [IsDeleted] BIT NOT NULL, 
    [CreatedByUserId] INT NOT NULL, 
    [LastModifiedByUserId] INT NULL, 
    [CreatedDateUTC] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateUTC] DATETIME NULL, 
    CONSTRAINT [FK_UserGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [GRP].[Group]([GroupId]),
    CONSTRAINT [FK_UserGroup_User_CreatedBy] FOREIGN KEY ([CreatedByUserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_UserGroup_User_LastModifiedBy] FOREIGN KEY ([LastModifiedByUserId]) REFERENCES [USR].[User]([UserId])
)
