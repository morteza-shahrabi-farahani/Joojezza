CREATE TABLE [dbo].[Table] (
    [id]        VARCHAR (250) NOT NULL,
    [password]  VARCHAR (250) NOT NULL,
    [name]      VARCHAR (250) DEFAULT ('Joojezza') NOT NULL,
    [phone]     VARCHAR (250) DEFAULT ((33333333)) NULL,
    [email]     VARCHAR (250) DEFAULT ('Joojezza@gmail.com') NULL,
    [address]   VARCHAR (250) DEFAULT ('Joojezza Street') NULL,
    [imageFile] VARCHAR (250) DEFAULT ('G:/works/university/AP/Joojezza/Joojezza/logo/logo.png') NULL,
    [counter]   INT           NULL DEFAULT 0,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

