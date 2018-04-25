USE InventorySystemTestDb
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategorySelectAll')
		DROP PROCEDURE CategorySelectAll
GO

CREATE PROCEDURE CategorySelectAll AS
BEGIN
	SELECT CategoryName 
	FROM Category
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AuthorSelectAll')
		DROP PROCEDURE AuthorSelectAll
GO

CREATE PROCEDURE AuthorSelectAll AS
BEGIN
	SELECT Id
	FROM AspNetUsers
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CurrencySelectAll')
		DROP PROCEDURE CurrencySelectAll
GO

CREATE PROCEDURE CurrencySelectAll AS
BEGIN
	SELECT CurrencyName 
	FROM Currency
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ArticleDetailsSelect')
		DROP PROCEDURE ArticleDetailsSelect
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ArticleSelectLong')
		DROP PROCEDURE ArticleSelectLong
GO

CREATE PROCEDURE ArticleSelectLong(

@ArticleId int
 )AS
BEGIN
	SELECT A.ArticleTitle,A.DateAdded,A.ArticleContent, A.ArticleId, A.UserId, I.ItemName
	FROM Article A
	inner join ItemArticle IA on A.ArticleId = IA.ArticleId 
	inner join Item I on IA.ItemId = I.ItemId
	where A.ArticleId = @ArticleId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ArticleSelectAll')
		DROP PROCEDURE ArticleSelectAll
GO

CREATE PROCEDURE ArticleSelectAll AS
BEGIN
	SELECT A.ArticleTitle,A.DateAdded,A.ArticleContent, A.ArticleId, A.UserId, A.Approved, I.ItemName, ANU.UserName
	FROM Article A
	LEFT join ItemArticle IA on A.ArticleId = IA.ArticleId
	LEFT join Item I on IA.ItemId = I.ItemId
	LEFT join AspNetUsers ANU on A.UserId = ANU.Id 
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ArticleDelete')
		DROP PROCEDURE ArticleDelete
GO

CREATE PROCEDURE ArticleDelete ( 
@ArticleId int
)
AS 
BEGIN 
BEGIN TRANSACTION
DELETE FROM ItemArticle WHERE ArticleId = @ArticleId
DELETE FROM Article WHERE ArticleId = @ArticleId
COMMIT TRANSACTION


END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemDelete')
		DROP PROCEDURE ItemDelete
GO

CREATE PROCEDURE ItemDelete ( 
@ItemId int
)
AS 
BEGIN 
BEGIN TRANSACTION
DELETE FROM ItemCurrency WHERE ItemId = @ItemId
DELETE FROM ItemCategory WHERE ItemId = @ItemId
DELETE FROM ItemArticle WHERE ItemId = @ItemId
DELETE FROM Item WHERE ItemId = @ItemId
COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ArticleInsert')
		DROP PROCEDURE ArticleInsert
GO
     CREATE PROCEDURE ArticleInsert (
   @ArticleId int output,
   @ArticleTitle VARCHAR(50),
   @ArticleContent VARCHAR(1000),
   @DateAdded DateTime2,
   @UserId NVarChar(128),
   @Approved bit
   )
   AS 
   BEGIN
insert into Article(ArticleTitle, ArticleContent, DateAdded, UserId, Approved)
VALUES (@ArticleTitle, @ArticleContent, SYSDATETIME(), @UserId, @Approved)
Set @ArticleId = SCOPE_IDENTITY();

   END
   GO


   IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemInsert')
		DROP PROCEDURE ItemInsert
GO
   CREATE PROCEDURE ItemInsert (
   @ItemId int output,
   @Description varchar(500),
   @DisplayValue int,
   @Favorite bit,
   @Featured bit,
   @ItemName varchar(200),
   @ItemPictureURL varchar(50),
   @RealValue int,
   @CurrencyName varchar(30),
   @CategoryName varchar(200)
   )
   AS 
   BEGIN
insert into Item(DateAdded,Description,DisplayValue,Favorite,Featured,ItemName,ItemPictureURL,RealValue)
VALUES (SYSDATETIME(),@Description,@DisplayValue,@Favorite,@Featured,@ItemName,@ItemPictureURL,@RealValue)
Set @ItemId = SCOPE_IDENTITY();

insert into Currency(CurrencyName)
VALUES (@CurrencyName)

DECLARE @CurrencyId int;
Set @CurrencyId = SCOPE_IDENTITY();
insert into ItemCurrency(ItemId, CurrencyId)
VALUES(@ItemId, @CurrencyId)

INSERT INTO Category(CategoryName)
VALUES (@CategoryName)

DECLARE @CategoryId int;
Set @CategoryId = SCOPE_IDENTITY();
insert into ItemCategory(ItemId, CategoryId)
VALUES(@ItemId, @CategoryId)

   END
   GO



   IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategoryDelete')
		DROP PROCEDURE CategoryDelete
