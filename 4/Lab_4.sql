CREATE SPATIAL INDEX MySpatialIndex ON Userr(location) ;
drop  index MySpatialIndex ON Userr ;

DECLARE @searchLocation geography = geography::STPointFromText('POINT(-122.34900 47.65100)', 4326);
SELECT *
FROM Userr WITH(INDEX(MySpatialIndex))
WHERE location.STDistance(@searchLocation) < 1000;


begin
    declare @a geography
    declare @b geography
    set @a=(select location from Userr where id=13)
    set @b=(select location from Userr where id=14)
    select @a.STIntersection(@b)    -- высчитывает пересечение двух точек
    select @a.STDistance(@b)        -- высчитывает расстояние между двумя точками
    select @a.STDifference(@b)      -- вычитает одну точку из другой
    select @a.STUnion(@b)           -- объединяет две точки в одну
end;


----------------------------------- 1 ---------------------------------------
create database map;
use map;
select * from map;

go
declare @g geometry = geometry::STGeomFromText('Point(0 0)', 0);
select @g.STBuffer(5), @g.STBuffer(5).ToString() as WKT from map;


----------------------------------- 5 -------------------------------------------
BEGIN
    DECLARE @b GEOGRAPHY = GEOGRAPHY::STPointFromText('POINT(-122.34900 47.65100)', 4326);
    DECLARE @a GEOGRAPHY = GEOGRAPHY::STPointFromText('POINT(-123.34900 47.65100)', 4326);
    DECLARE @refined GEOGRAPHY = @a.STUnion(@b);
    SELECT @refined;
END;