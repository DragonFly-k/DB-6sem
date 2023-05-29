CREATE TABLE DeviceTypes (
  ID INTEGER PRIMARY KEY AUTOINCREMENT,
  TypeName TEXT NOT NULL
);

CREATE TABLE Devices (
  ID INTEGER PRIMARY KEY AUTOINCREMENT,
  DeviceTypeID INTEGER NOT NULL,
  DeviceName TEXT NOT NULL,
  FOREIGN KEY (DeviceTypeID) REFERENCES DeviceTypes(ID)
);

CREATE TABLE DeviceTrigg (
  ID INTEGER PRIMARY KEY AUTOINCREMENT,
  Event TEXT NOT NULL
);

INSERT INTO DeviceTypes (TypeName) VALUES
('Телефон'),('Ноутбук'),('Планшет'),('Камера'),('Монитор');

INSERT INTO Devices (DeviceTypeID, DeviceName) VALUES
(1, 'iPhone'),(1, 'Samsung Galaxy'),(2, 'MacBook Pro'),
(2, 'Dell XPS'),(3, 'iPad'),(4, 'Nikon D850'),
(4, 'Canon EOS 5D Mark IV'),(5, 'LG UltraFine'),
(5, 'Dell Ultrasharp'),(5, 'HP EliteDisplay');

CREATE VIEW DeviceView AS
SELECT DeviceName FROM Devices WHERE DeviceTypeID = 5;

CREATE TRIGGER DeviceTrigger
AFTER INSERT ON Devices
FOR EACH ROW
BEGIN
    INSERT INTO DeviceTrigg (Event) VALUES (NEW.DeviceName);
END;

DELETE FROM DeviceTrigg;
DELETE FROM Devices
DELETE FROM DeviceTypes

