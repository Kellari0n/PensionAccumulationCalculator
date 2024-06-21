USE master
GO

CREATE DATABASE PensionAccumulationCalculator
GO

USE PensionAccumulationCalculator
GO

CREATE TABLE Users (
	User_id INT PRIMARY KEY IDENTITY(1,1),
	Login NVARCHAR(50),
	Password NVARCHAR(32)
);

CREATE TABLE Clients (
	User_id INT PRIMARY KEY IDENTITY(1,1),
	Second_name NVARCHAR(30),
	First_name NVARCHAR(30),
	Last_name NVARCHAR(30),
	Phone_number NVARCHAR(15),
	Email NVARCHAR(50)
);

CREATE TABLE Ref_coefficients_cost_by_year (
	Year INT PRIMARY KEY IDENTITY(1,1),
	Cost DECIMAL(6,2)
);

CREATE TABLE Work_records (
	Work_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Users(User_id)
);

CREATE TABLE Insurance_record (
	Insurance_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Users(User_id)
);

CREATE TABLE Military_record (
	Military_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Users(User_id)
);

CREATE TABLE Individual_pencion_coefficient_accumulation (
	Accumulation_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Users(User_id)
);

