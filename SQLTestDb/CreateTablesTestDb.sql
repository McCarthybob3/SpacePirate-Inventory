--CreateTablesTestDb
USE InventorySystemTestDb
Go

IF EXISTS(SELECT * FROM sys.tables WHERE name='ItemArticle')
	DROP TABLE ItemArticle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ItemCategory')
	DROP TABLE ItemCategory
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ItemCurrency')
	DROP TABLE ItemCurrency
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Item')
	DROP TABLE Item
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Currency')
	DROP TABLE Currency
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Category')
	DROP TABLE Category
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Article')
	DROP TABLE Article
GO

--IF EXISTS(SELECT * FROM sys.tables WHERE name='Author')
--	DROP TABLE Author
--GO

CREATE TABLE [Category] (
	CategoryId INT primary key identity(1,1),
	CategoryName varchar(200) NOT NULL
)
GO

CREATE TABLE Item (
	ItemId INT primary key identity(1,1),
	ItemName varchar(200) NOT NULL,
	ItemPictureURL varchar(50) NOT NULL,
	RealValue INT NOT NULL,
	DisplayValue INT NOT NULL,
	[Description] varchar(500) NULL,
	Favorite bit NOT NULL,
	Featured bit NOT NULL,
	DateAdded datetime2 NOT NULL
	--CurrencyId INT NOT NULL
)
GO

CREATE TABLE Currency(
	CurrencyId INT Primary key identity (1,1),
	CurrencyName varchar(30)
)

CREATE TABLE ItemCategory (
	ItemId INT,
	CategoryId INT,
	constraint PK_ItemCategory primary key (ItemId, CategoryId),
	constraint FK_ItemId
		foreign key (ItemId) references Item (ItemId),
	constraint FK_CategoryId
		foreign key (CategoryId) references Category (CategoryId)
)
GO

CREATE TABLE ItemCurrency (
	ItemId INT,
	CurrencyId INT,
	constraint PK_ItemCurrency primary key (ItemId, CurrencyID),
	constraint FK_ItemId2
		foreign key (ItemId) references Item (ItemId),
	constraint FK_CurrencyId
		foreign key (CurrencyId) references Currency (CurrencyId)
)

CREATE TABLE Article (
	ArticleId INT primary key identity(1,1),
	ArticleTitle varchar(50) NOT NULL,
	ArticleContent varchar(1000) NOT NULL,
	DateAdded datetime2 NOT NULL,
	UserId NVARCHAR(128) NOT NULL,
	Approved Bit Null
)
GO

--CREATE TABLE Author (
--	AuthorId INT primary key identity(1,1),
--	AspNetUserId int NOT NULL,
--	AuthorName varchar(50) NOT NULL
--)
--GO

CREATE TABLE ItemArticle (
	ItemId INT,
	ArticleId INT,
	constraint PK_ItemArticle primary key (ItemId, ArticleId),
	constraint FK_ItemId3
		foreign key (ItemId) references Item (ItemId),
	constraint FK_ArticleId
		foreign key (ArticleId) references Article (ArticleId)
)
GO