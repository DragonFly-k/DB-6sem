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

--drop PROCEDURE ReadExternalData
CREATE PROCEDURE ReadExternalData
    @FilePath NVARCHAR(MAX)
AS EXTERNAL NAME Licence_assambly.StoredProcedures.ReadExternalData;
go

EXEC ReadExternalData 'D:\универ\БД\лабы\3\3.txt'
go

CREATE TYPE LicDescription EXTERNAL NAME Licence_assambly.LicDescription;

declare @LicDescription as dbo.LicDescription;
set @LicDescription = '5, 886';
print @LicDescription.ToString();