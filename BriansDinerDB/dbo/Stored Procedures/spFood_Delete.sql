CREATE PROCEDURE [dbo].[spFood_Delete]
	@pId int
AS
begin
	set nocount on;

	delete
	from dbo.Food
	where Id = @pId;
end
