CREATE TABLE [GRP].[Section]
(
	[SectionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SectionName] NVARCHAR(50) NOT NULL, 
    [SectionDescription] NVARCHAR(200) NULL, 
    [GroupId] INT NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [IsDeleted] BIT NOT NULL, 
    [CreatedByUserId] INT NOT NULL, 
    [LastModifiedByUserId] INT NULL, 
    [CreatedDateUTC] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateUTC] DATETIME NULL, 
    CONSTRAINT [FK_Section_Group] FOREIGN KEY ([GroupId]) REFERENCES [GRP].[Group]([GroupId]),
    CONSTRAINT [FK_Section_User_CreatedBy] FOREIGN KEY ([CreatedByUserId]) REFERENCES [USR].[User]([UserId]), 
    CONSTRAINT [FK_Section_User_LastModifiedBy] FOREIGN KEY ([LastModifiedByUserId]) REFERENCES [USR].[User]([UserId])
)
