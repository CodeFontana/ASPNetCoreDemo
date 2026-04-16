CREATE PROCEDURE [dbo].[spFood_Insert]
	@pTitle nvarchar(50),
	@pDescription nvarchar(250),
	@pPrice money,
	@oId int output
AS
begin
	set nocount on;

	insert into dbo.Food([Title], [Description], [Price])
	values (@pTitle, @pDescription, @pPrice);

	set @oId = SCOPE_IDENTITY();
end
