CREATE TABLE [GRP].[GroupInvite]
(
	[GroupInviteId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FromUserId] INT NOT NULL, 
    [ToUserId] INT NOT NULL, 
    [FromGroupId] INT NOT NULL, 
    [CreatedDateUTC] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_GroupInvite_User_From] FOREIGN KEY ([FromUserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_GroupInvite_User_To] FOREIGN KEY ([ToUserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_GroupInvite_Group_From] FOREIGN KEY ([FromGroupId]) REFERENCES [GRP].[Group]([GroupId])
)
