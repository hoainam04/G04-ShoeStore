CREATE DATABASE NDShoeStore;
drop database NDShoeStore;
USE NDShoeStore;

-- staffs

CREATE TABLE Staffs(
	staff_id int auto_increment primary key,
    staff_name varchar(50),
    user_name varchar(50),
    user_pass varchar(50),
    staff_phone varchar(20),
    role int not null default 1
);

-- customers
CREATE TABLE Customers(
	customer_id int auto_increment primary key,
    customer_name varchar(50),
    customer_phone varchar(20),
    customer_address varchar(50)
);

-- invoices
CREATE TABLE Invoices(
	invoice_no int auto_increment primary key,
    customer_id int,
    invoice_date datetime default now() not null,
    invoice_status int,
    staff_id int,
    constraint fk_Invoices_Staffs foreign key(staff_id) references Staffs(staff_id),
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
    color_name varchar(50)
);

-- Shoes
CREATE TABLE Shoes(
	shoe_id int auto_increment primary key,
    shoe_name varchar(50),
    shoe_price double,
    brand_name varchar(50),
    shoe_quantity int,
    shoe_desception varchar(500)
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
    color_id int,
    size_id int,
    amount int,
    total_price double,
    total double,
    foreign key (color_id) references colors(color_id),
    foreign key (size_id) references sizes(size_id),
    foreign key (shoe_id) references Shoes(shoe_id),
    foreign key (invoice_no) references Invoices(invoice_no),
    primary key (invoice_no, shoe_id)
);

delimiter $$
create trigger tg_before_insert before insert
	on Shoes for each row
    begin
		if new.shoe_quantity < 0 then
            signal sqlstate '45001' set message_text = 'tg_before_insert: quantity must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create trigger tg_CheckQuantity
	before update on Shoes
	for each row
	begin
		if new.shoe_quantity < 0 then
            signal sqlstate '45001' set message_text = 'tg_CheckAmount: quantity must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create procedure sp_createCustomer(IN customerName varchar(50),IN customerPhone int ,IN customerAddress varchar(50), OUT customerId int)
begin
	insert into Customers(customer_name,customer_phone,customer_address) values (customerName,customerPhone ,customerAddress); 
    select max(customer_id) into customerId from Customers;
end $$
delimiter ;
-- call sp_createCustomer('Bill Gr','843234324','TP.HCM',@cusId);
-- select @cusId;

