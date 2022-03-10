CREATE TABLE [dbo].[Customers] (
    [CustomerID]      UNIQUEIDENTIFIER NOT NULL,
    [CustomerNo]      INT              NOT NULL,
    [CustomerName]    VARCHAR (75)     NOT NULL,
    [CustomerSurname] VARCHAR (75)     NOT NULL,
    [Address]         VARCHAR (100)    NULL,
    [PostCode]        INT              NOT NULL,
    [Country]         VARCHAR (100)    NOT NULL,
    [DateOfBirth]     DATETIME         NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

