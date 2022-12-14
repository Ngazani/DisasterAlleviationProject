Create database DisasterAlleviationdb;
use DisasterAlleviationdb;

CREATE TABLE users
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         Uname VARCHAR(200) NULL,
         email VARCHAR(200) NOT NULL UNIQUE,
         Upassword VARCHAR(200) NULL,
         Utype VARCHAR(200) NULL
);

INSERT INTO users (Uname,email,Upassword,Utype)
VALUES('Demo','demomail@gmail.com','demoword','User');

CREATE TABLE monetary
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         Donor_date VARCHAR(200) NOT NULL,
         amount VARCHAR(200) NOT NULL,
         donor VARCHAR(200) NULL
);

CREATE TABLE goods
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         donor_date VARCHAR(200) NOT NULL,
         quantity VARCHAR(200) NOT NULL,
         category VARCHAR(200) NOT NULL,
         description VARCHAR(200) NOT NULL,
         donor VARCHAR(200) NULL
);



CREATE TABLE disaster
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         start_date VARCHAR(200) NOT NULL,
         end_date VARCHAR(200) NOT NULL,
         location VARCHAR(200) NOT NULL,
         description VARCHAR(200) NOT NULL,
         aid VARCHAR(200) NULL,
         diStatus VARCHAR (200) NULL
);

CREATE TABLE category
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         name VARCHAR(200) NOT NULL UNIQUE
       
);
insert into category (name) values ('Clothes');

CREATE TABLE purchases
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         item VARCHAR(200) NOT NULL,
         quantity VARCHAR(200) NOT NULL,
         category VARCHAR(200) NOT NULL,
         price VARCHAR(200) NOT NULL
    
);


CREATE TABLE inventory
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         quantity VARCHAR(200) NOT NULL,
         Amount VARCHAR(200) NOT NULL,
);


CREATE TABLE goodsAllocation
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         item VARCHAR(200) NOT NULL,
         quantity VARCHAR(200) NOT NULL,
         category VARCHAR(200) NOT NULL,
         disaster VARCHAR(200) NOT NULL
    
);

CREATE TABLE moneyAllocation
(
         id INT IDENTITY NOT NULL PRIMARY KEY,
         amount VARCHAR(200) NOT NULL,
         disaster VARCHAR(200) NOT NULL
    
);

select * from monetary

