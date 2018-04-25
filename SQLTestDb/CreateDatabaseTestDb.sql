USE master
GO
if exists (select * from sysdatabases where name='InventorySystemTestDb')
		drop database InventorySystemTestDb
go

CREATE DATABASE InventorySystemTestDb
GO