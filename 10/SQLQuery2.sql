--2
create Login kkkk with password='kkkk';		
create user kkkk for login kkkk;				
create role user_role;	
alter role user_role add member kkkk;

grant select, insert, update, delete to user_role;
revoke update from user_role;

--3. заимствование прав 
CREATE PROCEDURE MyProcedure AS
BEGIN
   SELECT * FROM userr;
END

CREATE ROLE MyRole;
GRANT EXECUTE ON MyProcedure TO MyRole;

create Login MyUser with password='MyUser';		
CREATE USER MyUser FOR LOGIN MyUser;
ALTER ROLE MyRole ADD MEMBER MyUser;

EXECUTE AS LOGIN = 'kkkk';
EXEC MyProcedure;
REVERT;

--4.
use master;
go

create server audit Audit to file( 
	filepath = 'D:/универ/БД/лабы/10',
	maxsize = 10 mb, max_rollover_files = 0, reserve_disk_space = off
) with ( queue_delay = 1000, on_failure = continue);

create server audit PAudit to application_log;
create server audit AAudit to security_log;

select * from sys.server_audits;

--5. 
create server audit specification ServerAuditSpecification12
for server audit Audit
	add (database_change_group)
	with (state=on)

--6.
alter server audit Audit with (state=on);
create database kkkk;
drop database kkkk;
select statement from fn_get_audit_file('D:\универ\БД\лабы\10\Audit_5E3D75B6-8488-416D-88A1-0CD7B190C58B_0_133289038584550000.sqlaudit', null, null);	

--7-9
use Lice;
go
create database audit specification DBAudit
for server audit Audit 
add (insert on Lice.dbo.Userr by dbo)
with (state=on);

select * from Lice.dbo.Userr
INSERT INTO Userr (name, email) VALUES ('kl;','kjg');

select statement from fn_get_audit_file('D:\универ\БД\лабы\10\Audit_5E3D75B6-8488-416D-88A1-0CD7B190C58B_0_133289038584550000.sqlaudit', null, null);	

--10. 
use master;
go
alter server audit Audit with (state=off);
alter server audit PAudit with (state=off);
alter server audit AAudit with (state=off);

--11.
use Lice;
go
create asymmetric key Akey
with algorithm = rsa_2048
encryption by password = 'klaskd';

--12. 
declare @text nvarchar(21);
declare @ctext nvarchar (256);
set @text = 'qwerty qwerty qwerty qwerty';
print @text;
set @ctext = EncryptByAsymKey(AsymKey_ID('AKey'), @text);
print @ctext;
set @text = DecryptByAsymKey(AsymKey_ID('AKey'), @ctext, N'klaskd');
print @text;

--13.
create certificate Cert
encryption by password = N'klaskd'
with subject = N'Certificate',
expiry_date = N'01.01.2024'

--14.
declare @text nvarchar(21);
declare @ctext nvarchar (256);
set @text = 'qwerty qwerty qwerty qwerty';
print @text;
set @ctext = EncryptByCert(Cert_ID('Cert'), @text);
print @ctext;
set @text = CAST(DecryptByCert(Cert_ID('Cert'), @ctext, N'klaskd') as nvarchar(21));
print @text;
	
--15. 
create symmetric key SKey
with algorithm = AES_256
encryption by password = N'klaskd';

open symmetric key SKey 
decryption by password = N'klaskd';

--16.
declare @text nvarchar(21);
declare @ctext nvarchar (256);
set @text = 'qwerty qwerty qwerty qwerty';
print @text;
set @ctext = EncryptByKey(Key_GUID('SKey'), @text);
print @ctext;
set @text = CAST(DecryptByKey(@ctext) as nvarchar(21));
print @text;

close symmetric key SKey;

--17. ---прозрачное шифрование
use master;
create master key encryption by password = 'password';

create certificate MCert 
with subject = 'certificate', 
expiry_date = '01.01.2024';

use Lice;
create database encryption key
with algorithm = AES_256
encryption by server certificate MCert;

--18
select HashBytes('MD4', 'lkkkklklkl');
select HashBytes('SHA1', 'lkkkklklkl');

--19.
select * from sys. certificates;
select SIGNBYCERT(Cert_ID('Cert'), N'kkkkkkk', N'klaskd') as ЭЦП;	
select VERIFYSIGNEDBYCERT(Cert_ID('Cert'), N'kkkkkkk', 0x010005020400000086D56FC59B806CE6BF97A6A0538DDE5939AAAF5D1C7D8A22FB0DF8DFF6266E543F8C93220A32979472C5FA3FDF76E4EAFB9D7E58E05EDFCEEE4116423600082DCE591E7EB9500A0D4036839AEBC3C6F4FE5C7F88CC740232454008BE8FE72CC2410E5623CEEA3F22F002D494E5190399019AE218047606F75DB254CA54BC25666313E426344F38CF16AF54A00CD5386E5247A4B8C44D46F723D95D9999E3194981051F4478D9D0FC7AD4C01AFBBBFFFE5C0509E5D28351019A0EC72D66331F3B1A6D0DF315A158F4F1CEA31D40CCCD29772EBF1F139CA078986D88CB9035EC9DE6D09E37D2C0BE689E8532D2F7E6FD0E3EFB6B6BEAF9B93566E482EB7FC6B128);
	
--20
backup certificate Cert
to file = N'D:/универ/БД/лабы/10/Cert.cer'
with private key(
file = N'D:/универ/БД/лабы/10/Cert.pvk',
encryption by password = N'klaskd',
decryption by password = N'klaskd');

use master;
BACKUP MASTER KEY TO FILE = 'D:/универ/БД/лабы/10/MasterKey.key' 
ENCRYPTION BY PASSWORD = 'klaskd';

		