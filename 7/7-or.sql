---
create table Report(
  id int generated always as identity,
  xml_ xmltype
);
----
create or replace function gen_XML2
return xmltype 
as
  xml xmltype;
  a      NVARCHAR2(500);
begin
  a:='select licensekey, name, price, startdate  from userlicenses
				join licenses on userlicenses.LicenseID = licenses.id
				join software on software.id = licenses.SoftwareID ';        
  select xmlelement("XML",
      xmlelement(evalname('license'),
      dbms_xmlgen.getxmltype(a)))
  into xml
  from dual;
  return xml;
end gen_XML2;

select gen_XML2 from dual;

CREATE OR REPLACE PROCEDURE INSERTXML_PROC(
    id integer,
    x IN XMLTYPE)
IS
BEGIN
  INSERT INTO Report (xml_) values (x);
  COMMIT;
END;

begin 
    INSERTXML_PROC(1, gen_XML2);
end;
select * from Report;
select r.xml_.GETSTRINGVAL() from Report r;

-----
drop procedure FINDXML;

create or replace procedure FINDXML(aa out VARCHAR2 ) 
is
begin
      select r.xml_.GETSTRINGVAL() xml
      into aa from Report r
      where r.xml_.EXISTSNODE('/XML/license/ROWSET/ROW/NAME')=1 and r.id=1;
end FINDXML;

DECLARE 
    word VARCHAR2(4000); 
BEGIN
  FINDXML(:word);
  DBMS_OUTPUT.PUT_LINE(:word);
END;

----------------------------------
drop INDEX po_xmlindex_ix ;
select * from Report;


EXPLAIN PLAN FOR
SELECT *
FROM Report
WHERE id = 10;

SELECT * FROM TABLE(DBMS_XPLAN.DISPLAY);

CREATE INDEX report_id_idx ON Report (id);




