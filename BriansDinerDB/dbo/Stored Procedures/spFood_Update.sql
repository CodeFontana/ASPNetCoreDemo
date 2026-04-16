CREATE PROCEDURE [dbo].[spFood_Update]
	@pId int,
	@pTitle nvarchar(50),
	@pDescription nvarchar(250),
	@pPrice money
AS
begin
	set nocount on;

	update dbo.Food
	set [Title] = @pTitle
		,[Description] = @pDescription
		,[Price] = @pPrice
	where Id = @pId;
end
