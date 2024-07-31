create database SHA_Dat
use SHA_Dat
  
create schema project

create table project.pitcures
(
id int primary key identity(1,1),
images VARBINARY(MAX)
)





create schema home
create table home.home
(
id int primary key identity(1,1)
)
alter table home.home
add adres_id int unique not null
alter table home.home
add createdate datetime default getdate()

create table home.adres
(
id int primary key identity(1,1),
home_id int not null unique,
City_Province varchar(100),
District varchar(100),
Street varchar(100),
Apartment varchar(100),
Door_Number varchar(100),
Floor varchar(100),
Adrescrea  varchar(100),
foreign key (home_id) references home.home(id)
)

create table home.location--aksesuaraların buluncacağı konumu belirlemek için kulanılır
(
id int primary key identity(1,1),
loc_name varchar(100)
)

--------------------------------- eski veri çekme-----------------------
select id,room_name  from Smart_Home_Autamation_Syst.Home.Rooms
INSERT INTO home.location (id, loc_name) 
SELECT Smart_Home_Autamation_Syst.Home.Rooms.id, Smart_Home_Autamation_Syst.Home.Rooms.room_name
FROM Smart_Home_Autamation_Syst.Home.Rooms  
--------------------------------- eski veri çekme------------------------
--

create schema accessory
--Lambalar
create table accessory.lamb
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
location_id int,
stiation bit default 1
)
---------------foreign key bağlantıları-----------------------
alter table accessory.lamb
add foreign key (home_id) references home.home(id)
alter table accessory.lamb
add foreign key (location_id) references home.location(id)
---------------------------------------------------------------
create schema accessory

create table accessory.lamb
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
location_id int,
stiation bit default 1
)
---------------foreign key bağlantıları-----------------------
alter table accessory.lamb
add foreign key (home_id) references home.home(id)
alter table accessory.lamb
add foreign key (location_id) references home.location(id)
------------------------------------------------------
--Prizler
create table accessory.power_socket
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
location_id int,
stiation bit default 1
)
alter table accessory.power_socket
add foreign key (home_id) references home.home(id)
alter table accessory.power_socket
add foreign key (location_id) references home.location(id)
---------------------------------------------------------------
--klima
create table accessory.air_conditioning
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
location_id int,
stiation bit default 1,
degree int check(degree >= 16 and degree <=100),
mode varchar(100)
)
alter table  accessory.air_conditioning
alter column mode int
---------------foreign key bağlantıları-----------------------
alter table accessory.air_conditioning
add foreign key (home_id) references home.home(id)
alter table accessory.air_conditioning
add foreign key (location_id) references home.location(id)
----------------------------------------------------------------
--Kombi 
create table accessory.Combi
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
location_id int,
stiation bit default 1,
waterdegree int check(waterdegree >= 20 and waterdegree <=75),
airdegree int check(airdegree >= 34 and airdegree <=85)
)
---------------foreign key bağlantıları-----------------------
alter table accessory.Combi
add foreign key (home_id) references home.home(id)
alter table accessory.Combi
add foreign key (location_id) references home.location(id)
---------------------------------------------------------------
--Kapılar
create table accessory.Door
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
location_id int,
stiation bit default 1,
time int default 5
)
---------------foreign key bağlantıları-----------------------
alter table accessory.Door
add foreign key (home_id) references home.home(id)
alter table accessory.Door
add foreign key (location_id) references home.location(id)
---------------------------------------------------------------
--Fırın
create table accessory.Oven
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
location_id int,
stiation bit default 1,
degree int check(degree >= 100 and degree <=300),
mode varchar(100)
)
---------------foreign key bağlantıları-----------------------
alter table accessory.Oven
add foreign key (home_id) references home.home(id)
alter table accessory.Oven
add foreign key (location_id) references home.location(id)

---------------------------------------------------------------
create schema customer
create table customer.customers
(
id int primary key identity(1,1),
home_id int ,
name varchar(100),
lastname varchar(100),
TC varchar(100),
phonenumber varchar(20),
email varchar(100),
password varchar(100),
situation bit,
createdate datetime default getdate()
)

--------------------------------------------------------------


create table Home.RoomcreateData--evin konum bilgileri
(
id int primary key identity(1,1) ,
width int,
height int,
x int,
y int,
loc_id int,
foreign key (loc_id) references home.location(id)
)
alter table   Home.RoomcreateData
add home_id int