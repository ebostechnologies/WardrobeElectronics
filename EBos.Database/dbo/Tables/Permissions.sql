CREATE TABLE [dbo].[Permissions] (
    [PermissionID]          UNIQUEIDENTIFIER NOT NULL,
    [PermissionGroupID]     UNIQUEIDENTIFIER NOT NULL,
    [PermissionName]        VARCHAR (200)    NOT NULL,
    [PermissionDescription] TEXT             NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([PermissionID] ASC),
    CONSTRAINT [FK_Permissions_PermissionGroups] FOREIGN KEY ([PermissionGroupID]) REFERENCES [dbo].[PermissionGroups] ([PermissionGroupID])
);

