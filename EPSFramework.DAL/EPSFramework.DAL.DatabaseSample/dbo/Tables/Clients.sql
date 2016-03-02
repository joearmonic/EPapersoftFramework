CREATE TABLE [dbo].[Clients] (
    [Id]           INT           NOT NULL,
    [Name]         VARCHAR (50)  NOT NULL,
    [LastName]     VARCHAR (250) NOT NULL,
    [DateOfBirth]  DATETIME2 (7) NULL,
    [EmailAddress] VARCHAR (100) NULL,
    [IsDeleted_YN] CHAR (1)      DEFAULT ('N') NOT NULL,
    [Priority_YNC] CHAR (1)      DEFAULT ('N') NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

