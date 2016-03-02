CREATE TABLE [dbo].[Orders] (
    [Id]       INT   NOT NULL,
    [ClientId] INT   NOT NULL,
    [Total]    MONEY NOT NULL,
    [ShippedOn] DATETIME2 NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_ToClients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id])
);

