--DbResetTestDb
USE InventorySystemTestDb;
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'DbReset'
)
BEGIN
    DROP PROCEDURE DbReset
END
GO

CREATE PROCEDURE DbReset AS
BEGIN

	DELETE FROM ItemCategory;
	DELETE FROM ItemCurrency;
	DELETE FROM ItemArticle;
	DELETE FROM Item;
	DELETE FROM Category;
	DELETE FROM Currency;
	DELETE FROM Article;
	DELETE FROM AspNetUsers WHERE id IN ('00000000-0000-0000-0000-000000000000', '00000000-1111-1111-0000-000000000000');
	--DELETE FROM Author;


DBCC CHECKIDENT ('Item', RESEED, 1)
DBCC CHECKIDENT ('Category', RESEED, 1)
DBCC CHECKIDENT ('Article', RESEED, 1)

INSERT INTO AspNetUsers (Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES ('00000000-0000-0000-0000-000000000000', 0, 0, 'test@test.com', 0, 0, 0, 'test'),
	('00000000-1111-1111-0000-000000000000', 0, 0, 'test2@test.com', 0, 0, 0, 'testtwo')

SET IDENTITY_INSERT Category ON;
INSERT INTO Category (CategoryId, CategoryName)
VALUES (1, 'Treasure'),
(2, 'Planet'),
(3, 'Ship'),
(4, 'Mineral'),
(5, 'Animal')
SET IDENTITY_INSERT Category OFF;

SET IDENTITY_INSERT Currency ON;
INSERT INTO Currency (CurrencyId, CurrencyName)
VALUES (1, 'World Dollars'),
(2, 'Space Bucks')
SET IDENTITY_INSERT Currency OFF;


SET IDENTITY_INSERT Item ON;
INSERT INTO Item (ItemId, ItemName, ItemPictureURL, RealValue, DisplayValue, [Description], Favorite, Featured, DateAdded)
VALUES (1, 'Space Gold', 'spacegold.png', 100, 1000, 'Rare gold found in space', 1, 1, '1900-01-01'),
(2, 'Treasure Island', 'TreasureIsland.png', 100000, 1000000, 'A planet covered in spoils', 1, 1, '2018-02-05'),
(3, 'Hot Space Pie', 'spacepie.png', 100, 10000, 'A pie covered in spoils', 0, 1, '2018-06-05'),
(4, 'Alien Cow', 'spacecow.png', 500, 15000, 'A cow alien', 0, 1, '2012-06-05'),
(5, 'Blaster Gun', 'blastergun.png', 1200, 12200, 'Large fun gun', 1, 1, '3021-06-05'),
(6, 'Big Chunk of Cheese', 'bigcheese.png', 10, 1000000, 'A cheese chunk that is big I guess', 0, 1, '2778-06-05'),
(7, 'Declaration of Space Independence', 'spacedeclaration.png', 100000, 10000000, 'Cpt. Cage stole this for us', 0, 1, '7718-06-05'),
(8, 'Eyeballs', 'eyeballs.png', 100, 10000, 'A eye covered in boils', 0, 1, '2018-06-05')
SET IDENTITY_INSERT Item OFF;

INSERT INTO ItemCurrency (ItemId, CurrencyId)
VALUES (1, 2),
(2, 1),
(3, 1),
(4, 2),
(5, 1),
(6, 2),
(7, 1),
(8, 2)

INSERT INTO ItemCategory (ItemId, CategoryId)
VALUES (1,1),
(1, 3),
(2, 2),
(3, 1),
(4, 3),
(5, 4),
(6, 5),
(7, 3),
(8, 2)

--The AuthorId needs to bee replaced with a UserId, but the UserId values need to be added

SET IDENTITY_INSERT Article ON;
INSERT INTO Article (ArticleId, ArticleTitle, ArticleContent, DateAdded, UserId, Approved)
VALUES (1, 'Space and you', 'How to survive the vaccuum of space', '2000-03-28', '00000000-0000-0000-0000-000000000000', 0),
(2, 'Space Gluten and other allergies', 'Is space gluten really ten times more dangerous?', '2001-03-25', '00000000-0000-0000-0000-000000000000', 0),
(3, 'SPACE MEMES', 'AYYYYY LMAO', '1940-11-05', '00000000-0000-0000-0000-000000000000', 1),
(4, 'Space Art', 'Beautiful pictures of space', '2003-04-15', '00000000-0000-0000-0000-000000000000', 1)
SET IDENTITY_INSERT Article OFF;

--SET IDENTITY_INSERT Author ON;
--INSERT INTO Author (AuthorId, AspNetUserId, AuthorName)
--VALUES (1, 1, 'Cole Smith'),
--(2, 2, 'Mary Hawley'),
--(3, 3, 'Bob McCarthy'),
--(4, 4, 'Kyle Jakoby')
--SET IDENTITY_INSERT Author OFF;

INSERT INTO ItemArticle (ItemId, ArticleId)
VALUES (1,2),
(1, 3),
(2, 1),
(3, 1),
(4, 2),
(5, 4),
(6, 3),
(7, 2),
(8, 2)

END

--These are for highlighting and manually running
--exec DbReset
--Select * from Item INNER JOIN ItemCategory ON Item.ItemId = ItemCategory.ItemId INNER JOIN Category ON ItemCategory.CategoryId = Category.CategoryId
--Select * from Article INNER JOIN Author ON Article.AuthorId = Author.AuthorId