INSERT INTO Shoes(shoe_name,shoe_price,brand_name,shoe_quantity,shoe_desception) VALUES 
('Jordan 1', '799000', 'Nike', '999', 'USA' ),('Triple S', '999000', 'Baleciaga', '999', 'USA' ),
('UrBas The Gang', '399000', 'Vintas', '999', 'VN' ),('GD limitted', '1990000', 'Nike', '9', 'KR' ),
('Jordan 4', '699000', 'Nike', '200', 'USA' ),('Classic', '799000', 'Converse', '999', 'USA' ),
('Old Skool', '599000', 'Van', '100', 'USA' ),('JD Off White', '1299000', 'Nike', '150', 'USA' ),
('Off White 1', '1299000', 'Van', '150', 'USA' ),('PG 1', '1299000', 'Nike', '150', 'USA' ),
('Saigon 1980s NE','490000','Vintas','500','VN'),('Super Star','490000','Adidas','200','JP'),
('Ultra Boost 20B','2050000','Adidas','321','JP'),('EQT 91.18','890000','Adidas','213','JP'),
('Bumper Gum Mule','490000','Basas','500','VN'),('Xsport NY F1 Mule','250000','MLB','500','KR'),
('Bata','100000','ThuongDinh','1000','VN'),('StreetBall Master','890000','Peak','0','USA')
('TaiChi','1299000','Peak','0','CN'),('RS Pure Vision','1099000','Puma','0','Germany');

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

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- triples black
('2','3','6','27'),('2','3','7','27'),('2','3','8','27'),('2','3','9','27'),('2','3','10','27'),
('2','3','11','60'),('2','3','12','27'),('2','3','13','27'),('2','3','14','27'),
-- white
('2','6','6','27'),('2','6','7','27'),('2','6','8','27'),('2','6','9','27'),('2','6','10','27'),('2','6','11','60'),
('2','6','12','27'),('2','6','13','27'),('2','6','14','27'),
-- black and white
('2','10','6','27'),('2','10','7','27'),('2','10','8','27'),('2','10','9','27'),
('2','10','10','27'),('2','10','11','27'),('2','10','12','27'),('2','10','13','27'),('2','10','14','27');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- thegangs black
('3','3','6','27'),('3','3','7','27'),('3','3','8','27'),('3','3','9','27'),('3','3','10','27'),
('3','3','11','60'),('3','3','12','27'),('3','3','13','27'),('3','3','14','27'),
-- white
('3','6','6','27'),('3','6','7','27'),('3','6','8','27'),('3','6','9','27'),('3','6','10','27'),('3','6','11','60'),
('3','6','12','27'),('3','6','13','27'),('3','6','14','27');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- GDlm black
('4','14','9','1'),('4','14','10','2'),('4','14','11','3'),('4','14','12','2'),('4','14','13','1');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('5','3','6','10'),('5','3','7','10'),('5','3','8','10'),('5','3','9','10'),('5','3','10','10'),
('5','3','11','30'),('5','3','12','10'),('5','3','13','10'),('5','3','14','10'),
-- white
('5','6','6','5'),('5','6','7','5'),('5','6','8','5'),('5','6','9','5'),('5','6','10','5'),('5','6','11','5'),
('5','6','12','15'),('5','6','13','5'),('5','6','14','5'),
-- brown
('5','9','6','5'),('5','9','7','5'),('5','9','8','5'),('5','9','9','5'),('5','9','10','5'),('5','9','11','5'),
('5','9','12','15'),('5','9','13','5'),('5','9','14','5');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd1 black and white  
('6','10','6','27'),('6','10','7','27'),('6','10','8','27'),('6','10','9','27'),
('6','10','10','27'),('6','10','11','60'),('6','10','12','27'),('6','10','13','27'),('6','10','14','28'),
-- jd1 red abd white
('6','11','6','27'),('6','11','7','27'),('6','11','8','27'),('6','11','9','27'),
('6','11','10','27'),('6','11','11','60'),('6','11','12','27'),('6','11','13','27'),('6','11','14','28'),
-- jd1 blue  and white
('6','12','6','27'),('6','12','7','27'),('6','12','8','27'),('6','12','9','27'),
('6','12','10','27'),('6','12','11','60'),('6','12','12','27'),('6','12','13','27'),('6','12','14','28');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd1 black and white  
('7','10','6','12'),('7','10','7','12'),('7','10','8','12'),('7','10','9','12'),
('7','10','10','12'),('7','10','11','14'),('7','10','12','12'),('7','10','13','12'),('7','10','14','12');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd1 black and white  
('8','10','6','6'),('8','10','7','6'),('8','10','8','6'),('8','10','9','6'),
('8','10','10','6'),('8','10','11','6'),('8','10','12','27'),('8','10','13','6'),('8','10','14','6'),
-- jd1 red abd white
('8','11','6','6'),('8','11','7','6'),('8','11','8','6'),('8','11','9','6'),
('8','11','10','6'),('8','11','11','8'),('8','11','12','6'),('8','11','13','6'),('8','11','14','6'),
-- jd1 blue  and white
('8','12','6','6'),('8','12','7','6'),('8','12','8','6'),('8','12','9','6'),
('8','12','10','6'),('8','12','11','8'),('8','12','12','6'),('8','12','13','6'),('8','12','14','6');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd1 black and white  
('9','10','6','16'),('9','10','7','16'),('9','10','8','16'),('9','10','9','16'),
('9','10','10','16'),('9','10','11','29'),('9','10','12','25'),('9','10','13','16'),('9','10','14','16');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- pg1 black and white  
('10','14','5','15'),('10','14','6','15'),('10','14','7','15'),('10','14','8','15'),('10','14','9','15'),
('10','14','10','15'),('10','14','11','15'),('10','14','12','15'),('10','14','13','16'),('10','14','14','15');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('11','3','5','15'),('11','3','6','15'),('11','3','7','15'),('11','3','8','15'),('11','3','9','15'),('11','3','10','15'),
('11','3','11','15'),('11','3','12','15'),('11','3','13','15'),('11','3','14','15'),
-- white
('11','6','5','15'),('11','6','6','15'),('11','6','7','15'),('11','6','8','15'),('11','6','9','15'),('11','6','10','15'),('11','6','11','15'),
('11','6','12','15'),('11','6','13','15'),('11','6','14','15'),
-- brown
('11','9','5','10'),('11','9','6','10'),('11','9','7','10'),('11','9','8','10'),('11','9','9','10'),('11','9','10','10'),('11','9','11','10'),
('11','9','12','10'),('11','9','13','10'),('11','9','14','10'),
-- gray
('11','8','5','10'),('11','8','6','10'),('11','8','7','10'),('11','8','8','10'),('11','8','9','10'),('11','8','10','10'),('11','8','11','10'),
('11','8','12','10'),('11','8','13','10'),('11','8','14','10');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- white
('12','6','5','20'),('12','6','6','20'),('12','6','7','20'),('12','6','8','20'),('12','6','9','20'),('12','6','10','20'),('12','6','11','20'),
('12','6','12','20'),('12','6','13','20'),('12','6','14','20');



Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd1 black and white  
('13','10','6','10'),('13','10','7','10'),('13','10','8','10'),('13','10','9','10'),
('13','10','10','10'),('13','10','11','17'),('13','10','12','10'),('13','10','13','6'),('13','10','14','10'),
-- jd1 red abd white
('13','11','6','10'),('13','11','7','10'),('13','11','8','10'),('13','11','9','10'),
('13','11','10','10'),('13','11','11','17'),('13','11','12','10'),('13','11','13','10'),('13','11','14','10'),
-- jd1 blue  and white
('13','12','6','10'),('13','12','7','10'),('13','12','8','10'),('13','12','9','6'),
('13','12','10','10'),('13','12','11','17'),('13','12','12','10'),('13','12','13','6'),('13','12','14','10');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd1 black and white  
('14','10','6','10'),('14','10','7','10'),('14','10','8','10'),('14','10','9','10'),
('14','10','10','10'),('14','10','11','17'),('14','10','12','10'),('14','10','13','6'),('14','10','14','10'),
-- jd1 red abd white
('14','11','6','10'),('14','11','7','10'),('14','11','8','10'),('14','11','9','10'),
('14','11','10','10'),('14','11','11','17'),('14','11','12','10'),('14','11','13','10'),('14','11','14','10'),
-- jd1 blue  and white
('14','12','6','10'),('14','12','7','10'),('14','12','8','10'),('14','12','9','6'),
('14','12','10','10'),('14','12','11','17'),('14','12','12','10'),('14','12','13','6'),('14','12','14','10');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('15','3','5','25'),('15','3','6','25'),('15','3','7','25'),('15','3','8','25'),
('15','3','9','25'),('15','3','10','25'),
('15','3','11','25'),('15','3','12','25'),('15','3','13','25'),('15','3','14','25'),
-- white
('15','6','5','25'),('15','6','6','25'),('15','6','7','25'),('15','6','8','25'),
('15','6','9','25'),('15','6','10','25'),('15','6','11','25'),
('15','6','12','25'),('15','6','13','25'),('15','6','14','25');

Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('16','3','5','25'),('16','3','6','25'),('16','3','7','25'),('16','3','8','25'),
('16','3','9','25'),('16','3','10','25'),
('16','3','11','25'),('16','3','12','25'),('16','3','13','25'),('16','3','14','25'),
-- white
('16','6','5','25'),('16','6','6','25'),('16','6','7','25'),('16','6','8','25'),
('16','6','9','25'),('16','6','10','25'),('16','6','11','25'),
('16','6','12','25'),('16','6','13','25'),('16','6','14','25');


Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('17','3','5','50'),('17','3','6','50'),('17','3','7','50'),('17','3','8','50'),
('17','3','9','50'),('17','3','10','50'),
('17','3','11','50'),('17','3','12','50'),('17','3','13','50'),('17','3','14','50'),
-- white
('17','6','5','50'),('17','6','6','50'),('17','6','7','50'),('17','6','8','50'),
('17','6','9','50'),('17','6','10','50'),('17','6','11','50'),
('17','6','12','50'),('17','6','13','50'),('17','6','14','50');


Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('18','3','5','15'),('18','3','6','15'),('18','3','7','15'),('18','3','8','15'),
('18','3','9','15'),('18','3','10','15'),
('18','3','11','15'),('18','3','12','15'),('18','3','13','15'),('18','3','14','15'),
-- white
('18','6','5','15'),('18','6','6','15'),('18','6','7','15'),('18','6','8','15'),
('18','6','9','15'),('18','6','10','15'),('18','6','11','15'),
('18','6','12','15'),('18','6','13','15'),('18','6','14','15');


Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('19','3','5','10'),('19','3','6','10'),('19','3','7','10'),('19','3','8','10'),
('19','3','9','10'),('19','3','10','10'),
('19','3','11','10'),('19','3','12','10'),('19','3','13','10'),('19','3','14','10'),
-- white10
('19','6','5','10'),('19','6','6','10'),('19','6','7','10'),('19','6','8','10'),
('19','6','9','10'),('19','6','10','10'),('19','6','11','10'),
('19','6','12','10'),('19','6','13','10'),('19','6','14','10');


