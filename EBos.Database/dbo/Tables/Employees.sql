CREATE TABLE [dbo].[Employees] (
    [EmployeeID]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationUserID] UNIQUEIDENTIFIER NOT NULL,
    [FirstName]         VARCHAR (50)     NOT NULL,
    [LastName]          VARCHAR (50)     NOT NULL,
    [DoB]               DATETIME         NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    CONSTRAINT [FK_Employees_ApplicationUsers] FOREIGN KEY ([ApplicationUserID]) REFERENCES [dbo].[ApplicationUsers] ([ApplicationUserID])
);

