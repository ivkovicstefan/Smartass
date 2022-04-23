CREATE TABLE [GRP].[Post]
(
	[PostId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PostText] NVARCHAR(MAX) NULL,
    [ParentEntityId] INT NOT NULL,
    [PostTypeId] INT NOT NULL, 
    [CreatedByUserId] INT NOT NULL , 
    [CreatedDateUTC] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_Post_User_CreatedBy] FOREIGN KEY ([CreatedByUserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_Post_PostType] FOREIGN KEY ([PostTypeId]) REFERENCES [GRP].[PostType]([PostTypeId])
)
