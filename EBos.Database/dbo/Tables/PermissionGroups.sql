CREATE TABLE [dbo].[PermissionGroups] (
    [PermissionGroupID]   UNIQUEIDENTIFIER NOT NULL,
    [PermissionGroupName] VARCHAR (200)    NOT NULL,
    CONSTRAINT [PK_PermissionGroups] PRIMARY KEY CLUSTERED ([PermissionGroupID] ASC)
);

