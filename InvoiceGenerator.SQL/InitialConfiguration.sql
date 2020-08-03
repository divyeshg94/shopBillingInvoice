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

--Settings Table

Insert into Settings With (ROWLOCK) SELECT 'FromEmail', 'crescentbeautylounge@gmail.com', 'EmailSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'FromEmail')

Insert into Settings With (ROWLOCK) SELECT 'FromUserName', 'crescentbeautylounge@gmail.com', 'EmailSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'FromUserName')

Insert into Settings With (ROWLOCK) SELECT 'FromPassword', 'Dhakshu020415', 'EmailSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'FromPassword')

Insert into Settings With (ROWLOCK) SELECT 'InvoiceSubject', 'Invoice', 'InvoiceSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'InvoiceSubject')

Insert into Settings With (ROWLOCK) SELECT 'IsInvoiceSendInEmail', 'True', 'InvoiceSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'IsInvoiceSendInEmail')

Insert into Settings With (ROWLOCK) SELECT 'IsInvoiceSendInSms', 'True', 'SmsSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'IsInvoiceSendInSms')

Insert into Settings With (ROWLOCK) SELECT 'TwilioAccountSid', '*', 'SmsSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'TwilioAccountSid')

Insert into Settings With (ROWLOCK) SELECT 'TwilioAuthToken', '*', 'SmsSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'TwilioAuthToken')

Insert into Settings With (ROWLOCK) SELECT 'TwilioPhoneNumber', '*', 'SmsSettings', GETUTCDATE(), GETUTCDATE()
WHERE not exists (select * from Settings where [Key] = 'TwilioPhoneNumber')

--Items Table

--Threading
INSERT INTO Items  With (ROWLOCK) SELECT 'Eyebrows', 'Threading', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Eyebrows' and [Category] = 'Threading' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'UpperLips', 'Threading', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'UpperLips' and [Category] = 'Threading' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Chin', 'Threading', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Chin' and [Category] = 'Threading' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Forehead', 'Threading', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Forehead' and [Category] = 'Threading' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Full Face', 'Threading', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Full Face' and [Category] = 'Threading' and [Type] = 10)

--Wax
INSERT INTO Items  With (ROWLOCK) SELECT 'Half Arms', 'Wax', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Half Arms' and [Category] = 'Wax' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Full Arms', 'Wax', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Full Arms' and [Category] = 'Wax' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Half legs', 'Wax', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Half legs' and [Category] = 'Wax' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Full legs', 'Wax', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Full legs' and [Category] = 'Wax' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Front', 'Wax', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Front' and [Category] = 'Wax' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Back', 'Wax', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Back' and [Category] = 'Wax' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Full body', 'Wax', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Full body' and [Category] = 'Wax' and [Type] = 10)

--Pedicure & Manicure
INSERT INTO Items  With (ROWLOCK) SELECT 'Nail Makeover', 'Pedicure & Manicure', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Nail Makeover' and [Category] = 'Pedicure & Manicure' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Classic', 'Pedicure & Manicure', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Classic' and [Category] = 'Pedicure & Manicure' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Crystal', 'Pedicure & Manicure', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Crystal' and [Category] = 'Pedicure & Manicure' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Ice Cream', 'Pedicure & Manicure', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Ice Cream' and [Category] = 'Pedicure & Manicure' and [Type] = 10)

--Bleach
INSERT INTO Items  With (ROWLOCK) SELECT 'Lacto Bleach', 'Bleach', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Lacto Bleach' and [Category] = 'Bleach' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Oxy Bleach', 'Bleach', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Oxy Bleach' and [Category] = 'Bleach' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Milk Pack', 'Bleach', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Milk Pack' and [Category] = 'Bleach' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Detan Pack', 'Bleach', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Detan Pack' and [Category] = 'Bleach' and [Type] = 10)

