CREATE TABLE [GRP].[Script]
(
	[ScriptId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ScriptTitle] NVARCHAR(50) NULL, 
    [ScriptContent] NVARCHAR(MAX) NULL, 
    [SectionId] INT NOT NULL, 
    [CreatedByUserId] INT NOT NULL, 
    [LastModifiedByUserId] INT NULL, 
    [CreatedDateUTC] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateUTC] DATETIME NULL, 
    CONSTRAINT [FK_Script_Section] FOREIGN KEY ([SectionId]) REFERENCES [GRP].[Section]([SectionId]),
    CONSTRAINT [FK_Script_User_CreatedBy] FOREIGN KEY ([CreatedByUserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_Script_User_LastModifiedBy] FOREIGN KEY ([LastModifiedByUserId]) REFERENCES [USR].[User]([UserId])
)
