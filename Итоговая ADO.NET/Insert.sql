USE WholesaleStore

INSERT INTO UnitStorages VALUES ('шт'), ('кг'), ('м')

INSERT INTO Products VALUES ('Мышь', 250, 300, 1), ('Чехол', 50, 100, 1), ('Соевый соус', 35, 80, 1), ('Наушники', 580, 700, 1), ('Клавиатура', 1200, 1300, 1), 
('Провод', 30, 300, 3), ('Фломастеры', 60, 160, 1), ('Табуретка', 100, 200, 1), ('Ножницы', 40, 85, 1), ('Сосиски', 50, 150, 2)

INSERT INTO Sellers VALUES ('Фамилия1', 'Имя1', 'Отчество1', 30), ('Фамилия2', 'Имя2', 'Отчество2', 25), ('Фамилия3', 'Имя3', 'Отчество3', 47),
('Фамилия4', 'Имя4', 'Отчество4', 19), ('Фамилия5', 'Имя5', 'Отчество5', 11), ('Фамилия6', 'Имя6', 'Отчество6', 23),
('Фамилия7', 'Имя7', 'Отчество7', 18), ('Фамилия8', 'Имя8', 'Отчество8', 37), ('Фамилия9', 'Имя9', 'Отчество9', 22), ('Фамилия10', 'Имя10', 'Отчество10', 17)

DECLARE @i INT
SET @i = 0;

WHILE @i < 20
BEGIN
	DECLARE @product INT, @seller INT, @count INT, @date DATE
	SET @product = RAND()*10+1
	SET @seller = RAND()*10+1
	SET @count = RAND()*20+1
	SET @date = DATEADD(day, RAND()*31, current_timestamp)
	INSERT INTO Sales VALUES (@product, @count, @seller, @date)
	SET @i = @i + 1
END;