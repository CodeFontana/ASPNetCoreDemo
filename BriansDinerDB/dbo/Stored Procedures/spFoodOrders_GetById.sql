CREATE PROCEDURE [dbo].[spFoodOrders_GetById]
	@pId int
AS
begin
	set nocount on;

	select [Id], [OrderName], [OrderDate], [FoodId], [Quantity], [Total]
	from dbo.FoodOrder
	where Id = @pId;
end
