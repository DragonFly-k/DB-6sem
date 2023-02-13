create database License_Management

CREATE TABLE Userr (
  ID INT PRIMARY KEY IDENTITY(1,1),
  Name VARCHAR(50) NOT NULL,
  Email VARCHAR(100) NOT NULL
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
CREATE NONCLUSTERED INDEX idx_Licenses ON Licenses (Price);
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

CREATE PROCEDURE AddUser (@Name VARCHAR(50), @Email VARCHAR(100))
AS
BEGIN
INSERT INTO Userr(Name, Email)VALUES (@Name, @Email)
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
CREATE PROCEDURE AddUserLicenses (@UserID INT, @LicenseID INT, @LicenseKey VARCHAR(50), @StartDate DATE, @EndDate DATE)
AS
BEGIN
INSERT INTO UserLicenses(UserID, LicenseID, LicenseKey, StartDate, EndDate)
VALUES (@UserID, @LicenseID, @LicenseKey, @StartDate, @EndDate)
END;
go
EXEC AddUser @Name = 'dfkkd', @Email = 'jfdkld';
EXEC AddUser @Name = 'Kate', @Email = 'dkkff';
EXEC AddUser @Name = 'qrewdfks', @Email = 'dkfl';

EXEC AddSoftware @Name='java', @Version='5sd5', @Manufacturer='kjds';
EXEC AddSoftware @Name='node', @Version='855', @Manufacturer='kjdfskd';
EXEC AddSoftware @Name='python', @Version='266', @Manufacturer='fsn';

EXEC AddLicenses @SoftwareID= 1, @Price=6563;
EXEC AddLicenses @SoftwareID= 3, @Price=65;
EXEC AddLicenses @SoftwareID= 2, @Price=855;

EXEC AddUserLicenses @UserID = 1, @LicenseID = 1, @LicenseKey='kfslk5s5', @StartDate='2017-07-12', @EndDate ='2025-07-12';
EXEC AddUserLicenses @UserID = 2, @LicenseID = 3, @LicenseKey='slgf5dsf', @StartDate='2012-07-12', @EndDate ='2025-07-12';
EXEC AddUserLicenses @UserID = 3, @LicenseID = 2, @LicenseKey='dkflg8sd5', @StartDate='2017-07-12', @EndDate ='2018-07-12';

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
INSERT INTO Userr(Name, Email)VALUES ('trigger', 'tr')
UPDATE Userr SET Email='new' WHERE Name='trigger'
DELETE Userr WHERE Name='trigger'