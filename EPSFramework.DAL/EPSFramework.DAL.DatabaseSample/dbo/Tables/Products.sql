CREATE TABLE [dbo].[Products] (
    [Id]          INT           NOT NULL,
    [Description] VARCHAR (500) NOT NULL,
    [Price]       MONEY         DEFAULT ((2.1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

