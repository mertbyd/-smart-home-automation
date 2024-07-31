create database Smart_Home_Autamation_Syst
use  Smart_Home_Autamation_Syst

create schema Home
create table Home.Home
(
id int primary key identity(1,1),
adres_id int unique 
)
alter table Home.Home
add  situation bit--e�er durum false olursa direk homela ilgili her�ey veri taban�ndan silinecek bunun i�in gerekli
alter table Home.Home
add  createDate datetime default getdate()
create table  Home.Rooms--Aksesuarlar�n konum bilgisi i�in kulan�lacak database
(
id int primary key identity(1,1),
room_name varchar(200)
)


create schema accessories--otomasyon sisteminde kulan�lacak cihazlar�n �emas�


--------------------------------------------------------------------
--Lamblar i�in gerekli sql tablolar�
create table accessories.lamb
(
id int primary key identity(1,1),
home_id int  not null,--foreign key
name varchar(50),
location_id int ,
station bit
)
-- foreign ke ba�lant�lar�
alter table accessories.lamb
add foreign key (home_id) references Home.Home(id)

alter table accessories.lamb
add foreign key (location_id) references Home.Rooms(id)
------------------------------------------------------
--Prizler i�in gerekli sql tablolar�
create table accessories.Power_Socket
(
id int primary key identity(1,1),
home_id int  not null,--foreign key
name varchar(50),
location_id int ,
station bit
)
-- foreign ke ba�lant�lar�
alter table accessories.Power_Socket
add foreign key (home_id) references Home.Home(id)

alter table accessories.Power_Socket
add foreign key (location_id) references Home.Rooms(id)
-------------------------------------------------------------------------------
--Klima i�in gerekli tablolar
create table accessories.Air_conditioning
(
id int primary key identity(1,1),
home_id int  not null,--foreign key
name varchar(50),
location_id int ,
station bit,
degree int check(degree>=16 and degree<=40),
mode int,
)
-- foreign ke ba�lant�lar�
alter table accessories.Air_conditioning
add foreign key (home_id) references Home.Home(id)

alter table accessories.Air_conditioning
add foreign key (location_id) references Home.Rooms(id)
-----------------------------------------------------------------------------
--Robot s�p�rge i�in gerekli ba�lant�lar
create table accessories.Robot_vacum_cleaner
(
id int primary key identity(1,1),
home_id int  not null,--foreign key
name varchar(50),
location_id int ,
station bit,
charge int check(charge>=0 and charge<=100),
charge_station bit,
)
-- foreign ke ba�lant�lar�
alter table accessories.Air_conditioning
add foreign key (home_id) references Home.Home(id)

alter table accessories.Air_conditioning
add foreign key (location_id) references Home.Rooms(id)
-----------------------------------------------------------------------