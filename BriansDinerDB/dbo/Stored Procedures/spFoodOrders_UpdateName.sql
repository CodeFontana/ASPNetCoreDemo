CREATE PROCEDURE [dbo].[spFoodOrders_UpdateName]
	@pId int,
	@pOrderName nvarchar(50)
AS
begin
	set nocount on;

	update dbo.FoodOrder
	set OrderName = @pOrderName
	where Id = @pId;
end
