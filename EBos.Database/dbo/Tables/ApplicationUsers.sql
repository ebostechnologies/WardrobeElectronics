CREATE TABLE [dbo].[ApplicationUsers] (
    [ApplicationUserID] UNIQUEIDENTIFIER NOT NULL,
    [Username]          VARCHAR (50)     NOT NULL,
    [Password]          VARCHAR (75)     NOT NULL,
    CONSTRAINT [PK_ApplicationUsers] PRIMARY KEY CLUSTERED ([ApplicationUserID] ASC)
);

