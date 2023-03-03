exec sp_configure 'clr enabled',1;
reconfigure;

EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;

EXEC sp_configure 'clr strict security', 0;
RECONFIGURE;


CREATE ASSEMBLY Licence_assambly FROM 'D:\универ\БД\лабы\3\l3\bin\Debug\l3.dll'
go
--drop assembly Licence_assambly;
--drop procedure Test
create procedure Test
as
external name Licence_assambly.StoredProcedures.SqlStoredProcedure1;
go

exec Test
go

drop PROCEDURE ReadFileProcedure
CREATE PROCEDURE ReadFileProcedure
    @path NVARCHAR(255)
AS EXTERNAL NAME Licence_assambly.StoredProcedures.ReadTextFile;
go

EXEC ReadFileProcedure 'D:\универ\БД\лабы\3\3.txt';
go

--drop FUNCTION dbo.ReadTextFile
CREATE FUNCTION dbo.ReadTextFile (@path NVARCHAR(MAX), @pathto NVARCHAR(MAX))
RETURNS BIT
AS EXTERNAL NAME Licence_assambly.StoredProcedures.ReadTextFile;
go 

SELECT dbo.ReadTextFile('D:\универ\БД\лабы\3\3.txt', 'D:\универ\БД\лабы\3\new.txt')as Success;

--drop FUNCTION ReadFile
CREATE FUNCTION dbo.ReadFile(@path NVARCHAR(MAX))
RETURNS NVARCHAR(MAX)
AS EXTERNAL NAME Licence_assambly.StoredProcedures.ReadFile;

SELECT dbo.ReadFile('D:\универ\БД\лабы\3\3.txt');