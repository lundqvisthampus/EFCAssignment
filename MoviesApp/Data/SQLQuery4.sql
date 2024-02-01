DROP TABLE ProductReviews
DROP TABLE Products
DROP TABLE ProductImages
DROP TABLE Categories
DROP TABLE Users

CREATE TABLE Users
(
	Id int not null identity primary key,
	UserName nvarchar(20) not null unique,
	Email varchar(50) not null unique,
	Password nvarchar(max) not null
)

CREATE TABLE Categories
(
	Id int not null identity primary key,
	CategoryName nvarchar(50) not null unique
)

CREATE TABLE ProductImages
(
	Id int not null identity primary key,
	ImageUrl nvarchar(max) not null,
)

CREATE TABLE Products
(
	Id int not null identity primary key,
	ProductName nvarchar(50) not null unique,
	Price money not null,
	CategoryId int not null references Categories(Id),
	ProductImageId int not null references ProductImages(Id)
)

CREATE TABLE ProductReviews
(
	Id int not null identity primary key,
	Rating int not null,
	UserId int not null references Users(Id),
	ProductId int not null references Products(Id)
)