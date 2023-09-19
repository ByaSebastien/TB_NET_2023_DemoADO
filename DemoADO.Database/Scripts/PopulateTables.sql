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
ALTER TABLE [dbo].[Pokemon] NOCHECK CONSTRAINT ALL

INSERT INTO [dbo].[Pokemon](Id, [Name], Height, [Weight], Type1Id, Type2Id)
VALUES 
(1, 'Bulbizarre', 70, 6.9, 3, null),
(42, 'Nosferalto', 160, 55, 6, 7),
(25, 'Pikachu', 40, 6, 5, null);

ALTER TABLE [dbo].[Pokemon] CHECK CONSTRAINT ALL

SET IDENTITY_INSERT [dbo].[Type] ON;

INSERT INTO [dbo].[Type] ([Id], [Name]) VALUES
(1, 'Normal'),
(2, 'Feu'),
(3, 'Plante'),
(4, 'Eau'),
(5, 'Eletrik'),
(6, 'Poison'),
(7, 'Vol')
;

-- réactive l'autoincrémentation
SET IDENTITY_INSERT [dbo].[Type] OFF;