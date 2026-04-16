CREATE PROCEDURE [dbo].[spFoodOrders_Insert]
	@pOrderName nvarchar(50),
	@pOrderDate datetime2(7),
	@pFoodId int,
	@pQuantity int,
	@pTotal money,
	@oId int output
AS
begin
	set nocount on;

	insert into dbo.FoodOrder(OrderName, OrderDate, FoodId, Quantity, Total)
	values (@pOrderName, @pOrderDate, @pFoodId, @pQuantity, @pTotal);

	set @oId = SCOPE_IDENTITY();
end
