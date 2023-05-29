--1
CREATE TABLE Userr (
  ID INT PRIMARY KEY IDENTITY(1,1),
  Name VARCHAR(50) NOT NULL,
  Email VARCHAR(100) NOT NULL
);
CREATE TABLE Software (
  ID INT PRIMARY KEY IDENTITY(1,1),
  Name VARCHAR(50) NOT NULL,
  Version VARCHAR(20) NOT NULL,
  Manufacturer VARCHAR(50) NOT NULL,
  DeviceTypeID INT NOT NULL FOREIGN KEY REFERENCES DeviceTypes(ID)
);
CREATE TABLE Licenses (
  ID INT PRIMARY KEY IDENTITY(1,1),
  SoftwareID INT NOT NULL FOREIGN KEY REFERENCES Software(ID),
  Price INT NOT NULL
);
CREATE TABLE UserLicenses (
  ID INT PRIMARY KEY IDENTITY(1,1),
  UserID INT NOT NULL FOREIGN KEY REFERENCES Userr(ID),
  LicenseID INT NOT NULL FOREIGN KEY REFERENCES Licenses(ID),
  LicenseKey VARCHAR(50) NOT NULL,
  StartDate DATE NOT NULL,
  EndDate DATE NOT NULL
);
CREATE TABLE DeviceTypes (
  ID INT PRIMARY KEY IDENTITY(1,1),
  TypeName VARCHAR(50) NOT NULL
);
CREATE TABLE Devices (
  ID INT PRIMARY KEY IDENTITY(1,1),
  DeviceTypeID INT NOT NULL FOREIGN KEY REFERENCES DeviceTypes(ID),
  DeviceName VARCHAR(50) NOT NULL
);

INSERT INTO Userr (Name, Email) VALUES
('John Doe', 'john.doe@example.com'),
('Jane Smith', 'jane.smith@example.com'),
('Michael Johnson', 'michael.johnson@example.com'),
('Emily Davis', 'emily.davis@example.com'),
('David Wilson', 'david.wilson@example.com'),
('Sarah Anderson', 'sarah.anderson@example.com'),
('Christopher Taylor', 'christopher.taylor@example.com'),
('Jessica Martinez', 'jessica.martinez@example.com'),
('Matthew Brown', 'matthew.brown@example.com'),
('Lauren Thomas', 'lauren.thomas@example.com'),
('Andrew Garcia', 'andrew.garcia@example.com'),
('Olivia Rodriguez', 'olivia.rodriguez@example.com'),
('Daniel Martinez', 'daniel.martinez@example.com'),
('Ava Hernandez', 'ava.hernandez@example.com'),
('William Clark', 'william.clark@example.com'),
('Sophia Lewis', 'sophia.lewis@example.com'),
('Joseph Young', 'joseph.young@example.com'),
('Mia Lee', 'mia.lee@example.com'),
('James Hall', 'james.hall@example.com'),
('Abigail Wright', 'abigail.wright@example.com');
INSERT INTO DeviceTypes (TypeName)
VALUES ('Type 1'), ('Type 2'), ('Type 3'), ('Type 4'), ('Type 5'),
       ('Type 6'), ('Type 7'), ('Type 8'), ('Type 9'), ('Type 10'),
       ('Type 11'), ('Type 12'), ('Type 13'), ('Type 14'), ('Type 15'),
       ('Type 16'), ('Type 17'), ('Type 18'), ('Type 19'), ('Type 20');
INSERT INTO Devices (DeviceTypeID, DeviceName)
VALUES (1, 'Device 1'), (2, 'Device 2'), (3, 'Device 3'), (4, 'Device 4'), (5, 'Device 5'),
       (6, 'Device 6'), (7, 'Device 7'), (8, 'Device 8'), (9, 'Device 9'), (10, 'Device 10'),
       (11, 'Device 11'), (12, 'Device 12'), (13, 'Device 13'), (14, 'Device 14'), (15, 'Device 15'),
       (16, 'Device 16'), (17, 'Device 17'), (18, 'Device 18'), (19, 'Device 19'), (20, 'Device 20');
INSERT INTO Software (Name, Version, Manufacturer, DeviceTypeID)
VALUES ('Software 1', '1.0', 'Manufacturer 1', 2), ('Software 2', '1.0', 'Manufacturer 2', 2),
       ('Software 3', '1.0', 'Manufacturer 3', 1), ('Software 4', '1.0', 'Manufacturer 4', 4),
       ('Software 5', '1.0', 'Manufacturer 5', 2), ('Software 6', '1.0', 'Manufacturer 6', 6),
       ('Software 7', '1.0', 'Manufacturer 7', 7), ('Software 8', '1.0', 'Manufacturer 8', 8),
       ('Software 9', '1.0', 'Manufacturer 9', 9), ('Software 10', '1.0', 'Manufacturer 10', 10),
       ('Software 11', '1.0', 'Manufacturer 11', 11), ('Software 12', '1.0', 'Manufacturer 12', 12),
       ('Software 13', '1.0', 'Manufacturer 13', 13), ('Software 14', '1.0', 'Manufacturer 14', 14),
       ('Software 15', '1.0', 'Manufacturer 15', 15), ('Software 16', '1.0', 'Manufacturer 16', 16),
       ('Software 17', '1.0', 'Manufacturer 17', 17), ('Software 18', '1.0', 'Manufacturer 18', 18),
       ('Software 19', '1.0', 'Manufacturer 19', 19), ('Software 20', '1.0', 'Manufacturer 20', 20);
