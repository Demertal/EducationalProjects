USE [master]
GO

DROP DATABASE IF EXISTS TestBD
GO

CREATE DATABASE TestBD COLLATE Cyrillic_General_CI_AS
GO

USE TestBD

/*
* Products таблица товаров
* Title наименование товара
*/
CREATE TABLE Products(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(30) NOT NULL UNIQUE CHECK(Title !='')
)
GO

/*
* Categories таблица категорий
* Title наименование категории
*/
CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(30) NOT NULL UNIQUE CHECK(Title !='')
)
GO

/*
* ProductsInCategories таблица товаров в катеориях
* Title наименование категории
*/
CREATE TABLE ProductsInCategories(
	Id INT PRIMARY KEY IDENTITY,
	IdProduct INT,
	IdСategory INT,		
	FOREIGN KEY (IdProduct) REFERENCES Products (Id),
	FOREIGN KEY (IdСategory) REFERENCES Categories (Id)
)
GO

INSERT INTO Products VALUES ('Товар1'), ('Товар2'), ('Товар3'), ('Товар4'), ('Товар5')
INSERT INTO Categories VALUES ('Категория1'), ('Категория2'), ('Категория3'), ('Категория4'), ('Категория5')
INSERT INTO ProductsInCategories VALUES (1, 1), (1, 3), (1, 5), (2, 2), (2, 4), (3, 1), (3, 2), (3, 5)

SELECT Products.Title, Categories.Title FROM Products 
	LEFT JOIN ProductsInCategories ON Products.Id = ProductsInCategories.IdProduct
	LEFT JOIN Categories ON Categories.Id = ProductsInCategories.IdСategory 
	ORDER BY Products.Id