--Facial
INSERT INTO Items  With (ROWLOCK) SELECT 'Classic Facial', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Classic Facial' and [Category] = 'Facial' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Fruit Facial', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Fruit Facial' and [Category] = 'Facial' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Skin - Lightening Facial', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Skin - Lightening Facial' and [Category] = 'Facial' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Golden Facial', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Golden Facial' and [Category] = 'Facial' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Acne Treatment', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Acne Treatment' and [Category] = 'Facial' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Under eye Treatment', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Under eye Treatment' and [Category] = 'Facial' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Anti - Ageing Facial', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Anti - Ageing Facial' and [Category] = 'Facial' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Herbal Facial', 'Facial', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Herbal Facial' and [Category] = 'Facial' and [Type] = 10)

--Hair Treatment
INSERT INTO Items  With (ROWLOCK) SELECT 'Head Massage', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Head Massage' and [Category] = 'Hair Treatments' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Anti - Hair Fall Treatment', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Anti - Hair Fall Treatment' and [Category] = 'Hair Treatments' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Anti - Dandruff Treatment', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Anti - Dandruff Treatment' and [Category] = 'Hair Treatments' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Keratin Treatment', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Keratin Treatment' and [Category] = 'Hair Treatments' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Straightening / Smoothening', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Straightening / Smoothening' and [Category] = 'Hair Treatments' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Henna Pack', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Henna Pack' and [Category] = 'Hair Treatments' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Hair Wash & Conditioning', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Hair Wash & Conditioning' and [Category] = 'Hair Treatments' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Protein Treatment', 'Hair Treatments', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Protein Treatment' and [Category] = 'Hair Treatments' and [Type] = 10)

--Hair Spa
INSERT INTO Items  With (ROWLOCK) SELECT 'Deep Conditioning', 'Hair SPA', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Deep Conditioning' and [Category] = 'Hair SPA' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Dandruff Clear', 'Hair SPA', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Dandruff Clear' and [Category] = 'Hair SPA' and [Type] = 10)

--Hair Cuts
INSERT INTO Items  With (ROWLOCK) SELECT 'Straight Cut', 'Hair Cuts', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Straight Cut' and [Category] = 'Hair Cuts' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'U Cut', 'Hair Cuts', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'U Cut' and [Category] = 'Hair Cuts' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'V Cut', 'Hair Cuts', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'V Cut' and [Category] = 'Hair Cuts' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Layer or Step Cut', 'Hair Cuts', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Layer or Step Cut' and [Category] = 'Hair Cuts' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Feather Cut', 'Hair Cuts', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Feather Cut' and [Category] = 'Hair Cuts' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Kids Cut', 'Hair Cuts', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Kids Cut' and [Category] = 'Hair Cuts' and [Type] = 10)

--Hair Styling
INSERT INTO Items  With (ROWLOCK) SELECT 'Blow dry Straight', 'Hair Styling', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Blow dry Straight' and [Category] = 'Hair Styling' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Blow dry Curls', 'Hair Styling', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Blow dry Curls' and [Category] = 'Hair Styling' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Ironing', 'Hair Styling', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Ironing' and [Category] = 'Hair Styling' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Tongs', 'Hair Styling', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Tongs' and [Category] = 'Hair Styling' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Crimping', 'Hair Styling', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Crimping' and [Category] = 'Hair Styling' and [Type] = 10)

--MakeOvers
INSERT INTO Items  With (ROWLOCK) SELECT 'Classic', 'Make overs', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Classic' and [Category] = 'Make overs' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Krylon', 'Make overs', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Krylon' and [Category] = 'Make overs' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Guest Makeover', 'Make overs', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Guest Makeover' and [Category] = 'Make overs' and [Type] = 10)

INSERT INTO Items  With (ROWLOCK) SELECT 'Hair Styling', 'Make overs', '', 10, 0, 1, 1
WHERE not exists (select * from Items where [Name] = 'Hair Styling' and [Category] = 'Make overs' and [Type] = 10)