---1 sys
drop tablespace blob_lab INCLUDING CONTENTS;

CREATE TABLESPACE blob_lab
    DATAFILE 'D:\универ\БД\лабы\8\lab.dbf'
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
grant read, write on directory bfile_dir to lob_user;
grant DBA to lob_user;
GRANT EXECUTE ON DBMS_LOB TO lob_user;
--5 lob
drop table blob_t;
create table blob_t (
    id number primary key not null,
    foto blob ,
    doc bfile
);

select *from  blob_t
--6
declare
    fHnd bfile;
    b blob;
    srcOffset integer := 1;
    dstOffset integer := 1;
begin
    dbms_lob.CreateTemporary( b, true );
    fHnd := BFilename( 'ORACLE_BASE', '1.jpg' );
    dbms_lob.FileOpen( fHnd, DBMS_LOB.FILE_READONLY );
    dbms_lob.LoadFromFile( b, fHnd, DBMS_LOB.LOBMAXSIZE, dstOffset, srcOffset );
    insert into blob_t values(2, b, BFILENAME( 'ORACLE_BASE', '6.doc'));
    commit;
    dbms_lob.FileClose( fHnd );
end;
--check
select * from blob_t;