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


IF NOT EXISTS (SELECT * FROM DBO.Food)
BEGIN
    INSERT INTO DBO.Food([Title],[Description],[Price])
    VALUES 
    ( 'Cheeseburger meal','A cheeseburger, fries and a drink', 5.95),
    ( 'Chilli Dog Meal','Two chilli dogs, fries and a drink', 4.15),
    ( 'Vegan meal','Large salad and water', 1.95);
END