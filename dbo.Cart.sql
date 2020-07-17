CREATE TABLE [dbo].[Cart] (
    [date]        VARCHAR (MAX) NULL,
    [Time1]       VARCHAR (MAX) NULL,
    [number]      VARCHAR (MAX) NULL,
    [type]        VARCHAR (MAX) NULL,
    [name]        VARCHAR (MAX) NULL,
    [description] VARCHAR (MAX) NULL,
    [price]       VARCHAR (MAX) NULL,
    [imageFIle]   VARCHAR (MAX) NULL,
    [Time2]       VARCHAR (MAX) NULL,
    [Time3]       VARCHAR (MAX) NULL,
    [Time4]       VARCHAR (MAX) NULL,
    [Id]          INT           NOT NULL,
    [userID]      VARCHAR (MAX) NULL,
    [chief]       VARCHAR (MAX) NULL,
    [customers]   INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

