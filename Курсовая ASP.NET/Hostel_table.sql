USE [master]
GO

DROP DATABASE IF EXISTS [Hostel]
GO

CREATE DATABASE Hostel
GO

USE Hostel

CREATE TABLE Cities
(
	 Id INT PRIMARY KEY IDENTITY,
	 City NVARCHAR(20) NOT NULL UNIQUE CHECK(City !='')
);

CREATE TABLE Floors
(
	 Id INT PRIMARY KEY IDENTITY,
	 Floor INT NOT NULL UNIQUE CHECK(Floor != 0)
);

CREATE TABLE DaysWeek
(
	 Id INT PRIMARY KEY IDENTITY,
	 Day NVARCHAR(20) NOT NULL UNIQUE CHECK(Day !='')
);

CREATE TABLE Employee
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(20) NOT NULL CHECK(Name !=''),
	Patronymic NVARCHAR(20) NOT NULL CHECK(Patronymic !=''),
	Surname NVARCHAR(20) NOT NULL CHECK(Surname !='')
)

CREATE TABLE �leaning
(
	Id INT PRIMARY KEY IDENTITY,
	IdEmployee INT NOT NULL,
	IdFloor INT NOT NULL,
	IdDay INT NOT NULL,
	FOREIGN KEY (IdEmployee) REFERENCES Employee (Id) ON DELETE CASCADE,
	FOREIGN KEY (IdFloor) REFERENCES Floors (Id) ON DELETE CASCADE,
	FOREIGN KEY (IdDay) REFERENCES DaysWeek (Id) ON DELETE CASCADE,
	CONSTRAINT UNIQUE_Employee_Day_Floor UNIQUE (IdEmployee, IdFloor, IdDay)
)

CREATE TABLE RoomTypes
(
	Id INT PRIMARY KEY IDENTITY,
	Type NVARCHAR(20) NOT NULL UNIQUE CHECK(Type !=''),
	CountPlace INT NOT NULL CHECK(CountPlace > 0),
	Cost MONEY CHECK(Cost > 0),
)

CREATE TABLE Rooms
(
	Id INT PRIMARY KEY IDENTITY,
	IdRoomType INT NOT NULL,
	IdFloor INT NOT NULL,
	Phone NVARCHAR(9) NOT NULL UNIQUE CHECK(LEN(Phone) = 9),
	Number INT NOT NULL UNIQUE CHECK(Number > 0),
	FreePlace INT,
	FOREIGN KEY (IdRoomType) REFERENCES RoomTypes (Id) ON DELETE CASCADE,
	FOREIGN KEY (IdFloor) REFERENCES Floors (Id) ON DELETE CASCADE
)

CREATE TABLE Residents
(
	Id INT PRIMARY KEY IDENTITY,
	Passport NVARCHAR(10) NOT NULL CHECK(Passport !='' and LEN(Passport) = 10),
	Name NVARCHAR(20) NOT NULL CHECK(Name !=''),
	Patronymic NVARCHAR(20) NOT NULL CHECK(Patronymic !=''),
	Surname NVARCHAR(20) NOT NULL CHECK(Surname !=''),
	IdCity INT NOT NULL,
	SettlementDate DATE NOT NULL,
	IdRoom INT NOT NULL,
	IsEvicted BIT NOT NULL DEFAULT 0,
	FOREIGN KEY (IdCity) REFERENCES Cities (Id) ON DELETE CASCADE,
	FOREIGN KEY (IdRoom) REFERENCES Rooms (Id) ON DELETE CASCADE
)
go

CREATE TABLE Accommodation
(
	Id INT PRIMARY KEY IDENTITY,
	SettlementDate DATE NOT NULL,
	DepartureDate DATE DEFAULT NULL,
	IdRoom INT NOT NULL,
	FOREIGN KEY (IdRoom) REFERENCES Rooms (Id) ON DELETE CASCADE
)
go

CREATE TRIGGER Residents_INSERT
ON Residents
AFTER INSERT
AS
BEGIN
DECLARE @freePlace int; 
(select @freePlace = FreePlace FROM Rooms INNER JOIN inserted i on i.IdRoom = Rooms.Id);
if @freePlace != 0
BEGIN
	INSERT INTO Accommodation(SettlementDate, IdRoom) SELECT SettlementDate, IdRoom FROM INSERTED
	UPDATE Rooms SET FreePlace = FreePlace - 1 FROM Rooms INNER JOIN inserted i on i.IdRoom = Rooms.Id;
END
ELSE
BEGIN
    ROLLBACK TRANSACTION
END
END
GO

CREATE TRIGGER Rooms_INSERT
ON Rooms
AFTER INSERT
AS
BEGIN 
UPDATE Rooms SET FreePlace = (SELECT CountPlace FROM RoomTypes INNER JOIN inserted i on i.IdRoomType = RoomTypes.Id) FROM Rooms
    INNER JOIN inserted i on i.Id = Rooms.Id
END
GO

CREATE TRIGGER Residents_DELETE
ON Residents
AFTER DELETE
AS
BEGIN 
UPDATE Rooms SET FreePlace = FreePlace + 1 FROM Rooms
    INNER JOIN deleted i on i.IdRoom = Rooms.Id;
END
GO