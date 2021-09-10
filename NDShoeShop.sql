CREATE DATABASE NDShoeStore;

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

INSERT INTO sizes VALUES ('1','28');
INSERT INTO sizes VALUES ('2','29');
INSERT INTO sizes VALUES ('3','30');
INSERT INTO sizes VALUES ('4','31');
INSERT INTO sizes VALUES ('5','32');
INSERT INTO sizes VALUES ('6','34');
INSERT INTO sizes VALUES ('7','35');
INSERT INTO sizes VALUES ('8','36');
INSERT INTO sizes VALUES ('9','37');
INSERT INTO sizes VALUES ('10','38');
INSERT INTO sizes VALUES ('11','39');
INSERT INTO sizes VALUES ('12','40');
INSERT INTO sizes VALUES ('13','41');
INSERT INTO sizes VALUES ('14','42');
INSERT INTO sizes VALUES ('15','43');
INSERT INTO sizes VALUES ('16','44');

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
INSERT INTO colors VALUES ('15','Other');


CREATE USER IF NOT EXISTS 'hoainam'@'localhost' identified by 'hoainam04';
grant all on ndshoestore.* to 'hoainam'@'localhost';

insert into Staffs(staff_name, user_name, user_pass, role) values
				('HoaiNam', 'hoainam', 'e78a1f1f50970cdea9956ff3c1867a2f',1);
insert into Staffs(staff_name, user_name, user_pass, role) values
				('TranDat', 'trandat', '2ea25ca22051274aa3a3240889cea233',2);
                
Select * from Staffs;

Select * from Staffs where user_name = 'hoainam'and staff_name ='HoaiNam' and user_pass='e78a1f1f50970cdea9956ff3c1867a2f';
Select staff_name from Staffs where staff_name= 'HoaiNam';

select * from Shoes where brand_name = 'van';
