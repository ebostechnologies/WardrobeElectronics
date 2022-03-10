CREATE TABLE [dbo].[ApplicationUserPermissionGroups] (
    [ApplicationUserPermissionGroupID] UNIQUEIDENTIFIER NOT NULL,
    [ApplicationUserID]                UNIQUEIDENTIFIER NOT NULL,
    [PermissionGroupID]                UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ApplicationUserPermissionGroups] PRIMARY KEY CLUSTERED ([ApplicationUserPermissionGroupID] ASC),
    CONSTRAINT [FK_ApplicationUserPermissionGroups_ApplicationUsers] FOREIGN KEY ([ApplicationUserID]) REFERENCES [dbo].[ApplicationUsers] ([ApplicationUserID]),
    CONSTRAINT [FK_ApplicationUserPermissionGroups_PermissionGroups] FOREIGN KEY ([PermissionGroupID]) REFERENCES [dbo].[PermissionGroups] ([PermissionGroupID])
);

