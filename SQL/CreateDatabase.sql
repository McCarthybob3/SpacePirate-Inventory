USE master
GO
if exists (select * from sysdatabases where name='InventorySystem')
		drop database InventorySystem
go

CREATE DATABASE InventorySystem
GO