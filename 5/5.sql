alter table Software add hid hierarchyid;
select* from Software;

delete from Software where id = 16;

insert into Software values (4,6,4, hierarchyid::GetRoot()); -- ������

declare @Id hierarchyid  
--����� ������������� �������� �� ������� hid ������� Software, ��� ������ ����� ����� ��������.
select @Id = MAX(hid) from Software where hid.GetAncestor(1) = hierarchyid::GetRoot(); -- ������ ������
update Software set hid = hierarchyid::GetRoot().GetDescendant(@id, null) where version = 1

declare @Id hierarchyid  
select @Id = MAX(hid) from Software where hid.GetAncestor(1) = hierarchyid::GetRoot(); -- ������ ������
update Software set hid = hierarchyid::GetRoot().GetDescendant(@id, null) where version = 2

declare @Id hierarchyid  
select @Id = MAX(hid) from Software where hid.GetAncestor(1) = (select hid from Software where version = 6); -- ������ 6
update Software set hid = (select hid from Software where version = 6).GetDescendant(@Id, null) where version = 3

declare @Id hierarchyid  
select @Id = MAX(hid) from Software where hid.GetAncestor(1) = (select hid from Software where version = 2); -- ������ 2
update Software set hid = (select hid from Software where version = 2).GetDescendant(@Id, null) where version = 4

declare @Id hierarchyid  
select @Id = MAX(hid) from Software where hid.GetAncestor(1) = (select hid from Software where version = 2); -- ������ 2
update Software set hid = (select hid from Software where version = 2).GetDescendant(@Id, null) where version = 5

select * from Software;
select hid.ToString() as hid, hid.GetLevel() as level, * from Software

-- drop procedure GetChildNodes;

create procedure GetChildNodes @version varchar(20)
as
begin
	declare @parentNodeHid hierarchyid
	select  @parentNodeHid = (select hid from Software where version = @version);
	select hid.ToString() as hid, hid.GetLevel() as level, * from Software where
	hid.GetAncestor(1) = @parentNodeHid;
end;

exec GetChildNodes 6;

create procedure AddChildNode(@parent_version varchar(20), @version varchar(20),@name varchar(50), @manufacturer varchar(50))   
AS   
BEGIN  
   DECLARE @parenthid hierarchyid, @hid hierarchyid  
   SELECT @parenthid = hid   
   FROM Software 
   WHERE version = @parent_version  
      SELECT @hid = max(hid)   
      FROM Software 
      WHERE hid.GetAncestor(1) = @parenthid ;  
      INSERT Software VALUES(@name, @manufacturer, @version, @parenthid.GetDescendant(@hid, NULL))  
END;

exec AddChildNode 6, 7, 7, 7;

exec GetChildNodes 6;

CREATE PROCEDURE MoveSubtree(@old_parent_version varchar(20), @new_parent_version varchar(20))
as
BEGIN
DECLARE @nold hierarchyid, @nnew hierarchyid  
SELECT @nold = hid FROM Software WHERE version = @old_parent_version ;  
SELECT @nnew = hid FROM Software WHERE version = @new_parent_version ;  
SELECT @nnew = @nnew.GetDescendant(max(hid), NULL) FROM Software WHERE hid.GetAncestor(1)=@nnew;  
UPDATE Software SET hid = hid.GetReparentedValue(@nold, @nnew) WHERE hid.IsDescendantOf(@nold) = 1;  
END; 

exec MoveSubtree 2, 6

select hid.ToString() as hid, hid.GetLevel() as level, * from Software

--GetAncestor � ������ hierarchyid ������, ����� ������� ������� ������, �������� 1 ������� ����������������� ������;
--GetDescendant � ������ hierarchyid �������, ��������� ��� ���������, � ������� ������� ����� ��������� ���, 
--������ ������ ������� ���������� �������� �� ������;
--GetLevel � ������ ������� hierarchyid;
--GetRoot � ������ ������� �����;
--IsDescendant � ��������� �������� �� hierarchyid ���������� ����� �������� ��������;
--Parse � ������������ ��������� ������������� hierarchyid � ���������� hierarchyid;
--Reparent � ��������� �������� �������� ������;
--ToString � ������������ hierarchyid � ��������� �������������.