USE Hostel
GO

DROP FUNCTION IF EXISTS FindResidentByRoom
DROP FUNCTION IF EXISTS FindResidentByCity
DROP FUNCTION IF EXISTS FindEmployeeByDayAndFloor
DROP FUNCTION IF EXISTS FindFreePlaceRooms
DROP FUNCTION IF EXISTS FindPartiallyFreePlaceRooms
GO

CREATE FUNCTION FindResidentByRoom(@id INT)
RETURNS TABLE AS RETURN 
(SELECT Residents.Name, Residents.Patronymic, Residents.Surname, Residents.Passport, Cities.City,
	Residents.SettlementDate FROM Residents JOIN Cities on Cities.Id = Residents.IdCity where IdRoom = @id)
GO

CREATE FUNCTION FindResidentByCity(@id INT)
RETURNS TABLE AS RETURN 
(SELECT Residents.Name, Residents.Patronymic, Residents.Surname, Residents.Passport,
	Rooms.Number, Residents.SettlementDate FROM Residents JOIN Rooms on Rooms.Id = Residents.IdRoom where IdCity = @id)
GO

CREATE FUNCTION FindEmployeeByDayAndFloor(@id_day INT, @id_floor INT)
RETURNS TABLE AS RETURN 
(SELECT * FROM Employee WHERE Id = (SELECT IdEmployee FROM Ñleaning WHERE IdDay = @id_day and IdFloor = @id_floor))
GO

CREATE FUNCTION FindFreePlaceRooms()
RETURNS TABLE AS RETURN 
(SELECT RoomTypes.Type, Floors.Floor, Rooms.Phone, Rooms.Number, RoomTypes.Cost FROM Rooms JOIN RoomTypes on RoomTypes.Id = Rooms.IdRoomType
  JOIN Floors on Floors.Id = Rooms.IdFloor WHERE Rooms.FreePlace = (SELECT CountPlace FROM RoomTypes WHERE Rooms.IdRoomType = RoomTypes.Id))
GO

CREATE FUNCTION FindPartiallyFreePlaceRooms()
RETURNS TABLE AS RETURN 
(SELECT RoomTypes.Type, Floors.Floor, Rooms.Phone, Rooms.Number, RoomTypes.Cost, Rooms.FreePlace FROM Rooms JOIN RoomTypes on RoomTypes.Id = Rooms.IdRoomType
  JOIN Floors on Floors.Id = Rooms.IdFloor WHERE Rooms.FreePlace < (SELECT CountPlace FROM RoomTypes WHERE Rooms.IdRoomType = RoomTypes.Id) and Rooms.FreePlace > 0)
GO

CREATE FUNCTION CleaningView()
RETURNS TABLE AS RETURN 
SELECT Ñleaning.Id, (SELECT Name FROM Employee WHERE Employee.Id = Ñleaning.IdEmployee) as Name,
	(SELECT Patronymic FROM Employee WHERE Employee.Id = Ñleaning.IdEmployee) as Patronymic,
	(SELECT Surname FROM Employee WHERE Employee.Id = Ñleaning.IdEmployee) as Surname,
	(SELECT Floor FROM Floors WHERE Floors.Id = Ñleaning.IdFloor) as Floor,
	(SELECT Day FROM DaysWeek WHERE DaysWeek.Id = Ñleaning.IdDay) as Day FROM Ñleaning
GO

CREATE FUNCTION CleaningViewByEmployee(@id INT)
RETURNS TABLE AS RETURN 
SELECT Ñleaning.Id, (SELECT Name FROM Employee WHERE Employee.Id = Ñleaning.IdEmployee) as Name,
	(SELECT Patronymic FROM Employee WHERE Employee.Id = Ñleaning.IdEmployee) as Patronymic,
	(SELECT Surname FROM Employee WHERE Employee.Id = Ñleaning.IdEmployee) as Surname,
	(SELECT Floor FROM Floors WHERE Floors.Id = Ñleaning.IdFloor) as Floor,
	(SELECT Day FROM DaysWeek WHERE DaysWeek.Id = Ñleaning.IdDay) as Day FROM Ñleaning WHERE Ñleaning.IdEmployee = @id
GO

CREATE FUNCTION RoomsView()
RETURNS TABLE AS RETURN 
SELECT Rooms.Id, Rooms.Number, RoomTypes.Type, Floors.Floor, Rooms.Phone, RoomTypes.Cost, Rooms.FreePlace FROM Rooms JOIN RoomTypes on RoomTypes.Id = Rooms.IdRoomType
  JOIN Floors on Floors.Id = Rooms.IdFloor
GO

CREATE FUNCTION RoomsViewForResident()
RETURNS TABLE AS RETURN 
SELECT Rooms.Id, Rooms.Number FROM Rooms JOIN Residents on Residents.IdRoom = Rooms.Id
GO

CREATE FUNCTION ResidentsView()
RETURNS TABLE AS RETURN 
SELECT Residents.Id, Residents.Name, Residents.Patronymic, Residents.Surname, Residents.Passport, Cities.City, Rooms.Number, Residents.SettlementDate FROM Residents
 JOIN Cities on Cities.Id = Residents.IdCity JOIN Rooms on Rooms.Id = Residents.IdRoom
GO

CREATE FUNCTION AccommodationView()
RETURNS TABLE AS RETURN 
SELECT Accommodation.Id, Accommodation.SettlementDate, Accommodation.DepartureDate, Rooms.Number FROM Accommodation
 JOIN Rooms on Rooms.Id = Accommodation.IdRoom
GO

CREATE FUNCTION CitiesForResidentView()
RETURNS TABLE AS RETURN 
SELECT Cities.Id, Cities.City FROM Cities where Cities.Id in (SELECT Residents.IdCity FROM Residents)
GO

CREATE FUNCTION Report(@start DATE, @end DATE)
RETURNS TABLE AS RETURN 
SELECT Rooms.Number, COUNT(*) as CountNum, SUM(datediff(d, Accommodation.SettlementDate, Accommodation.DepartureDate)) as CountDayBusy, RoomTypes.Cost
  FROM Accommodation RIGHT JOIN Rooms on Accommodation.IdRoom = Rooms.Id and @start <= Accommodation.SettlementDate JOIN RoomTypes on RoomTypes.Id = Rooms.IdRoomType GROUP BY Rooms.Id, Rooms.Number,  RoomTypes.Cost
GO