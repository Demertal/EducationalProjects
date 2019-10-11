/*
�������� �� ������� ������ ���������� � �������, �������� ��������� ������� �������� ��� (�����) � ���� ������� ���������� ������ 200 ���.
*/
CREATE VIEW Query1 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice, 
Products.SalesPrice as SalesPrice, UnitStorages.Title as UnitStorage
FROM Products INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id
WHERE Products.PurchasePrice <= 200 and UnitStorages.Title = '��'
GO

/*
�������� �� ������� ������ ���������� � �������, ���� ������� ������� ��������� � ��������� ������ 500 ���. �� ������� ������
*/
CREATE VIEW Query2 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice, 
Products.SalesPrice as SalesPrice, UnitStorages.Title as UnitStorage
FROM Products INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id
WHERE Products.PurchasePrice >= 500
GO

/*
�������� �� ������� ������ ���������� � ������� � �������� ������������� (��������, ������), ��� ������� ���� ������� ������ 1000 ���.
*/
CREATE VIEW Query3 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice, 
Products.SalesPrice as SalesPrice, UnitStorages.Title as UnitStorage
FROM Products INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id
WHERE Products.PurchasePrice <= 1000 and Products.Title = '��������'
GO

/*
�������� �� ������� �������� ���������� � ��������� � �������� ��������� �������� ������������.
���������� �������� �������� ������������ �������� ��� ���������� �������
*/
CREATE PROCEDURE Query4
    @commissions FLOAT
AS
SELECT * FROM Sellers WHERE Sellers.�ommissions = @commissions
GO

/*
�������� �� ������ ������, �������� � ������� ���������� ��� ���� ��������������� ������ ������� �������
 (������������ ������, ���� �������, ���� �������, ���� �������), ��� ������� ���� ������� ��������� � ��������� �������� ��������.
  ������ � ������� ������� ��������� ���� ������� �������� ��� ���������� �������
*/
CREATE PROCEDURE Query5
    @lowSalesPrice MONEY,
	@upSalesPrice MONEY
AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice,
Products.SalesPrice, Sales.DateSale
 FROM Sales INNER JOIN Products ON Sales.IdProduct = Products.Id
  WHERE Products.SalesPrice BETWEEN @lowSalesPrice AND @upSalesPrice;
GO

/*
��������� ������� �� ������� �� ������ ��������� �����.
 �������� ���� ���� �������, ������������ ������, ���� �������, ���� �������, ���������� ��������� ������, �������.
  ���������� �� ���� ������������ ������
*/
CREATE PROCEDURE Query6 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice,
Products.SalesPrice as SalesPrice, Sales.DateSale as DateSale, Sales.Count as Count,
(Products.SalesPrice - Products.PurchasePrice)*Sales.Count as profit
FROM Sales INNER JOIN Products ON Sales.IdProduct = Products.Id
ORDER BY Title
GO

--��������� ����������� �� ���� ������������ ������. ��� ������� ������������ ��������� ������� ���� ������� ������
CREATE VIEW Query7 AS
SELECT Products.Title as Title, AVG(Products.PurchasePrice) as PurchasePrice
FROM Products
GROUP BY Title
GO

--��������� ����������� �� ���� ��� �������� �� ������� �������.
-- ��� ������� �������� ��������� ������� �������� �� ���� ���� ������� ������� ������
CREATE VIEW Query8 AS
SELECT CONCAT(Sellers.Surname, ' ', Sellers.Name, ' ',Sellers.Patronymic) as FullName, AVG(Products.SalesPrice) as SalesPrice
FROM Sales INNER JOIN Products ON Sales.IdProduct = Products.Id
 INNER JOIN Sellers ON Sales.IdSeller = Sellers.Id
GROUP BY Sellers.Id, Sellers.Surname, Sellers.Name, Sellers.Patronymic
GO

--������� ������� �������_���������, ���������� ���������� � �������, �������� ��������� ������� �������� ��� (�����)
CREATE PROCEDURE Query9 AS		
	DROP TABLE IF EXISTS Query9Table
	CREATE TABLE Query9Table(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(120) NOT NULL CHECK(Title !=''),
	PurchasePrice MONEY NOT NULL DEFAULT 0 CHECK(PurchasePrice > 0),
	SalesPrice MONEY NOT NULL DEFAULT 0 CHECK(SalesPrice > 0),	
	IdUnitStorage INT NOT NULL,
	FOREIGN KEY (IdUnitStorage) REFERENCES UnitStorages (Id))
	INSERT INTO Query9Table(Title, PurchasePrice, SalesPrice, IdUnitStorage) 
	SELECT  Products.Title, Products.PurchasePrice, Products.SalesPrice, Products.IdUnitStorage FROM Products
	 INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id  WHERE UnitStorages.Title = '��'
GO

--������� ����� ������� ������ � ������ �����_������
CREATE PROCEDURE Query10 AS
DROP TABLE IF EXISTS ProductsCopy
SELECT * INTO ProductsCopy FROM Products
GO

--������� �� ������� �����_������ ������, � ������� �������� � ���� ���� ������� ������� ������ ������ 500 ���.
CREATE PROCEDURE Query11 AS
DELETE ProductsCopy WHERE PurchasePrice >= 500
GO

--������������� �������� � ���� ������� ������������ ������� �������� ������ 10 % ��� ��� ���������, ������� ������������ ������� ������ 30 %
CREATE PROCEDURE Query12 AS
UPDATE Sellers SET �ommissions = 10 WHERE �ommissions >= 30
GO

select * from Sellers