INSERT INTO Licenses (SoftwareID, Price) VALUES
(1, 199),
(2, 299),
(3, 100),
(4, 150),
(5, 2499),
(6, 9999),
(7, 8640),
(8, 780),
(9, 550),
(10, 1999),
(11, 70),
(12, 180),
(13, 149),
(14, 2999),
(15, 780),
(16, 610),
(17, 149),
(18, 610),
(19, 3240),
(20, 755);
INSERT INTO UserLicenses (UserID, LicenseID, LicenseKey, StartDate, EndDate) VALUES
(1, 1, 'ABC123', '2023-01-01', '2023-12-31'),
(2, 2, 'DEF456', '2023-02-01', '2023-12-31'),
(3, 3, 'GHI789', '2022-03-01', '2023-12-31'),
(4, 4, 'JKL012', '2022-04-01', '2023-12-31'),
(5, 5, 'MNO345', '2022-05-01', '2023-12-31'),
(6, 6, 'PQR678', '2022-06-01', '2023-12-31'),
(7, 7, 'STU901', '2022-07-01', '2023-12-31'),
(8, 8, 'VWX234', '2022-08-01', '2023-12-31'),
(9, 9, 'YZA567', '2022-09-01', '2023-12-31'),
(10, 10, 'BCD890', '2022-10-01', '2023-12-31'),
(11, 11, 'EFG123', '2022-11-01', '2023-12-31'),
(12, 12, 'HIJ456', '2022-12-01', '2023-12-31'),
(13, 13, 'KLM789', '2023-01-01', '2023-12-31'),
(14, 14, 'NOP012', '2023-02-01', '2023-12-31'),
(15, 15, 'QRS345', '2023-03-01', '2023-12-31'),
(16, 16, 'TUV678', '2023-04-01', '2023-12-31'),
(17, 17, 'WXY901', '2023-05-01', '2023-12-31'),
(18, 18, 'ZAB234', '2022-06-01', '2023-12-31'),
(19, 19, 'CDE567', '2022-07-01', '2023-12-31'),
(20, 20, 'FGH890', '2022-08-01', '2023-12-31');

select * from userr
select * from Software
select * from Licenses
select * from UserLicenses
select * from Devices
select * from DeviceTypes
DELETE FROM Userr;
DELETE FROM Software;
DELETE FROM Licenses;
DELETE FROM UserLicenses;
drop table Licenses

--2
--Вычисление итогов стоимости определенного вида ПО за период:
-- количество и стоимость лицензий;
-- сравнение их с общим количество лицензий (в %);
-- сравнение их с общей стоимостью лицензий (в %).
SELECT
  s.Name AS SoftwareName,
  COUNT(l.ID) OVER (PARTITION BY s.ID) AS LicenseCount,
  SUM(l.Price) OVER (PARTITION BY s.ID) AS TotalPrice,
  100.0 * COUNT(u.ID) / SUM(COUNT(u.ID)) OVER () AS LicenseCountPercentage,
  100.0 * SUM(l.Price) / SUM(SUM(l.Price)) OVER () AS TotalPricePercentage
FROM Software s
  INNER JOIN Licenses l ON s.ID = l.SoftwareID
  INNER JOIN UserLicenses u ON l.ID = u.LicenseID
WHERE u.StartDate >= '2023-01-01' AND u.EndDate <= '2023-12-31'
Group by s.Name, s.ID, l.ID, l.Price

--3
WITH Rows AS
(
    SELECT *, ROW_NUMBER() OVER (ORDER BY name) AS RowNumber
    FROM Software
)
SELECT * FROM Rows
WHERE RowNumber BETWEEN 21 AND 40;

--4
WITH Rows AS
(
  SELECT ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY ID) AS RowNumber,
    * FROM Userr
)
DELETE FROM Rows WHERE RowNumber > 1;

select * from userr

--5
--Вернуть для каждого вендора суммы затраченных на лицензирование средств за последние 6 месяцев помесячно.
SELECT Manufacturer,
 MONTH(StartDate) AS Month,
 YEAR(StartDate) AS Year,
 SUM(Price) OVER (PARTITION BY Manufacturer, YEAR(StartDate), MONTH(StartDate)) AS TotalSpent
FROM Software
 JOIN Licenses ON Software.ID = Licenses.SoftwareID
 JOIN UserLicenses ON Licenses.ID = UserLicenses.LicenseID
WHERE StartDate >= DATEADD(MONTH, -6, GETDATE())
ORDER BY Manufacturer, YEAR(StartDate), MONTH(StartDate);

--6
--Какой тип программного обеспечения использовался наибольшее число раз для устройств определенного вида? Вернуть для всех видов.
SELECT
  dt.TypeName AS DeviceType,
  s.Name AS SoftwareType,
  ROW_NUMBER() OVER (PARTITION BY dt.TypeName ORDER BY COUNT(*) DESC) AS Rank
FROM DeviceTypes dt
JOIN Devices d ON dt.ID = d.DeviceTypeID
JOIN Software s ON d.DeviceTypeID = s.DeviceTypeID
GROUP BY dt.TypeName, s.Name
ORDER BY dt.TypeName, Rank;







