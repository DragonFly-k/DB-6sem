create database License_Management
--drop database License_Management

delete from Software
ALTER TABLE Userr ADD location geography;

CREATE TABLE Userr (
  ID INT PRIMARY KEY IDENTITY(1,1),
  Name VARCHAR(50) NOT NULL,
  Email VARCHAR(100) NOT NULL,
);
CREATE TABLE Software (
  ID INT PRIMARY KEY IDENTITY(1,1),
  Name VARCHAR(50) NOT NULL,
  Version VARCHAR(20) NOT NULL,
  Manufacturer VARCHAR(50) NOT NULL
);
CREATE TABLE Licenses (
  ID INT PRIMARY KEY IDENTITY(1,1),
  SoftwareID INT NOT NULL FOREIGN KEY REFERENCES Software(ID),
  Price INT NOT NULL
);
CREATE TABLE  UserLicenses(
  ID INT PRIMARY KEY IDENTITY(1,1),
  UserID  INT NOT NULL FOREIGN KEY REFERENCES Userr(ID),
  LicenseID INT NOT NULL FOREIGN KEY REFERENCES Licenses(ID),
  LicenseKey VARCHAR(50) NOT NULL,
  StartDate DATE NOT NULL,
  EndDate DATE NOT NULL  
);
go

CREATE NONCLUSTERED INDEX idx_User ON Userr (Name,Email);
CREATE NONCLUSTERED INDEX idx_Software ON Software (Name,Version,Manufacturer);
CREATE NONCLUSTERED INDEX idx_UserLicenses
ON UserLicenses (UserID, StartDate, EndDate)
INCLUDE (LicenseID, LicenseKey);
CREATE INDEX idx_FilteredLicense
ON Licenses (SoftwareID) WHERE Price > 1000;
go

CREATE VIEW ActiveUserLicenses AS
SELECT Userr.Name AS UserName, Software.Name, Licenses.Price, UserLicenses.StartDate, UserLicenses.EndDate
FROM Userr
JOIN UserLicenses ON Userr.ID = UserLicenses.UserID
JOIN Licenses ON UserLicenses.LicenseID = Licenses.ID
JOIN Software ON Licenses.SoftwareID = Software.ID
WHERE UserLicenses.EndDate >= GETDATE();
go
CREATE VIEW ExpiredUserLicenses AS
SELECT Userr.Name AS UserName, Software.Name AS SoftwareName, Licenses.Price, UserLicenses.StartDate, UserLicenses.EndDate
FROM Userr
JOIN UserLicenses ON Userr.ID = UserLicenses.UserID
JOIN Licenses ON UserLicenses.LicenseID = Licenses.ID
JOIN Software ON Licenses.SoftwareID = Software.ID
WHERE UserLicenses.EndDate < GETDATE();
go
CREATE VIEW vw_UserLicenses AS
SELECT Userr.Name AS UserName,Userr.Email,Software.Name AS SoftwareName,Software.Version,Software.Manufacturer,
Licenses.Price,UserLicenses.LicenseKey,UserLicenses.StartDate,UserLicenses.EndDate
FROM Userr
INNER JOIN UserLicenses ON Userr.ID = UserLicenses.UserID
INNER JOIN Licenses ON UserLicenses.LicenseID = Licenses.ID
INNER JOIN Software ON Licenses.SoftwareID = Software.ID;
go
select * from ActiveUserLicenses;
select * from ExpiredUserLicenses;
select * from vw_UserLicenses;
go

CREATE or alter PROCEDURE AddUser (@Name VARCHAR(50), @Email VARCHAR(100), @location VARCHAR(max))
AS
BEGIN
INSERT INTO Userr(Name, Email, location)VALUES (@Name, @Email,'geography::STPointFromText('@location', 4326);')
END;
go
CREATE PROCEDURE AddSoftware (@Name VARCHAR(50), @Version VARCHAR(20), @Manufacturer VARCHAR(50))
AS
BEGIN
INSERT INTO Software(Name, Version, Manufacturer) VALUES (@Name, @Version, @Manufacturer)
END;
go
CREATE PROCEDURE AddLicenses (@SoftwareID INT, @Price INT)
AS
BEGIN
INSERT INTO Licenses(SoftwareID, Price) VALUES (@SoftwareID, @Price)
END;
go
CREATE OR ALTER PROCEDURE AddUserLicenses (@UserID INT, @LicenseID INT, @LicenseKey VARCHAR(50), @StartDate DATE, @EndDate DATE)
AS
BEGIN
    IF EXISTS (
        SELECT * FROM UserLicenses 
		WHERE UserID = @UserID AND LicenseID = @LicenseID AND EndDate > GETDATE())
    BEGIN
        RAISERROR('Cannot add a license for the same software until the current license expires.', 16, 1);
        RETURN;
    END;
    
    INSERT INTO UserLicenses(UserID, LicenseID, LicenseKey, StartDate, EndDate)
    VALUES (@UserID, @LicenseID, @LicenseKey, @StartDate, @EndDate);
END;

