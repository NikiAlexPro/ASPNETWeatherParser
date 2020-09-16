
create database weatherdb;
use weatherdb;

drop table Region;
drop table City;
drop table WeatherForecast;


delete from WeatherForecast
where Id > 0;

create table Region(
Id int primary key auto_increment not null,
RegionName varchar(60) not null,
RegionLink varchar(50) not null
);

create table City(
Id int primary key auto_increment not null,
RegionId int not null,
CityName varchar(150),
CityLink varchar(150),
foreign key (RegionId) references Region (Id)
);

create table WeatherForecast (
Id int primary key auto_increment not null,
CityId int not null,
WeatherDate date not null,
TempDay varchar(30) not null,
TempNight varchar(30) not null,
Pressure varchar(30) not null,
AirHumidity varchar(30) not null,
WindDirection varchar(30) not null,
WeatherIconLink varchar(150) not null,
foreign key (CityId) references City (Id)
);





select * from Region;
select * from City;
select * from WeatherForecast;

select CityName from City
join WeatherForecast on CityId = City.Id;

select Id, RegionId, CityName, CityLink  from City where RegionId = (select Id from Region where RegionName = 'Брянская область') and CityName = 'Брянск';

select CityName, WeatherDate, TempDay, TempNight, WindDirection, WeatherForecast.Id as WatherForId, City.Id as CityId from City
join WeatherForecast on CityId = City.Id
join Region on RegionId = Region.Id
where CityName = 'Брянск' and RegionName = 'Брянская область';