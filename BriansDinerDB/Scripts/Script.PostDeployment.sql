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
    ('Classic Cheeseburger Meal', 'A juicy cheeseburger with lettuce, tomato, fries and a drink.', '15.95')
    ,('Steak Taco Meal', 'Grilled steak tacos with pico de gallo, rice, beans and a drink.', '18.95')
    ,('Caesar Salad Meal', 'Crisp romaine with parmesan, croutons, caesar dressing and a water.', '14.95')
    ,('Grilled Chicken Sandwich Meal', 'Herb-marinated grilled chicken breast with lettuce, tomato, fries and a drink.', '16.49')
    ,('BBQ Pulled Pork Meal', 'Slow-smoked pulled pork with coleslaw, baked beans and a drink.', '17.95')
    ,('Fish and Chips Meal', 'Beer-battered cod with seasoned fries, tartar sauce and a drink.', '19.49')
    ,('Veggie Burger Meal', 'Black bean and quinoa patty with avocado, sweet potato fries and a drink.', '15.49')
    ,('Chicken Tenders Meal', 'Crispy chicken tenders with honey mustard, fries and a drink.', '14.49')
    ,('Philly Cheesesteak Meal', 'Shaved ribeye with peppers, onions, provolone on a hoagie with fries and a drink.', '18.49')
    ,('Margherita Flatbread Meal', 'Fresh mozzarella, basil, tomato sauce on flatbread with a side salad and a drink.', '16.95')
    ,('Grilled Salmon Meal', 'Atlantic salmon fillet with steamed vegetables, rice pilaf and a drink.', '22.95')
    ,('Chicken Alfredo Meal', 'Fettuccine in creamy alfredo sauce with grilled chicken, garlic bread and a drink.', '17.49')
    ,('Southwest Burrito Bowl', 'Seasoned ground beef, cilantro-lime rice, black beans, guacamole and a drink.', '16.49')
    ,('Turkey Club Meal', 'Roasted turkey, bacon, lettuce, tomato on sourdough with chips and a drink.', '15.95')
    ,('Shrimp Po Boy Meal', 'Fried shrimp with remoulade, shredded lettuce on French bread with fries and a drink.', '19.95')
    ,('Teriyaki Chicken Bowl', 'Grilled teriyaki chicken over steamed rice with stir-fried vegetables and a drink.', '16.95')
    ,('Meatball Sub Meal', 'Italian meatballs in marinara with melted mozzarella on a sub roll with fries and a drink.', '15.49')
    ,('Cobb Salad Meal', 'Grilled chicken, bacon, avocado, egg, blue cheese over mixed greens and a water.', '16.49')
    ,('Bacon Cheeseburger Meal', 'Double patty with bacon, cheddar, pickles, onion rings and a drink.', '18.95')
    ,('Chicken Quesadilla Meal', 'Grilled chicken and cheese quesadilla with salsa, sour cream, rice and a drink.', '14.95')
) as Source ([Title], [Description], [Price])
on Target.[Title] = Source.[Title]
when matched then
    update set 
        Target.[Description] = Source.[Description]
        ,Target.[Price] = Source.[Price]
when not matched then
    insert ([Title], [Description], [Price])
    values (Source.[Title], Source.[Description], Source.[Price])
when not matched by source then
    delete;