GO
   CREATE PROCEDURE CategoryDelete ( 
@CategoryId int
)
AS 
BEGIN 
BEGIN TRANSACTION
DELETE FROM ItemCategory WHERE CategoryId = @CategoryId
DELETE FROM Category WHERE CategoryId = @CategoryId
COMMIT TRANSACTION
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategoryInsert')
		DROP PROCEDURE CategoryInsert
GO
     CREATE PROCEDURE CategoryInsert (
   @CategoryId int output,
   @CategoryName VARCHAR(200)
 
   )
   AS 
   BEGIN
insert into Category(CategoryName)
VALUES (@CategoryName)
Set @CategoryId = SCOPE_IDENTITY();

   END
   GO


   
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ItemUpdate')
      DROP PROCEDURE ItemUpdate
GO

CREATE PROCEDURE ItemUpdate (
   @ItemId int,
   @Description varchar(500),
   @DisplayValue int,
   @Favorite bit,
   @Featured bit,
   @ItemName varchar(200),
   @ItemPictureURL varchar(50),
   @RealValue int,
   @CategoryName varchar(200),
   @CategoryId int,
   @CurrencyName varchar(30),
   @CurrencyId int
)
AS
Begin
   update Item
   set [Description]=@Description,
   DisplayValue=@DisplayValue,
   Featured=@Featured,
   Favorite=@Favorite,
   ItemName = @ItemName,
   ItemPictureURL = @ItemPictureURL,
   RealValue = @RealValue
   where ItemId=@ItemId
   update Category
   set CategoryName = @CategoryName
   where CategoryId = @CategoryId
   update Currency
   set CurrencyName = @CurrencyName
   where CurrencyId = @CurrencyId 
   end
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ArticleUpdate')
      DROP PROCEDURE ArticleUpdate
GO

CREATE PROCEDURE ArticleUpdate (
  @ArticleId int output,
   @ArticleTitle VARCHAR(50),
   @ArticleContent VARCHAR(1000),
   @UserId NVarchar(128)
)
AS
Begin
   update Article
   set ArticleTitle=@ArticleTitle,
   ArticleContent=@ArticleContent,
   UserId=@UserId
   where ArticleId=@ArticleId
   end
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategorySelectById')
		DROP PROCEDURE CategorySelectById
GO

CREATE PROCEDURE CategorySelectById(
@CategoryId int
) AS
BEGIN
	SELECT CategoryName
	FROM Category
	where CategoryId= @CategoryId
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectById')
		DROP PROCEDURE ItemSelectById
GO

CREATE PROCEDURE ItemSelectById(
@ItemId int
) AS
BEGIN
	SELECT DateAdded,Description,DisplayValue,Favorite,Featured,ItemName,ItemPictureURL,RealValue
	FROM Item
	where ItemId= @ItemId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectByIdDetail')
		DROP PROCEDURE ItemSelectByIdDetail
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectByIdLong')
		DROP PROCEDURE ItemSelectByIdLong
GO

CREATE PROCEDURE ItemSelectByIdLong(
@ItemId int
) AS
BEGIN

CREATE TABLE #Temp(
	ItemID int,
	CategoryName varchar(200)
)
INSERT INTO #Temp
SELECT i.ItemId, c.CategoryName
FROM Item i
	JOIN ItemCategory ic
		ON ic.ItemId = i.ItemId
	JOIN Category c
		ON c.CategoryId = ic.CategoryId

CREATE TABLE #CategoryString
(
	ItemID int,
	CategoryName varchar(200)
)
INSERT INTO #CategoryString

Select Main.ItemID,
		LEFT(Main.Categories,LEN(Main.Categories)-1) As "Categories"
FROM(
		Select distinct T2.ItemId,
		(
			Select T1.CategoryName + ', ' AS [text()]
			FROM #Temp T1
			WHERE T1.ItemId = T2.ItemID
			ORDER BY T1.ItemId
			FOR XML PATH ('')
		)[Categories]
		FROM #Temp T2
)[Main]


	SELECT I.ItemId, I.DateAdded,[Description],DisplayValue,Favorite,Featured,ItemName,ItemPictureURL,RealValue, #CategoryString.CategoryName, A.ArticleId, Cu.CurrencyId, Cu.CurrencyName

	FROM Item I
	inner join ItemCategory ICa on I.ItemId = ICa.ItemId
	inner join ItemArticle IA on I.ItemId = IA.ItemId
	inner join Category Ca on ICa.CategoryId = CA.CategoryId
	inner join Article A on IA.ArticleId = A.ArticleId
	inner join ItemCurrency ICu on I.ItemId = ICu.ItemId
	inner join Currency Cu on ICu.CurrencyId = Cu.CurrencyId
	inner join #CategoryString on I.ItemId = #CategoryString.ItemID

	where I.ItemId= @ItemId
