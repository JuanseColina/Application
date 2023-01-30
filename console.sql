CREATE DATABASE mydb;
USE mydb;

CREATE TABLE users (
                       name VARCHAR(255) NOT NULL,
                       email VARCHAR(255) NOT NULL,
                       document_number VARCHAR(255) NOT NULL,
                       id INT AUTO_INCREMENT PRIMARY KEY
                       
);