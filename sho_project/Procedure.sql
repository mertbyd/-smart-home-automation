--Procedureler

alter procedure controlCustomer--customer bilgisini kontrol eden procedur
(
@email varchar(100),
@password varchar(100)
)
as
begin
select id from customer.customers where email=@email and @password =@password
end

alter procedure getCustomer --Customer bilgisini kontrol eden procedure
(
@id int
)
as 
begin 
select id,home_id,name,lastname,TC,phonenumber,email,password,situation,createdate
from customer.customers as cs where id=@id
end

create procedure getpitcures
(
@id int
)
as
begin
select id,images from project.pitcures where id=@id
end








--Iþýklar
create proc getlambs--lamba verilerini getiren proc
(
@home_id int
)
as
begin
select * from  accessory.lamb as lb
inner join  home.location as lc
on lb.location_id=  lc.id
where  lb.home_id=@home_id
end
--

	

--

--Prizler----------------------------------------------------------------------------------------------------------
alter proc getPowersock--Priz verilerini getiren proc
(
@home_id int
)
as
begin
select * from  accessory.power_socket as lb
inner join  home.location as lc
on lb.location_id=  lc.id
where  lb.home_id=@home_id
end


alter proc onof_powersoc--	Priz açýp kapatma
(
@sit bit,
@id int
)
as
begin
Update   accessory.power_socket  
set    accessory.power_socket.stiation=@sit 
where id=@id
end
------------------------------------------------------------------------------------

--Kombi----------------------------------------------------------------------
alter proc getCombi--Combi verilerini getiren proc
(
@home_id int
)
as
begin
select * from  accessory.Combi as com
where com.home_id =@home_id
end


alter proc updateDegree --derece deðiþtirmek için kulancaðýmýz yapý
(
@id int,
@airdepree int,
@waterdegree int
)
as
begin
Declare @dbwaterdegree int-- atama iþlemi
select @dbwaterdegree=waterdegree from accessory.Combi where id=@id

Declare @dbairdegree int -- atama iþlemi
select @dbairdegree=airdegree from accessory.Combi where id=@id

Declare @sit int -- atama iþlemi
select @sit=stiation from accessory.Combi where id=@id
if  @sit=1
begin
	if  @dbairdegree != @airdepree
		begin
		update accessory.Combi set airdegree=@airdepree where id=@id
		end
	 if  @dbwaterdegree != @waterdegree
		begin
		update accessory.Combi set waterdegree=@waterdegree where id=@id
		end
end
end


alter proc onof_comb--	combi açýp kapatma
(
@sit bit,
@lamb_id int 
)
as
begin
Update   accessory.Combi  
set    accessory.Combi.stiation=@sit 
where id=@lamb_id
end
----------------------------------------------------------------------------------------------------------------------------------------------------------------


---Fýrýn-----------
alter proc getOver--lamba verilerini getiren proc
(
@home_id int
)
as
begin
select *from  accessory.Oven as  ov
inner join  home.location as lc
on ov.location_id=  lc.id
where  ov.home_id=@home_id
end


create proc  ISworks--fýrýnýn çalýþ çalýþmadýðýný kontrol eder
(
@over_id int
)
as
begin
select works from accessory.Oven where id=@over_id
end


alter proc workOver --fýrýný çalýþtýrma
(
@over_id int,
@time int,
@degree int,
@mode int
)
as
begin
Declare @works bit
select @works=works from accessory.Oven where id=@over_id
if  @works=0
begin
	update accessory.Oven set accessory.Oven.degree =@degree where id=@over_id
	update accessory.Oven set accessory.Oven.mode =@mode where id=@over_id
	DECLARE @StartTime DATETIME = GETDATE();
	DECLARE @CountdownMinutes INT = @time;
	DECLARE @EndTime DATETIME = DATEADD(MINUTE, @CountdownMinutes, @StartTime);
	WHILE GETDATE() < @EndTime
	begin
		 update accessory.Oven set accessory.Oven.works =1 where id=@over_id
	end
		update accessory.Oven set accessory.Oven.works =0 where id=@over_id
	end
end

create proc againstart -- fýrýn ý yenden baþlatýlmak istndiðinde fýrýný yeniden çalýþtýrmak için kulannýnlýr
(
@over_id int
)
as
begin
update accessory.Oven set works=0 where id=@over_id
end


--Klþma-----------------------------------------------
alter proc getAC--klima verilerini getiren proc
(
@home_id int
)
as
begin
select *from  accessory.air_conditioning as  ac
inner join  home.location as lc
on ac.location_id=  lc.id
where  ac.home_id=@home_id
end



alter proc updateDegreeMode --klima derece-mod deðiþtirmek için kulancaðýmýz yapý
(
@id int,
@degree int,
@mode int
)
as
begin
Declare @dbmode int-- atama iþlemi
select @dbmode=mode from accessory.air_conditioning where id=@id

Declare @dbairdegree int -- atama iþlemi
select @dbairdegree=degree from accessory.air_conditioning where id=@id

Declare @sit int -- atama iþlemi
select @sit=stiation from accessory.air_conditioning where id=@id
if  @sit=1
begin
	if  @dbairdegree != @degree
		begin
		update accessory.air_conditioning set degree=@degree where id=@id
		end
	if  @dbmode != @mode
		begin
		update accessory.air_conditioning set mode=@mode where id=@id
		end
end
end


create proc onof_ac--	klima açýp kapatma
(
@sit bit,
@ac_id int 
)
as
begin
Update   accessory.air_conditioning  
set    accessory.air_conditioning.stiation=@sit 
where id=@ac_id
end
--------------------------------------------------------------------------------------------------------
create proc getAllHouseAccsLoc --evin hangi odalarýnda eþya olduðunu belirleyen metot
(
@home_id int
)
as
begin
--ayný sütünlara sahip tablolarýn o sütünlarýný alt alta yazarken kulanýlabilecek metod
SELECT location_id FROM accessory.lamb where lamb.home_id=@home_id
UNION
SELECT location_id FROM accessory.Combi where Combi.home_id=@home_id
UNION
SELECT location_id FROM accessory.air_conditioning where air_conditioning.home_id=@home_id
UNION
SELECT location_id FROM accessory.Oven where Oven.home_id=@home_id
UNION
SELECT location_id FROM accessory.power_socket where power_socket.home_id=@home_id
end

----------------------------------------------------------------------------
SELECT location_id FROM accessory.lamb where lamb.home_id='1'
UNION
SELECT location_id FROM accessory.Combi where Combi.home_id='1'
UNION
SELECT location_id FROM accessory.air_conditioning where air_conditioning.home_id='1'
UNION
SELECT location_id FROM accessory.Oven where Oven.home_id='1'
UNION
SELECT location_id FROM accessory.power_socket where power_socket.home_id='1'
--------------------------------------------------------------------------------

create proc getLocdata --servise konum verilerini göndercek proc
(
@home_id int
)
as
begin
select *  from home.RoomcreateData as rd
inner join  home.location on rd.loc_id=home.location.id
where rd.home_id  = @home_id
end


