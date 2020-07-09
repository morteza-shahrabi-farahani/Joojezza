CREATE TABLE [dbo].[UserInformation] (
    [email]    VARCHAR (250) NOT NULL,
    [password] VARCHAR (250) NULL,
    [name]     VARCHAR (250) NULL,
    [phone]    VARCHAR (250) NULL,
    [id]       VARCHAR (250) NULL,
    [address]  VARCHAR (250) NULL,
    [image] IMAGE NULL, 
    [filename] VARBINARY(MAX) NULL DEFAULT 'logo.png', 
    PRIMARY KEY CLUSTERED ([email] ASC)
);

