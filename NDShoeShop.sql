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

-- Shoes
CREATE TABLE Shoes(
	shoe_id int auto_increment primary key,
    shoe_name nvarchar(50),
    shoe_price double,
    brand_name nvarchar(50),
    shoe_desception nvarchar(500)
);

-- ShoeDetails
CREATE TABLE ShoesDetails(
	shoe_id int,
    color_id int,
    size_id int,
    quantity int,
    constraint fk_ShoesDetails_Shoes foreign key (shoe_id) references Shoes(shoe_id),
    constraint fk_ShoesDetails_Colors foreign key (color_id) references Colors(color_id),
    constraint fk_ShoesDetails_Sizes foreign key (size_id) references Sizes(size_id),
    constraint pk_ShoesDetails primary key (shoe_id, color_id, size_id)
);

-- invoices
CREATE TABLE Invoices(
	invoice_no int auto_increment primary key,
    invoice_date datetime default now() not null,
    invoice_status int,
    customer_id int,
    staff_id int
);

-- InvoiceDetails
CREATE TABLE InvoiceDetails(
	invoice_no int ,
    shoe_id int ,
    amount int,
    price double,
    constraint fk_InvoiceDetails_Shoes foreign key (shoe_id) references Shoes(shoe_id),
    constraint fk_InvoiceDetails_Invoices foreign key (invoice_no) references Invoices(invoice_no),
    constraint pk_InvoiceDetails primary key (invoice_no, shoe_id)
);

-- triger
delimiter $$
create trigger tg_before_insert before insert
	on Shoes for each row
    begin
		if new.shoe_id < 0 then
            signal sqlstate '45001' set message_text = 'tg_before_insert: id must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create procedure sp_createCustomer(IN customerName varchar(100), IN customerPhone int, IN customerAddress varchar(200), OUT customerId int)
begin
	insert into Customers(customer_name, customer_phone, customer_address) values (customerName, customerPhone, customerAddress); 
    select max(customer_id) into customerId from Customers;
end $$
delimiter ;

call sp_createCustomer('no name','57','any where', @cusId);
select @cusId;

/* Insert data */

insert into Customers(customer_name,customer_phone, customer_address) values
	('Nguyen Thi X','12345678','Hai Duong'),
    ('Nguyen Van N','987654','Hanoi'),
    ('Nguyen Van B','365774587','Ho Chi Minh'),
    ('Nguyen Van A','46847485','Hanoi');

INSERT INTO Shoes(shoe_name,shoe_price,brand_name,shoe_desception) VALUES 
('Superstar', '2490000', 'Adidas', 'USA' ),
('ZX 1K Boost Pride', '599000','Adidas','USA');
INSERT INTO Shoes(shoe_name,shoe_price,brand_name,shoe_desception) VALUES 
( 'Jordan 1', '1000000', 'Nike', 'USA' ),
( 'Triple S', '599000', 'Baleciaga', 'USA' ),
('UrBas The Gang', '999000', 'Vintas', 'VN' ),
('GD limitted', '15990000', 'Nike', 'KR' ),
('Jordan 4', '890000', 'Nike',  'USA' ),
('Classic', '590000', 'Converse', 'USA' ),
('Old Skool', '490000', 'Van',  'USA' ),
('JD Off White', '1299000', 'Nike',  'USA' ),
('Off White 1', '1299000', 'Van',  'USA' ),
('PG 1', '1299000', 'Nike', 'USA' );

INSERT INTO sizes VALUES ('1','35');
INSERT INTO sizes VALUES ('2','36');
INSERT INTO sizes VALUES ('3','37');
INSERT INTO sizes VALUES ('4','38');
INSERT INTO sizes VALUES ('5','39');
INSERT INTO sizes VALUES ('6','40');
INSERT INTO sizes VALUES ('7','41');
INSERT INTO sizes VALUES ('8','42');
INSERT INTO sizes VALUES ('9','43');
INSERT INTO sizes VALUES ('10','44');

INSERT INTO colors VALUES ('1','red');
INSERT INTO colors VALUES ('2','blue');
INSERT INTO colors VALUES ('3','black');
INSERT INTO colors VALUES ('4','yellow');
INSERT INTO colors VALUES ('5','pink');
INSERT INTO colors VALUES ('6','white');
INSERT INTO colors VALUES ('7','purple');
INSERT INTO colors VALUES ('8','gray');
INSERT INTO colors VALUES ('9','brown');
INSERT INTO colors VALUES ('10','black and white');
INSERT INTO colors VALUES ('11','red and white');
INSERT INTO colors VALUES ('12','blue and white');
INSERT INTO colors VALUES ('13','white and gray');
INSERT INTO colors VALUES ('14','special');


CREATE USER IF NOT EXISTS 'hoainam'@'localhost' identified by 'hoainam04';
grant all on ndshoestore.* to 'hoainam'@'localhost';

insert into Staffs(staff_name, user_name, user_pass) values
				('HoaiNam', 'hoainam', 'e78a1f1f50970cdea9956ff3c1867a2f');
insert into Staffs(staff_name, user_name, user_pass) values
				('TranDat', 'trandat', '2ea25ca22051274aa3a3240889cea233');
                
Select * from Staffs;
delete  from Staffs where staff_id = '1';

Select * from Staffs where user_name = 'hoainam'and staff_name ='HoaiNam' and user_pass='e78a1f1f50970cdea9956ff3c1867a2f';
Select staff_name from Staffs where staff_name= 'HoaiNam';

select * from Shoes where shoe_name like '%n%';
select * from Shoes;

select shoe_id, shoe_name, shoe_price, brand_name, ifnull(shoe_desception, '') as shoe_desception from Shoes where shoe_id = '3';

select shoe_id, shoe_name, shoe_price, brand_name, ifnull(shoe_desception, '') as shoe_desception from Shoes where shoe_name Like '%e%';

select shoe_id, shoe_name, shoe_price, brand_name, ifnull(shoe_desception, '') as shoe_desception from Shoes where brand_name LIKE '%e%';