END
GO

--EXEC ItemSelectByIdLong @ItemId = 3

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectForSearch')
		DROP PROCEDURE ItemSelectForSearch
GO

CREATE PROCEDURE ItemSelectForSearch(
@MinValue decimal,
@MaxValue decimal,
@ItemName varchar(50),
@CategoryName varchar(50)

) AS
BEGIN
	SELECT DISTINCT I.ItemId, I.DateAdded,[Description],DisplayValue,Favorite,Featured,ItemName,ItemPictureURL,RealValue, Ca.CategoryName
	FROM Item I
	inner join ItemCategory ICa on I.ItemId = ICa.ItemId
	inner join ItemArticle IA on I.ItemId = IA.ItemId
	inner join Category Ca on ICa.CategoryId = CA.CategoryId
	WHERE 1 = 1
	AND (@MinValue IS NULL OR (@MinValue <= DisplayValue))
	AND (@MaxValue IS NULL OR (@MaxValue >= DisplayValue))
	AND (@ItemName IS NULL OR (ItemName LIKE '%' + @ItemName + '%'))
	AND (@CategoryName IS NULL OR (CategoryName LIKE '%' + @CategoryName + '%'))
ORDER BY DisplayValue DESC
END
GO

--IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
--	WHERE ROUTINE_NAME = 'AuthorSelectById')
--		DROP PROCEDURE AuthorSelectById
--GO

--CREATE PROCEDURE AuthorSelectById(
--@AuthorId int
--) AS
--BEGIN
--	SELECT AuthorName
--	FROM Author
--	where AuthorId= @AuthorId
--END
--GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ArticleSelectById')
		DROP PROCEDURE ArticleSelectById
GO

CREATE PROCEDURE ArticleSelectById(
@ArticleId int
) AS
BEGIN
	SELECT A.ArticleTitle,A.DateAdded,A.ArticleContent, A.ArticleId, A.UserId, A.Approved, I.ItemName, ANU.UserName
	FROM Article A
	inner join ItemArticle IA on A.ArticleId = IA.ArticleId
	inner join Item I on IA.ItemId = I.ItemId
	inner join AspNetUsers ANU on A.UserId = ANU.Id 
	where A.ArticleId = @ArticleId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectFeautredShortAll')
		DROP PROCEDURE ItemSelectFeautredShortAll
GO

CREATE PROCEDURE ItemSelectFeautredShortAll AS
BEGIN
	SELECT top 9 ItemId, ItemName, ItemPictureURL, DisplayValue
	FROM Item
	Where Featured = 1
	ORDER BY DisplayValue DESC
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectShortById')
		DROP PROCEDURE ItemSelectShortById
GO

CREATE PROCEDURE ItemSelectShortById(
@ItemId int
) AS
BEGIN
	SELECT ItemId, ItemName, ItemPictureURL, DisplayValue
	FROM Item
where ItemId = @ItemId
ORDER BY DisplayValue DESC
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ArticleSelectShort')
		DROP PROCEDURE ArticleSelectShort
GO

CREATE PROCEDURE ArticleSelectShort
AS
BEGIN
	SELECT A.ArticleTitle,A.ArticleContent, A.ArticleId
	FROM Article A
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectShortAll')
		DROP PROCEDURE ItemSelectShortAll
GO

CREATE PROCEDURE ItemSelectShortAll AS
BEGIN
	SELECT ItemId, ItemName, ItemPictureURL, DisplayValue
	FROM Item
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UserListSelect')
		DROP PROCEDURE UserListSelect
GO

CREATE PROCEDURE UserListSelect AS
BEGIN
	SELECT ANU.UserName, ANU.Id, ANU.Email, ANR.Id, ANR.Name
	FROM AspNetUsers ANU
	Inner join AspNetUserRoles ANUR on ANU.Id = ANUR.UserId
	inner join AspNetRoles ANR on ANUR.RoleId = ANR.Id
END
GO

--exec UserListSelect

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ItemSelectAll')
		DROP PROCEDURE ItemselectAll
GO

CREATE PROCEDURE ItemSelectAll AS
BEGIN
	SELECT I.ItemId, I.ItemName,I.DisplayValue, I.RealValue, I.DisplayValue, CA.CategoryName, CU.CurrencyName
	FROM Item I
	left join ItemCategory ICa on I.ItemId = ICa.ItemId
	left join Category CA on ICa.CategoryId = CA.CategoryId
	left join ItemCurrency ICu on I.ItemId = ICu.ItemId
	left join Currency CU on ICu.CurrencyId = CU.CurrencyId
END
GO