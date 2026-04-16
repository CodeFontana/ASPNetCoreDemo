CREATE PROCEDURE [dbo].[spFoodOrders_Delete]
	@pId int
AS
begin
	set nocount on;

	delete 
	from dbo.FoodOrder
	where Id = @pId;
end
