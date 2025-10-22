create database Products_managementDB
go

create table [User](

	Userid				int primary key identity(1,1),
	username			nvarchar(100) not null,
	useremail			nvarchar(max) not null,
	userpassword		int not null,
	[Role]				NVARCHAR(50) NOT NULL CHECK ([Role] IN ('Customer', 'Admin'))

);
go

create table Cart(

	Cartid				INT PRIMARY KEY IDENTITY(1,1),
	Userid				int references [User](Userid) not null,
	TotalPrice			int not null

);

create table Product(

	Productid				INT PRIMARY KEY IDENTITY(1,1),
	Decription				nvarchar(max) not null,
	[Name]					nvarchar(max) not null,
	Price					int not null,
	InStockQuantity			int not null,

);


create table CartItem(

	CartItemid			INT PRIMARY KEY IDENTITY(1,1),
	Cartid				int references Cart(Cartid) not null,
	Productid			int references Product(Productid) not null,
	Quantity			int not null

);

INSERT INTO [User] (username, useremail, userpassword, [Role])
VALUES (N'Admin', N'admin@example.com', 123, N'Admin');

INSERT INTO [User] (username, useremail, userpassword, [Role])
VALUES (N'Customer', N'customer@example.com', 123456, N'Customer');

INSERT INTO Product (Decription, [Name], Price, InStockQuantity)
VALUES 
(N'High-quality wireless headphones', N'Wireless Headphones', 1500, 25),
(N'Ergonomic office chair with lumbar support', N'Office Chair', 3200, 10);

select * from Product