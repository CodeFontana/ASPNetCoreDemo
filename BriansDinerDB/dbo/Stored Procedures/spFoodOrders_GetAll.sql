CREATE PROCEDURE [dbo].[spFoodOrders_GetAll]
AS
begin
	set nocount on;

	select fo.[Id]
		,fo.[OrderName]
		,fo.[OrderDate]
		,fo.[FoodId]
		,f.[Title] as [FoodTitle]
		,fo.[Quantity]
		,fo.[Total]
	from dbo.FoodOrder fo
		inner join dbo.Food f on fo.FoodId = f.Id
	order by fo.[OrderDate] desc;
end
