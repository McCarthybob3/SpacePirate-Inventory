USE InventorySystem
GO

CREATE LOGIN Captian WITH PASSWORD='space'
GO

USE InventorySystem
GO

CREATE USER Captian FOR LOGIN Captian
GO

--GRANT EXECUTE ON Article TO Captian
--GRANT EXECUTE ON Author TO Captian
--GRANT EXECUTE ON Category TO Captian
--GRANT EXECUTE ON Currency TO Captian
--GRANT EXECUTE ON Item TO Captian
--GRANT EXECUTE ON ItemArticle TO Captian
--GRANT EXECUTE ON ItemCategory TO Captian
--GRANT EXECUTE ON ItemCurrency TO Captian

ALTER ROLE Captian ADD MEMBER Captian
go