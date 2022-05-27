-- Create database 
create database CafeManagementSystem

-- use database 
use CafeManagementSystem

-- create table for Admin 
create table tblAdmin(
AdminId int primary key identity(1,1),
AdminUserName varchar(30) not null,
AdminPass varchar(30) not null
)

-- Use trigger for  no one can update delete and insert the new admin
create  trigger trig_Admin    
ON tblAdmin      
AFTER INSERT, UPDATE  ,delete       
AS print 'you can not insert, update and delete this table tblAdmin'        
rollback;
disable trigger
--alter trigger for delete  admin 
alter trigger trig_Admin    
ON tblAdmin      
AFTER INSERT, UPDATE  
AS print 'you can not insert and update  this table tblAdmin'        
rollback;

-- alter trigger for update 
alter trigger trig_Admin    
ON tblAdmin      
AFTER INSERT,delete        
AS print 'you can not insert and delete  this table tblAdmin'        
rollback;

-- alter trigger for insert another admin
alter trigger trig_Admin    
ON tblAdmin      
AFTER  UPDATE , delete       
AS print 'you can not update and delete this table tblAdmin'        
rollback;

-- insert admin
insert into tblAdmin values('Admin','admin')

delete from tblAdmin  where AdminId = 5

-- show all in admin table
select * from tblAdmin

-- create table for customer 
create table tblCustomer(
Cusid int primary key identity(1,1),
CusName varchar(30) not null,
CusEmail varchar(50) not null,
CusPass nvarchar(50) not null
)

--insert customer details
insert into tblCustomer values('Areeb','areeb12@gmail.com','areeb1234')
insert into tblCustomer values('Hasam','Hasam12@gmail.com','hasam1234'),('Moheen Sajjad','Moheen32@gmail.com','1234'),('M.Bilal','Bilal45@gmail.com','Bilal34'),
('Mustufa Hassan','voilent12@gmail.com','MH12'),('Hassan Khan','Hassna2@gmail.com','Hassan123'),('Tehreem Khan','Speed45@gmail.com','speed1234'),('Maham Arif','maham42@gmail.com','Maham786'),
('musab shyk','musab712@gmail.com','musab34'),('Usama Yasir','usamayasir5@gmail.com','usamaYasir12'),('Manazir Jaffri','ManazirJaffri8@gmail.com','korea1234')
insert into tblCustomer values('rafiq','rafiq12@gmail.com','rafiq34')
INSERT INTO  tblcustomer values('test','test@gmail,com','areeb')
--update customer detail
update tblCustomer set CusName='M.Areeb' where Cusid=1

--delete customer 
delete from tblCustomer  where Cusid = 1
select * from tblCustomer

--create procedure for customers detail store in one value 
CREATE PROCEDURE SelectAllCustomers 
@CustomerName varchar(30)
AS
begin
SELECT * FROM tblCustomer where CusName=@CustomerName
end


--execute procedure 
exec SelectAllCustomers 'Areeb'


--Create table order
create table tblOrder(
OrderId int primary key identity(1,1),
CustomerId int foreign key references tblCustomer(Cusid)
)

-- insert  customer id to take order id 
insert into tblOrder values(3),(4),(7),(5)


-- inner join between table order and table Customer
select tblOrder.OrderId,tblCustomer.CusName,tblCustomer.Cusid from tblOrder 
inner join tblCustomer on tblOrder.CustomerId=tblCustomer.Cusid

-- create table  for Categories
create table tblCategories(
CategoriesId int primary key identity(1,1),
CategoriesName varchar(50) not null
)

--insert Categories
insert into tblCategories values('Cake'),('Tea'),('Iced Coffee'),('Hot Coffee'),('Drinks')
select * from tblCategories

-- create table for item
create table tblItem(
ItemId int primary key identity(1,1),
Cateid int foreign key references tblCategories(CategoriesId),
ItemName varchar(50) not null,
size varchar(50) not null,
qty int not null,
price int not null
)

select tblCategories.CategoriesName,tblItem.ItemName from tblItem 
inner join tblCategories on tblItem.Cateid=tblCategories.CategoriesId


--insert item detials
insert into tblItem values(1,'Three Milk','Medium',10,1500),
(1,'Milky Malt','Medium',5,1050),(1,'Nutella Sundae','Medium',7,1200),(1,'Galaxy Sundae','Medium',8,1350)
insert into tblItem values(1,'Three Milk','Small',7,1000),
(1,'Milky Malt','Small',5,800),(1,'Nutella Sundae','Small',5,700),(1,'Galaxy Sundae','Small',3,750)
insert into tblItem values(2,'Jasmine','Small',15,100),(2,'Jasmine','Medium',8,150),(2,'Black','Small',7,100),(2,'Black','Medium',11,150),
(2,'WinterMelon','Small',12,100),(2,'Wintermelon','Medium',5,150)
insert into tblItem values(3,'Expresso','Small',15,150),(3,'Expresso','Medium',8,200),(3,'Latte','Small',7,150),(3,'Latte','Medium',11,200),
(3,'Coca','Small',12,150),(3,'Coca','Medium',5,200)
insert into tblItem values(4,'Flate White','Small',12,150),(4,'Flate White','Medium',15,200),(4,'Cappuccio','Small',14,150),(4,'Cappuccio','Medium',11,200),
(4,'Chocolate','Small',5,150),(4,'Chocolate','Medium',10,200)
insert into tblItem values(5,'Orange Juice ','Small',10,100),(5,'Orange Juice','Medium',8,150),(5,'Apple Juice','Small',20,100),(5,'Apple Juice','Medium',11,150),
(5,'WinterMelon','Juice',12,100),(5,'PineApple Juice','Medium',5,150)
select * from tblItem

update tblItem set Cateid=4 where ItemId=26


--inner join between tblCategories and tbl item
select tblItem.ItemName,tblCategories.CategoriesName,tblItem.size,tblItem.qty,tblItem.price from tblItem 
inner join tblCategories on tblCategories.CategoriesId=tblItem.Cateid where tblCategories.CategoriesName='cake'

--create  table  orderdeatials
create table tblOrderDetails(
DetialOrder int primary key identity(1,1),
orderId  int foreign key references tblOrder(OrderId),
ItemId  int foreign key references tblItem(ItemId),
qty int not null,
price int not null
)


--insert orderDetials
insert into tblOrderDetails values(1,1,3,3*1500) 
update  tblItem set qty=qty-3 where ItemId=1
select * from tblOrderDetails



-- Show proper orderdetail to give by customer 
select tblOrder.OrderId,tblCustomer.CusName,tblItem.ItemName,tblCategories.CategoriesName,tblItem.size,tblOrderDetails.qty,tblOrderDetails.price
from ((((tblOrderDetails 
inner join tblOrder on tblOrderDetails.orderId = tblOrder.OrderId)
inner join tblCustomer on tblOrder.CustomerId=tblCustomer.Cusid)
inner join tblItem on tblOrderDetails.ItemId = tblItem.ItemId)
inner join tblCategories on tblCategories.CategoriesId=tblItem.Cateid)



create table product(
cat_ID int primary key identity (1,1) not null,
Product_Name varchar(50)
Product_Cost int not nul
)


select max(Productname) from product where select max(productost) from product )



create table order(
oorder_Id int,
product_Id foriegn key refrence not null,

)

select order_Id,Product_Id from Product,Order
inner join on order_Id and Product_Id