--USE [master]
--GO

--DROP DATABASE IF EXISTS [WholesaleStore]
--GO

--CREATE DATABASE WholesaleStore
--GO

USE WholesaleStore

/*
* UnitStorages ������� ����� ��������
* Title ������������ ����
*/
CREATE TABLE UnitStorages(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(20) NOT NULL UNIQUE CHECK(Title !=''),
)
GO

/*
* Products ������� �������
* Title ������������ ������
* PurchasePrice ���������� ����
* SalesPrice ���� �������
* IdUnitStorage id ���� ��������
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
* Sellers ������� ���������
* Surname �������
* Name ���
* Patronymic ��������
*/
CREATE TABLE Sellers(
	Id INT PRIMARY KEY IDENTITY,
	Surname NVARCHAR(120) NOT NULL CHECK(Surname !=''),
	Name NVARCHAR(120) NOT NULL CHECK(Name !=''),
	Patronymic NVARCHAR(120) NOT NULL CHECK(Patronymic !=''),
	�ommissions FLOAT NOT NULL DEFAULT 0 CHECK(�ommissions >= 0)
)
GO

/*
* Sales ������� ������
* IdProduct id ������
* Count ���-�� ��������� ������
* IdSeller id ��������
* DateSale ���� �������
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