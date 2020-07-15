CREATE TABLE [dbo].[Cart]
(
	[date]        VARCHAR (MAX) NULL,
    [Time1]        VARCHAR (MAX) NULL,
    [number]      VARCHAR (MAX) NULL,
    [type]        VARCHAR (MAX) NULL,
    [name]        VARCHAR (MAX) NOT NULL,
    [description] VARCHAR (MAX) NULL,
    [price]       VARCHAR (MAX) NULL,
    [imageFIle] VARCHAR(MAX) NULL, 
    [Time2] VARCHAR(MAX) NULL, 
    [Time3] VARCHAR(MAX) NULL, 
    [TIme4] VARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Cart] PRIMARY KEY ([name]), 
    
)
