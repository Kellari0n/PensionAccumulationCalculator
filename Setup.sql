USE master
GO

-- Добавить проверку

CREATE DATABASE PensionAccumulationCalculator
GO

USE PensionAccumulationCalculator
GO

CREATE TABLE Ref_tables (
	table_id INT PRIMARY KEY IDENTITY(1,1),
	table_name NVARCHAR(50)
);

CREATE TABLE Clients (
	User_id INT PRIMARY KEY IDENTITY(1,1),
	Second_name NVARCHAR(30),
	First_name NVARCHAR(30),
	Last_name NVARCHAR(30),
	Phone_number NVARCHAR(15),
	Email NVARCHAR(50),
);

CREATE TABLE Users (
	User_id INT PRIMARY KEY IDENTITY(1,1),
	Login NVARCHAR(50),
	Password NVARCHAR(32),
	FOREIGN KEY (User_id) REFERENCES Clients(User_id)
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
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
);

CREATE TABLE Insurance_record (
	Insurance_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
);

CREATE TABLE Military_record (
	Military_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
);

CREATE TABLE Individual_pencion_coefficient_accumulation (
	Accumulation_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Record_id INT,
	Table_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year),
	FOREIGN KEY (Table_id) REFERENCES Ref_tables(table_id)
);

CREATE TABLE Error_logs (
	Log_id INT PRIMARY KEY IDENTITY(1,1),
	Error_datetime DATETIME,
	Source_table_id INT,
	Details NVARCHAR(200),
	FOREIGN KEY (Source_table_id) REFERENCES Ref_tables(table_id),
);

GO

CREATE PROCEDURE dbo.CreateUser
	@login NVARCHAR(50),
	@password NVARCHAR(32)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			INSERT Clients(Second_name, First_name, Last_name)
			VALUES ('', '', '');

			INSERT Users(Login, Password)
			VALUES (@login, @password);
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		--INSERT Error_logs(Error_datetime, Source_table_id, Details)
		--VALUES (GETDATE(), ,'');

		DBCC CHECKIDENT ('Users', RESEED);
		DBCC CHECKIDENT ('Clients', RESEED);

		ROLLBACK TRANSACTION;
	END CATCH
END
GO 

CREATE PROCEDURE dbo.GetUserById
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			SELECT *
			FROM Users
			WHERE User_id = @id;
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- ERROR LOG

		ROLLBACK TRANSACTION;
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllUsers AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			SELECT *
			FROM Users;
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- ERROR LOG

		ROLLBACK TRANSACTION;
	END CATCH
END
GO

CREATE PROCEDURE dbo.UpdateUser 
	@id INT,
	@login NVARCHAR(50),
	@password NVARCHAR(32)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			UPDATE Users
			SET Login = @login, Password = @password
			WHERE User_id = @id;
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- ERROR LOG

		ROLLBACK TRANSACTION;
	END CATCH
END
GO

CREATE PROCEDURE dbo.DeleteUser
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM Users
			WHERE User_id = @id;

			DELETE FROM Clients
			WHERE User_id = @id;
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- ERROR LOG

		DBCC CHECKIDENT ('Users', RESEED);
		DBCC CHECKIDENT ('Clients', RESEED);

		ROLLBACK TRANSACTION;
	END CATCH
END
GO