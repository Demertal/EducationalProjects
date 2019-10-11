/*
Выбирает из таблицы ТОВАРЫ информацию о товарах, единицей измерения которых является «шт» (штуки) и цена закупки составляет меньше 200 руб.
*/
CREATE VIEW Query1 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice, 
Products.SalesPrice as SalesPrice, UnitStorages.Title as UnitStorage
FROM Products INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id
WHERE Products.PurchasePrice <= 200 and UnitStorages.Title = 'шт'
GO

/*
Выбирает из таблицы ТОВАРЫ информацию о товарах, цена закупки которых находится в диапазоне больше 500 руб. за единицу товара
*/
CREATE VIEW Query2 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice, 
Products.SalesPrice as SalesPrice, UnitStorages.Title as UnitStorage
FROM Products INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id
WHERE Products.PurchasePrice >= 500
GO

/*
Выбирает из таблицы ТОВАРЫ информацию о товарах с заданным наименованием (например, «сахар»), для которых цена закупки меньше 1000 руб.
*/
CREATE VIEW Query3 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice, 
Products.SalesPrice as SalesPrice, UnitStorages.Title as UnitStorage
FROM Products INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id
WHERE Products.PurchasePrice <= 1000 and Products.Title = 'Наушники'
GO

/*
Выбирает из таблицы ПРОДАВЦЫ информацию о продавцах с заданным значением процента комиссионных.
Конкретное значение процента комиссионных вводится при выполнении запроса
*/
CREATE PROCEDURE Query4
    @commissions FLOAT
AS
SELECT * FROM Sellers WHERE Sellers.Сommissions = @commissions
GO

/*
Выбирает из таблиц ТОВАРЫ, ПРОДАВЦЫ и ПРОДАЖИ информацию обо всех зафиксированных фактах продажи товаров
 (Наименование товара, Цена закупки, Цена продажи, дата продажи), для которых Цена продажи оказалась в некоторых заданных границах.
  Нижняя и верхняя границы интервала цены продажи задаются при выполнении запроса
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
Вычисляет прибыль от продажи за каждый проданный товар.
 Включает поля Дата продажи, Наименование товара, Цена закупки, Цена продажи, Количество проданных единиц, Прибыль.
  Сортировка по полю Наименование товара
*/
CREATE PROCEDURE Query6 AS
SELECT Products.Title as Title, Products.PurchasePrice as PurchasePrice,
Products.SalesPrice as SalesPrice, Sales.DateSale as DateSale, Sales.Count as Count,
(Products.SalesPrice - Products.PurchasePrice)*Sales.Count as profit
FROM Sales INNER JOIN Products ON Sales.IdProduct = Products.Id
ORDER BY Title
GO

--Выполняет группировку по полю Наименование товара. Для каждого наименования вычисляет среднюю цену закупки товара
CREATE VIEW Query7 AS
SELECT Products.Title as Title, AVG(Products.PurchasePrice) as PurchasePrice
FROM Products
GROUP BY Title
GO

--Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ.
-- Для каждого продавца вычисляет среднее значение по полю Цена продажи единицы товара
CREATE VIEW Query8 AS
SELECT CONCAT(Sellers.Surname, ' ', Sellers.Name, ' ',Sellers.Patronymic) as FullName, AVG(Products.SalesPrice) as SalesPrice
FROM Sales INNER JOIN Products ON Sales.IdProduct = Products.Id
 INNER JOIN Sellers ON Sales.IdSeller = Sellers.Id
GROUP BY Sellers.Id, Sellers.Surname, Sellers.Name, Sellers.Patronymic
GO

--Создает таблицу ЕДИНИЦЫ_ИЗМЕРЕНИЯ, содержащую информацию о товарах, единицей измерения которых является «шт» (штуки)
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
	 INNER JOIN UnitStorages ON Products.IdUnitStorage = UnitStorages.Id  WHERE UnitStorages.Title = 'шт'
GO

--Создает копию таблицы ТОВАРЫ с именем КОПИЯ_ТОВАРЫ
CREATE PROCEDURE Query10 AS
DROP TABLE IF EXISTS ProductsCopy
SELECT * INTO ProductsCopy FROM Products
GO

--Удаляет из таблицы КОПИЯ_ТОВАРЫ записи, в которых значение в поле Цена закупки единицы товара больше 500 руб.
CREATE PROCEDURE Query11 AS
DELETE ProductsCopy WHERE PurchasePrice >= 500
GO

--Устанавливает значение в поле Процент комиссионных таблицы ПРОДАВЦЫ равным 10 % для тех продавцов, процент комиссионных которых больше 30 %
CREATE PROCEDURE Query12 AS
UPDATE Sellers SET Сommissions = 10 WHERE Сommissions >= 30
GO

select * from Sellers