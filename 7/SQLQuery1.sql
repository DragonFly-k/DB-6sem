---------
drop table Report

create table Report (
	id INTEGER primary key identity(1,1),
	xml_ XML
);
select * from Report
----------------
drop procedure gen_xml

create procedure gen_xml
as
declare @x XML
set @x = (select licensekey, name, price, startdate  from userlicenses
				join licenses on userlicenses.LicenseID = licenses.id
				join software on software.id = licenses.SoftwareID 	
				FOR XML AUTO);
SELECT @x
go

execute gen_xml;

------------------
create procedure add_report
as
DECLARE  @x XML  
SET @x = (select licensekey, name, price, startdate  from userlicenses
				join licenses on userlicenses.LicenseID = licenses.id
				join software on software.id = licenses.SoftwareID 
				for xml path('newName'))
insert into Report values(@x);
go
  
execute add_report
select * from Report;

------------
create primary xml index xml_ind on Report(xml_)
create xml index xml_in on Report(xml_) using xml index xml_ind for path

select * from Report where xml_.exist('//newName/price')=1;
-----------
drop procedure findXML;

CREATE PROCEDURE findXML
    @value NVARCHAR(50)
AS
BEGIN
    SELECT * FROM Report
    WHERE xml_.exist('/newName/*[.=sql:variable("@value")]')=1
END

EXECUTE findXML 'kdkd';
EXECUTE findXML 'kd';