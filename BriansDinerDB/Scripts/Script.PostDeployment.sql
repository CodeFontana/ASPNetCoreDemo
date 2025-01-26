/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select 1 from dbo.Food)
begin
    insert into dbo.Food([Title], [Description], [Price])
    values ('Cheeseburger Meal', 'A cheeseburger, fries and a drink.', '5.95'),
    ('Taco Meal', 'Steak Taco, salad and a drink.', '8.95'),
    ('Salad Meal', 'A cesear salad and a water.', '4.95');
end