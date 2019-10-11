--USE [master]
--GO

--DROP DATABASE IF EXISTS [WholesaleStore]
--GO

--CREATE DATABASE WholesaleStore
--GO

USE WholesaleStore

/*
* UnitStorages таблица типов хранения
* Title наименование типа
*/
CREATE TABLE UnitStorages(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(20) NOT NULL UNIQUE CHECK(Title !=''),
)
GO

/*
* Products таблица товаров
* Title наименование товара
* PurchasePrice закупочная цена
* SalesPrice цена продажи
* IdUnitStorage id типа хранения
*/
CREATE TABLE Products(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(120) NOT NULL CHECK(Title !=''),
	PurchasePrice MONEY NOT NULL DEFAULT 0 CHECK(PurchasePrice > 0),
	SalesPrice MONEY NOT NULL DEFAULT 0 CHECK(SalesPrice > 0),	
	IdUnitStorage INT NOT NULL,
	FOREIGN KEY (IdUnitStorage) REFERENCES UnitStorages (Id)
)
GO

/*
* Sellers таблица продавцов
* Surname фамилия
* Name имя
* Patronymic отчетсво
*/
CREATE TABLE Sellers(
	Id INT PRIMARY KEY IDENTITY,
	Surname NVARCHAR(120) NOT NULL CHECK(Surname !=''),
	Name NVARCHAR(120) NOT NULL CHECK(Name !=''),
	Patronymic NVARCHAR(120) NOT NULL CHECK(Patronymic !=''),
	Сommissions FLOAT NOT NULL DEFAULT 0 CHECK(Сommissions >= 0)
)
GO

/*
* Sales таблица продаж
* IdProduct id товара
* Count кол-во проданных единиц
* IdSeller id продавца
* DateSale дата продажи
*/
CREATE TABLE Sales(
	Id INT PRIMARY KEY IDENTITY,
	IdProduct INT NOT NULL,
	Count INT NOT NULL CHECK(Count >= 0),
	IdSeller INT NOT NULL,
	DateSale DATE NOT NULL,
	FOREIGN KEY (IdProduct) REFERENCES Products (Id),
	FOREIGN KEY (IdSeller) REFERENCES Sellers (Id)
)
GO