Insert into ShoesDetails(shoe_id,color_id,size_id,quantity) Values
-- jd4 black
('20','3','5','5'),('20','3','6','5'),('20','3','7','5'),('20','3','8','5'),
('20','3','9','5'),('20','3','10','5'),
('20','3','11','5'),('20','3','12','5'),('20','3','13','5'),('20','3','14','5'),
-- white
('20','6','5','5'),('20','6','6','5'),('20','6','7','5'),('20','6','8','5'),
('20','6','9','5'),('20','6','10','5'),('20','6','11','5'),
('20','6','12','5'),('20','6','13','5'),('20','6','14','5');

update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 1;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 1;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 2;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 3;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 4;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 5;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 6;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 7;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 8;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 9;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 10;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 11;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 12;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 13;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 14;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 15;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 16;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 17;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 18;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 19;
update shoes set shoes.shoe_quantity= (select sum(shoesdetails.quantity) from shoesdetails where shoesdetails.shoe_id=shoes.shoe_id) where shoe_id = 20;

insert into customers(customer_name,customer_phone,customer_address) values('Phung Thanh Do','0962358243','Cao Bang');

insert into Staffs(staff_name, user_name, user_pass) values
				('Hoai Nam', 'hoainam', 'e78a1f1f50970cdea9956ff3c1867a2f');
