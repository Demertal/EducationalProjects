USE [master]
GO

DROP DATABASE IF EXISTS TestBD
GO

CREATE DATABASE TestBD COLLATE Cyrillic_General_CI_AS
GO

USE TestBD

/*
* Products ������� �������
* Title ������������ ������
*/
CREATE TABLE Products(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(30) NOT NULL UNIQUE CHECK(Title !='')
)
GO

/*
* Categories ������� ���������
* Title ������������ ���������
*/
CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(30) NOT NULL UNIQUE CHECK(Title !='')
)
GO

/*
* ProductsInCategories ������� ������� � ���������
* Title ������������ ���������
*/
CREATE TABLE ProductsInCategories(
	Id INT PRIMARY KEY IDENTITY,
	IdProduct INT,
	Id�ategory INT,		
	FOREIGN KEY (IdProduct) REFERENCES Products (Id),
	FOREIGN KEY (Id�ategory) REFERENCES Categories (Id)
)
GO

INSERT INTO Products VALUES ('�����1'), ('�����2'), ('�����3'), ('�����4'), ('�����5')
INSERT INTO Categories VALUES ('���������1'), ('���������2'), ('���������3'), ('���������4'), ('���������5')
INSERT INTO ProductsInCategories VALUES (1, 1), (1, 3), (1, 5), (2, 2), (2, 4), (3, 1), (3, 2), (3, 5)

SELECT Products.Title, Categories.Title FROM Products 
	LEFT JOIN ProductsInCategories ON Products.Id = ProductsInCategories.IdProduct
	LEFT JOIN Categories ON Categories.Id = ProductsInCategories.Id�ategory 
	ORDER BY Products.Id