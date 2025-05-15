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

merge into dbo.Food as Target
using (values 
    ('Cheeseburger Meal', 'A cheeseburger, fries and a drink.', '15.95'),
    ('Taco Meal', 'Steak Taco, salad and a drink.', '18.95'),
    ('Salad Meal', 'A cesear salad and a water.', '14.95')
) as Source ([Title], [Description], [Price])
on Target.[Title] = Source.[Title]
when matched then
    update set 
        Target.[Description] = Source.[Description],
        Target.[Price] = Source.[Price]
when not matched then
    insert ([Title], [Description], [Price])
    values (Source.[Title], Source.[Description], Source.[Price]);