CREATE TABLE [GRP].[GroupRequest]
(
	[GroupRequestId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [GroupId] INT NOT NULL, 
    [CreatedDateUTC] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_GroupRequest_User] FOREIGN KEY ([UserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_GroupRequest_Group] FOREIGN KEY ([GroupId]) REFERENCES [GRP].[Group]([GroupId])
)
