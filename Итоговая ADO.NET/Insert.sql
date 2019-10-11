USE WholesaleStore

INSERT INTO UnitStorages VALUES ('��'), ('��'), ('�')

INSERT INTO Products VALUES ('����', 250, 300, 1), ('�����', 50, 100, 1), ('������ ����', 35, 80, 1), ('��������', 580, 700, 1), ('����������', 1200, 1300, 1), 
('������', 30, 300, 3), ('����������', 60, 160, 1), ('���������', 100, 200, 1), ('�������', 40, 85, 1), ('�������', 50, 150, 2)

INSERT INTO Sellers VALUES ('�������1', '���1', '��������1', 30), ('�������2', '���2', '��������2', 25), ('�������3', '���3', '��������3', 47),
('�������4', '���4', '��������4', 19), ('�������5', '���5', '��������5', 11), ('�������6', '���6', '��������6', 23),
('�������7', '���7', '��������7', 18), ('�������8', '���8', '��������8', 37), ('�������9', '���9', '��������9', 22), ('�������10', '���10', '��������10', 17)

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