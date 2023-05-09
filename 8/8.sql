---1
drop tablespace blob_lab;

CREATE TABLESPACE blob_lab
    DATAFILE 'D:\универ\БД\лабы\8\ts_lab2.dbf'
    SIZE 1000 m
    AUTOEXTEND ON NEXT 500M
    MAXSIZE 2000M
    EXTENT MANAGEMENT LOCAL;

--2
create directory bfile_dir as 'D:\универ\БД\лабы\8\pics\';
drop DIRECTORY bfile_dir;
--3
alter session set "_ORACLE_SCRIPT"=true;  
CREATE USER lob_user IDENTIFIED BY 12345
    DEFAULT TABLESPACE blob_lab QUOTA UNLIMITED ON blob_lab
    ACCOUNT UNLOCK;
    
grant create table to lob_user; 
grant create session, CREATE ANY DIRECTORY, DROP ANY DIRECTORY to lob_user;
grant all privileges to lob_user;
--5
drop table blob_t;
create table blob_t (
    id number primary key not null,
    foto blob not null,
    doc bfile
    );

--6
insert into blob_t values (3,
utl_raw.cast_to_raw('D:\универ\учеба\Blog\frontside\src\images\1448006675_5.jpg'),
BFILENAME( 'BFILE_DIR', '6.doc'));

DECLARE
  l_bfile BFILE;
  l_blob  BLOB;
BEGIN
  l_bfile := BFILENAME('BFILE_DIR', '6.doc');
  DBMS_LOB.FILEOPEN(l_bfile, DBMS_LOB.FILE_READONLY);
  DBMS_LOB.CREATETEMPORARY(l_blob, TRUE);
  DBMS_LOB.LOADFROMFILE(l_blob, l_bfile, DBMS_LOB.GETLENGTH(l_bfile));
  DBMS_LOB.FILECLOSE(l_bfile);
  -- do something with the blob variable, such as write it to a file
END;

  
--check
select * from blob_t;
select * from all_directories;
update blob_t set doc = BFILENAME( 'bfile_dir', 'doc.txt') where id =3;