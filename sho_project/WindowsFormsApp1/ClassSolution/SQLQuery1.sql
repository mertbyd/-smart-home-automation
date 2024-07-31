create database Smart_Home_Autamation_Syst
use  Smart_Home_Autamation_Syst

create schema Home
create table Home.Home
(
id int primary key identity(1,1),
adres_id int unique 
)
alter table Home.Home
add  situation bit--eğer durum false olursa direk homela ilgili herşey veri tabanından silinecek bunun için gerekli
alter table Home.Home
add  createDate datetime default getdate()
create table  Home.Rooms--Aksesuarların konum bilgisi için kulanılacak database
(
id int primary key identity(1,1),
room_name varchar(200)
)


create schema accessories--otomasyon sisteminde kulanılacak cihazların şeması


--------------------------------------------------------------------
--Lamblar için gerekli sql tabloları
create table accessories.lamb
(
id int primary key identity(1,1),
home_id int  not null,--foreign key
name varchar(50),
location_id int ,
station bit
)
-- foreign ke bağlantıları
alter table accessories.lamb
add foreign key (home_id) references Home.Home(id)

alter table accessories.lamb
add foreign key (location_id) references Home.Rooms(id)
------------------------------------------------------
--Prizler için gerekli sql tabloları
create table accessories.Power_Socket
(
id int primary key identity(1,1),
home_id int  not null,--foreign key
name varchar(50),
location_id int ,
station bit
)
-- foreign ke bağlantıları
alter table accessories.Power_Socket
add foreign key (home_id) references Home.Home(id)

alter table accessories.Power_Socket
add foreign key (location_id) references Home.Rooms(id)
-------------------------------------------------------------------------------
--Klima için gerekli tablolar
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
-- foreign ke bağlantıları
alter table accessories.Air_conditioning
add foreign key (home_id) references Home.Home(id)

alter table accessories.Air_conditioning
add foreign key (location_id) references Home.Rooms(id)
-----------------------------------------------------------------------------
--Robot süpürge için gerekli bağlantılar
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
-- foreign ke bağlantıları
alter table accessories.Air_conditioning
add foreign key (home_id) references Home.Home(id)

alter table accessories.Air_conditioning
add foreign key (location_id) references Home.Rooms(id)
-----------------------------------------------------------------------