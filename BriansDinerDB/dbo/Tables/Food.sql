﻿CREATE TABLE [dbo].[Food]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL, 
    [Price] MONEY NOT NULL,
    constraint unq_Food_Title unique(Title)
)
GO

