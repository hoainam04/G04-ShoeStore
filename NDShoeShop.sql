CREATE DATABASE NDShoeStore;
drop database ndshoestore;

USE NDShoeStore;

-- staffs

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
    customer_id int,
    invoice_date datetime default now() not null,
    invoice_status nvarchar(50),
    constraint fk_Invoices_Customers foreign key(customer_id) references Customers(customer_id)
);

-- sizes
CREATE TABLE Sizes(
	size_id int auto_increment primary key,
    size_number int
);

-- color
CREATE TABLE Colors(
	color_id int auto_increment primary key,
    color_name nvarchar(50)
);

-- Shoes
CREATE TABLE Shoes(
	shoe_id int auto_increment primary key,
    shoe_name nvarchar(50),
    shoe_price double,
    brand_name nvarchar(50),
    shoe_quantity int,
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
    shoe_price double,
    foreign key (shoe_id) references Shoes(shoe_id),
    foreign key (invoice_no) references Invoices(invoice_no),
    primary key (invoice_no, shoe_id)
);

delimiter $$
create procedure sp_createCustomer(IN customerName varchar(50),IN customerPhone int ,IN customerAddress varchar(50), OUT customerId int)
begin
	insert into Customers(customer_name,customer_phone,customer_address) values (customerName,customerPhone ,customerAddress); 
    select max(customer_id) into customerId from Customers;
end $$
delimiter ;
call sp_createCustomer('Bill Gr','843234324','TPHCM',@cusId);
select @cusId;

INSERT INTO Shoes(shoe_name,shoe_price,brand_name,shoe_quantity,shoe_desception) VALUES 
('Jordan 1', '799000', 'Nike', '999', 'USA' ),('Triple S', '999000', 'Baleciaga', '999', 'USA' ),
('UrBas The Gang', '399000', 'Vintas', '999', 'VN' ),('GD limitted', '1990000', 'Nike', '9', 'KR' ),
('Jordan 4', '699000', 'Nike', '200', 'USA' ),('Classic', '799000', 'Converse', '999', 'USA' ),
('Old Skool', '599000', 'Van', '100', 'USA' ),('JD Off White', '1299000', 'Nike', '150', 'USA' ),
('Off White 1', '1299000', 'Van', '150', 'USA' ),('PG 1', '1299000', 'Nike', '150', 'USA' ),
('Saigon 1980s NE','490000','Vintas','500','VN'),('Super Star','490000','Adidas','200','JP'),
('Ultra Boost 20B','2050000','Adidas','321','JP'),('EQT 91.18','890000','Adidas','213','JP'),('Bumper Gum Mule','490000','Basas','500','VN');

INSERT INTO sizes(size_number) VALUES 
('31'),('32'),('33'),('34'),('35'),('36'),('37'),('38'),('39'),('40'),('41'),('42'),('43'),('44');

INSERT INTO colors(color_name) VALUES ('red'),('blue'),('black'),
('yellow'),('pink'),('white'),('purple'),('gray'),('brown'),('black and white'),('red and white'),('blue and white'),('white and gray'),('special');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd1 black and white  
('1','10','6','27'),('1','10','7','27'),('1','10','8','27'),('1','10','9','27'),
('1','10','10','27'),('1','10','11','60'),('1','10','12','27'),('1','10','13','27'),('1','10','14','28'),
-- jd1 red abd white
('1','11','6','27'),('1','11','7','27'),('1','11','8','27'),('1','11','9','27'),
('1','11','10','27'),('1','11','11','60'),('1','11','12','27'),('1','11','13','27'),('1','11','14','28'),
-- jd1 blue  and white
('1','12','6','27'),('1','12','7','27'),('1','12','8','27'),('1','12','9','27'),
('1','12','10','27'),('1','12','11','60'),('1','12','12','27'),('1','12','13','27'),('1','12','14','28');

select shoe_name,color_name,size_number,quantity
from shoes,ShoesDetails,Sizes,Colors where Shoes.shoe_id = ShoesDetails.shoe_id
 and Sizes.size_id= ShoesDetails.size_id
 and Colors.color_id= ShoesDetails.color_id;

insert into customers(customer_name,customer_phone,customer_address) values('Phung Thanh Do','0962358243','Cao Bang');

CREATE USER IF NOT EXISTS 'hoainam'@'localhost' identified by 'hoainam04';
grant all on ndshoestore.* to 'hoainam'@'localhost';

insert into Staffs(staff_name, user_name, user_pass) values
				('Hoai Nam', 'hoainam', 'e78a1f1f50970cdea9956ff3c1867a2f');
insert into Staffs(staff_name, user_name, user_pass) values
				('Tran Dat', 'trandat', '2ea25ca22051274aa3a3240889cea233');
                
Select * from Staffs;

Select * from Staffs where user_name = 'hoainam'and staff_name ='HoaiNam' and user_pass='e78a1f1f50970cdea9956ff3c1867a2f';
Select staff_name from Staffs where staff_name= 'HoaiNam';

select * from Shoes where brand_name = 'van';
