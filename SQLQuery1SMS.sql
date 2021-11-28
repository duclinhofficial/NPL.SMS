CREATE DATABASE SMS

GO

USE SMS

GO

-- Tao bang Customer
CREATE TABLE Customer
(
	customer_id int  IDENTITY(1,1) PRIMARY KEY,
	customer_name nvarchar(100)
)
GO

--Tao bang Employee
CREATE TABLE Employee
(
	employee_id int IDENTITY(1,1) PRIMARY KEY,
	employee_name nvarchar(100),
	salary double PRECISION,
	supervisor_id int
)
GO

-- Tao bang Product
CREATE TABLE Product 
(
	product_id int IDENTITY(1,1) PRIMARY KEY,
	product_name nvarchar(100),
	product_price double PRECISION,
)
GO
 
-- Tao bang Orders
CREATE TABLE Orders
(
	order_id int IDENTITY(1,1) PRIMARY KEY,
	order_date datetime,
	customer_id int,
	employee_id int,
	total double PRECISION
)
GO

-- Tao bang LineItem
CREATE TABLE LineItem
(
	order_id int,
	product_id int,
	PRIMARY KEY (order_id, product_id),
	quantity int,
	price double PRECISION
)
GO

-- Them du lieu vao customer
INSERT INTO Customer (customer_name)
VALUES 
	('Nguoi thu 1'), 
	('Nguoi thu 2'), 
	('Nguoi thu 3')
GO

--Them nhan vien
INSERT INTO Employee (employee_name, salary, supervisor_id)
VALUES
	('Nhan vien 1', 50.444, 123),
	('Nhan vien 2', 123.444, 001),
	('Nhan vien 3', 555.444, 245),
	('Nhan vien 4', 69, 890)
GO

-- Them san pham
INSERT INTO Product (product_name, product_price)
VALUES
	('San pham 1', 20000),
	('San pham 2', 10000),
	('San pham 3', 300000),
	('San pham 4', 699999.999)
GO

--Them vao Orders
INSERT INTO Orders (order_date, customer_id,employee_id,total)
VALUES
	('20210509 10:00:00 AM', 1, 1, 2605000),
	('20210618 12:30:09 AM', 1, 2, 600000),
	('20210725 08:34:09 AM', 1, 3, 1500000),
	('20210918 09:00:30 AM', 2, 2, 450000),
	('20211120 12:35:29 AM', 3, 3, 29999999.97)
GO

--Them vao LineItem
INSERT INTO LineItem (order_id,product_id,quantity, price)
VALUES
	(1,1,2,30000),
	(1,2,3,15000),
	(1,3,5,500000),
	(2,2,40,15000),
	(3,1,50,30000),
	(4,2,10,15000),
	(4,1,10,30000),
	(5,4,30,999999.999)
GO

--Cau 4: Function tinh tong order 
CREATE FUNCTION FN_Compute_OrderTotal(@Order_id int)
RETURNS double PRECISION AS
BEGIN
	DECLARE @total_price double PRECISION;
	SELECT @total_price = (SELECT SUM (quantity * price) FROM dbo.LineItem WHERE order_id = @Order_id);
	RETURN @total_price;
END
GO