go
CREATE PROCEDURE GetLicensesToUpdateNextMonth
AS
BEGIN
SELECT UserLicenses.LicenseKey,UserLicenses.StartDate,UserLicenses.EndDate,Licenses.Price,
Software.Name AS SoftwareName,Userr.Name AS UserName
FROM UserLicenses 
INNER JOIN Licenses ON UserLicenses.LicenseID = Licenses.ID
INNER JOIN Software ON Licenses.SoftwareID = Software.ID
INNER JOIN Userr ON UserLicenses.UserID = Userr.ID
WHERE MONTH(UserLicenses.EndDate) = MONTH(DATEADD(MONTH, 1, GETDATE())) 
AND YEAR(UserLicenses.EndDate) = YEAR(DATEADD(MONTH, 1, GETDATE()))
END;
go
EXEC GetLicensesToUpdateNextMonth;
delete userr
delete Software
delete Licenses
delete UserLicenses

INSERT INTO Userr (name, email, location) 
  VALUES(1,1,geography::STGeomFromText('POINT(55.9271035250276 -3.29431266523898)',4326))
  INSERT INTO Userr (name, email, location) 
  VALUES(4,4,geography::STGeomFromText('POINT(55.9271035250276 -3.29431266523898)',4326))
INSERT INTO Userr (name, email, location) 
  VALUES(2,2,geography::STGeomFromText('POINT(54.9271035250276 -3.29431266523898)',4326))
INSERT INTO Userr (name, email, location) 
  VALUES(3,3,geography::STGeomFromText('POINT(56.9271035250276 -3.29431266523898)',4326))

EXEC AddUser @Name = '1', @Email = '1',@location= 'POINT(52.3833 30.4061)';
EXEC AddUser @Name = '2', @Email = '2',@location= 'POINT(54.3833 30.4061)';
EXEC AddUser @Name = '3', @Email = '3',@location= 'POINT(56.3833 30.4061)';

EXEC AddSoftware @Name='4', @Version='4', @Manufacturer='1';
EXEC AddSoftware @Name='2', @Version='5', @Manufacturer='2';
EXEC AddSoftware @Name='3', @Version='7', @Manufacturer='3';

EXEC AddLicenses @SoftwareID= 1, @Price=1;
EXEC AddLicenses @SoftwareID= 2, @Price=2;
EXEC AddLicenses @SoftwareID= 3, @Price=3;

EXEC AddUserLicenses @UserID = 1, @LicenseID = 1, @LicenseKey='kfslk5s5', @StartDate='2017-07-12', @EndDate ='2025-07-12';
EXEC AddUserLicenses @UserID = 2, @LicenseID = 2, @LicenseKey='slgf5dsf', @StartDate='2012-07-12', @EndDate ='2023-03-12';
EXEC AddUserLicenses @UserID = 3, @LicenseID = 3, @LicenseKey='dkflg8sd', @StartDate='2020-07-12', @EndDate ='2023-07-12';
EXEC AddUserLicenses @UserID = 1, @LicenseID = 3, @LicenseKey='dkflg8sd', @StartDate='2020-07-12', @EndDate ='2023-03-12';
EXEC AddUserLicenses @UserID = 1, @LicenseID = 2, @LicenseKey='dkflg8sd', @StartDate='2023-03-01', @EndDate ='2023-03-12';
go


CREATE FUNCTION count_user_licenses (@user_id INT) RETURNS INT
AS
BEGIN
  DECLARE @count INT
  SELECT @count = COUNT(*) FROM UserLicenses WHERE UserID = @user_id
  RETURN @count
END;
go
CREATE FUNCTION ActiveLicensesCount() RETURNS INT
AS
BEGIN
  DECLARE @ActiveLicenses INT
  SELECT @ActiveLicenses = COUNT(*) FROM UserLicenses WHERE StartDate <= GETDATE() AND EndDate >= GETDATE()
  RETURN @ActiveLicenses
END;
go
CREATE FUNCTION GetLicenseCountBySoftwareID (@SoftwareID INT) RETURNS INT
AS
BEGIN
  DECLARE @Result INT;
  SELECT @Result = COUNT(*) FROM Licenses WHERE SoftwareID = @SoftwareID;
  RETURN @Result;
END;
go
SELECT dbo.count_user_licenses(1);
SELECT dbo.ActiveLicensesCount();
SELECT dbo.GetLicenseCountBySoftwareID(1);
go

CREATE TABLE UserLog (
ID INT PRIMARY KEY IDENTITY(1,1),
UserID INT NOT NULL,
ActionType VARCHAR(50) NOT NULL,
ActionDate DATETIME NOT NULL
);
go

CREATE TRIGGER trg_InsertUser ON Userr
AFTER INSERT
AS
BEGIN
INSERT INTO UserLog (UserID, ActionType, ActionDate)
SELECT ID, 'Insert', GETDATE()
FROM INSERTED;
END
go
CREATE TRIGGER trg_UpdateUser ON Userr
AFTER UPDATE
AS
BEGIN
INSERT INTO UserLog (UserID, ActionType, ActionDate)
SELECT ID, 'Update', GETDATE() FROM INSERTED;
END
go
CREATE TRIGGER trg_DeleteUser ON Userr
AFTER DELETE
AS
BEGIN
INSERT INTO UserLog (UserID, ActionType,ActionDate)
SELECT ID, 'Delete', GETDATE() FROM DELETED;
END;
go

select * from UserLog
select * from Userr
select * from UserLicenses
INSERT INTO Userr(Name, Email)VALUES ('trigger', 'tr')
UPDATE Userr SET Email='new' WHERE Name='trigger'
DELETE Userr WHERE Name='trigger'

delete UserLicenses WHERE id =4