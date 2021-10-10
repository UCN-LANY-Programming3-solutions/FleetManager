CREATE TABLE Locations(
	Id int primary key not null identity(1,1), 
	Name nvarchar(max) not null
);

CREATE TABLE Cars(
	Id int primary key not null identity(1,1),
	Brand nvarchar(max) not null, 
	Mileage int not null, 
	Reserved datetime2 (7) null, 
	LocationId int null foreign key references Locations(Id)
);
