CREATE DATABASE NDShoeStore;

USE NDShoeStore;

-- staffs
drop table staffs;
CREATE TABLE Staffs(
	staff_id int auto_increment primary key,
    staff_name nvarchar(50),
    user_name nvarchar(50),
    user_pass nvarchar(50),
    staff_phone nvarchar(20),
    role int not null default 1
);

-- customers
CREATE TABLE Customers(
	customer_id int auto_increment primary key,
    customer_name nvarchar(50),
    customer_phone int,
    customer_address nvarchar(50)
);

-- invoices
CREATE TABLE Invoices(
	invoice_no int auto_increment primary key,
    invoice_date datetime,
    invoice_status nvarchar(50)
);

-- sizes
CREATE TABLE Sizes(
	size_id int primary key,
    size_number int
);

-- color
CREATE TABLE Colors(
	color_id int primary key,
    color_name nvarchar(50)
);

-- Brand
CREATE TABLE Brands(
	brand_id int primary key,
    brand_name nvarchar(50)
);

-- Shoes
CREATE TABLE Shoes(
	shoe_id int auto_increment primary key,
    shoe_name nvarchar(50),
    shoe_price double,
    brand_name nvarchar(50) references Brands(brand_name),
    shoe_quanliti int,
    shoe_desception nvarchar(500)
);

-- ShoeDetails
CREATE TABLE ShoesDetails(
	shoe_id int,
    color_id int,
    size_id int,
    quantity int,
    foreign key (shoe_id) references Shoes(shoe_id),
    foreign key (color_id) references Colors(color_id),
    foreign key (size_id) references Sizes(size_id),
    primary key (shoe_id, color_id, size_id)
);

-- InvoiceDetails
CREATE TABLE InvoiceDetails(
	invoice_no int ,
    shoe_id int ,
    amount int,
    foreign key (shoe_id) references Shoes(shoe_id),
    foreign key (invoice_no) references Invoices(invoice_no),
    primary key (invoice_no, shoe_id)
);


INSERT INTO Shoes VALUES (1, 'Jordan 1', '1.000', 'Nike', '12', 'USA' );
INSERT INTO Shoes VALUES (2, 'Triple S', '100', 'Baleciaga', '100', 'USA' );
INSERT INTO Shoes VALUES (3, 'UrBas The Gang', '999', 'Vintas', '120', 'VN' );
INSERT INTO Shoes VALUES (4, 'GD limitted', '1000', 'Nike', '9', 'KR' );
INSERT INTO Shoes VALUES (5, 'Jordan 4', '1000', 'Nike', '1200', 'USA' );
INSERT INTO Shoes VALUES (6, 'Classic', '999', 'Converse', '999', 'USA' );
INSERT INTO Shoes VALUES (7, 'Old Skool', '1000', 'Van', '100', 'USA' );
INSERT INTO Shoes VALUES (8, 'JD Off White', '1299', 'Nike', '50', 'USA' );
INSERT INTO Shoes VALUES (9, 'Off White 1', '1299', 'Van', '50', 'USA' );
INSERT INTO Shoes VALUES (10, 'PG1', '1299', 'Nike', '50', 'USA' );

select shoe_name from Shoes;
CREATE USER IF NOT EXISTS 'hoainam'@'localhost' identified by 'hoainam04';
grant all on ndshoestore.* to 'hoainam'@'localhost';

insert into Staffs(staff_name, user_name, user_pass, role) values
				('HoaiNam', 'hoainam', 'e78a1f1f50970cdea9956ff3c1867a2f',1);
                
Select * from Staffs;
Select * from Staffs where user_name = 'hoainam' and user_pass='e78a1f1f50970cdea9956ff3c1867a2f';