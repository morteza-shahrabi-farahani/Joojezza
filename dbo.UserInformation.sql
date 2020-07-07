CREATE TABLE [dbo].[UserInformation] (
    [email]    VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NULL,
    [name]     VARCHAR (50) NULL,
    [phone]    VARCHAR (50) NULL,
    [id]       VARCHAR (50) NULL,
    [address] VARCHAR(250) NULL, 
    PRIMARY KEY CLUSTERED ([email] ASC)
);

