CREATE DATABASE customers_db;

USE customers_db;

CREATE TABLE customers(
	id int not null auto_increment,
    firstname varchar(50) not null,
    lastname varchar(50) not null,
    email varchar(255) not null,
    phone varchar(44) not null,
    address varchar(120) not null,
    primary key (id)
);
SELECT *FROM customers;
INSERT INTO customers(id,firstname,lastname,email,phone,address) VALUES(1,'Alexis','Reyes','alexis@gmail.com','5555','cerrada num.3');
DELETE FROM customers WHERE customers.id = 4;