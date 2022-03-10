CREATE TABLE [dbo].[CustomerCalls] (
    [CustomerCallID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]     UNIQUEIDENTIFIER NOT NULL,
    [CustomerNo]     INT              NOT NULL,
    [DateOfCall]     DATE             NOT NULL,
    [TimeOfCall]     TIME (7)         NOT NULL,
    [Subject]        VARCHAR (200)    NOT NULL,
    [Description]    TEXT             NULL,
    CONSTRAINT [PK_CustomerCalls] PRIMARY KEY CLUSTERED ([CustomerCallID] ASC),
    CONSTRAINT [FK_CustomerCalls_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);