insert into Staffs(staff_name, user_name, user_pass) values
				('Tran Dat', 'trandat', '2ea25ca22051274aa3a3240889cea233');
                
CREATE USER IF NOT EXISTS 'hoainam'@'localhost' identified by 'hoainam04';
grant all on ndshoestore.* to 'hoainam'@'localhost';


select* from Shoes;

select shoes.shoe_id,shoe_name,shoe_price,brand_name,colors.color_id, color_name,Sizes.size_id,size_number,quantity from shoesdetails inner join Colors 
on colors.color_id = shoesdetails.color_id inner join Sizes 
on sizes.size_id =shoesdetails.size_id
inner join shoes  on shoes.shoe_id = shoesdetails.shoe_id where shoes.shoe_id=4;

SELECT DISTINCT colors.color_id,color_name FROM shoesdetails 
inner join Colors on colors.color_id = shoesdetails.color_id 
inner join shoes on shoes.shoe_id =shoesdetails.shoe_id where shoes.shoe_id=1;

select Sizes.size_id,size_number,quantity from shoesdetails inner join Colors 
on colors.color_id = shoesdetails.color_id inner join Sizes 
on sizes.size_id =shoesdetails.size_id
inner join shoes  on shoes.shoe_id = shoesdetails.shoe_id where shoes.shoe_id=9 and Colors.color_id = 10;


lock tables Staffs write,Invoices write, Customers write, Shoes write, sizes write,  colors write, invoicedetails write, shoesdetails write;
unlock tables;

select customer_id from Customers order by customer_id desc limit 1;

select invoices.invoice_no,customer_name,shoe_name,shoe_price,amount,total_price from invoicedetails inner join invoices 
on invoicedetails.invoice_no = invoices.invoice_no inner join
customers on customers.customer_id = invoices.customer_id
inner join shoes on shoes.shoe_id = invoicedetails.shoe_id
where invoices.invoice_no =1;

select invoices.invoice_no,invoice_date,customer_name,shoe_name,shoe_price,
color_name,size_number,amount,total_price
from invoicedetails inner join invoices 
on invoicedetails.invoice_no = invoices.invoice_no inner join
customers on customers.customer_id = invoices.customer_id
inner join shoes on shoes.shoe_id = invoicedetails.shoe_id
inner join colors on colors.color_id = invoicedetails.color_id
inner join sizes on sizes.size_id = invoicedetails.size_id;
where invoices.invoice_no =16;

SELECT SUM(total_price) as total FROM invoicedetails where invoice_no = 1;
update invoicedetails set total= sum(total_price) where invoice_no = 16;

select shoe_id,color_id,size_id from shoesdetails where shoe_id=1 and color_id=11 and size_id=11;


update Shoes set shoe_quantity=shoe_quantity-3 where shoe_id=1;
update shoesdetails set quantity = quantity - 3 where shoe_id =1 and color_id =10 and size_id = 11;
update invoicedetails inner join shoes on shoes.shoe_id = invoicedetails.shoe_id set total_price = shoes.shoe_price  * amount where invoice_no = 1 ;

update invoicedetails set total_price = 0 where invoice_no = 1;

                
select customer_id, customer_name,customer_phone,ifnull(customer_address, '') as customer_address from Customers where customer_phone=3;
Select * from customers;

select LAST_INSERT_ID() as invoice_no;

Select * from Staffs where user_name = 'hoainam'and staff_name ='HoaiNam' and user_pass='e78a1f1f50970cdea9956ff3c1867a2f';
Select staff_name from Staffs where staff_name= 'HoaiNam';

select * from Shoes where brand_name = 'van';
lock tables Customers write, Invoices write, Shoes write, InvoiceDetails write;
unlock tables;
select * from Customers order by customer_id desc limit 1;
