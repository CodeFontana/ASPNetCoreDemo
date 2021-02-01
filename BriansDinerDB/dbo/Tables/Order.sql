CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderName] NVARCHAR(50) NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL, 
    [FoodId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Total] MONEY NOT NULL, 
    CONSTRAINT [FK_Order_Food] FOREIGN KEY ([FoodId]) REFERENCES [Food]([Id])
)
