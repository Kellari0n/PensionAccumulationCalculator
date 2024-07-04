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
	Login NVARCHAR(50) UNIQUE,
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

CREATE TABLE Insurance_records (
	Insurance_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT,
	Individual_pension_coefficient DECIMAL(6,2),
	Year INT,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
);

CREATE TABLE Military_records (
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
	Details NVARCHAR(500),
	FOREIGN KEY (Source_table_id) REFERENCES Ref_tables(table_id),
);
GO

SET IDENTITY_INSERT dbo.Ref_tables ON;  

INSERT Ref_tables(table_id, table_name)
VALUES
	(1, 'Users'),
	(2, 'Clients'),
	(3, 'Work_records'),
	(4, 'Insurance_record'),
	(5, 'Military_record'),
	(6, 'Individual_pencion_coefficient_accumulation'),
	(7, 'Ref_coefficients_cost_by_year');

SET IDENTITY_INSERT dbo.Ref_tables OFF;  

SET IDENTITY_INSERT dbo.Ref_coefficients_cost_by_year ON;

INSERT Ref_coefficients_cost_by_year(Year, Cost)
VALUES 
	(2015, 71.41),
	(2016, 74.27),
	(2017, 78.58),
	(2018, 81.49),
	(2019, 87.24),
	(2020, 93.00),
	(2021, 98.86),
	(2022, 118.10),
	(2023, 123.77),
	(2024, 133.05);


SET IDENTITY_INSERT dbo.Ref_coefficients_cost_by_year OFF;

GO

-- USERS

CREATE PROCEDURE dbo.CreateUser
	@login NVARCHAR(50),
	@password NVARCHAR(32),
	@second_name NVARCHAR(30) = '',
	@first_name NVARCHAR(30) = '',
	@last_name NVARCHAR(30) = '',
	@phone_number NVARCHAR(30) = '',
	@email NVARCHAR(30) = ''
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			INSERT Clients(Second_name, First_name, Last_name, Phone_number, Email)
			VALUES (@second_name, @first_name, @last_name, @phone_number, @email);

			INSERT Users(Login, Password)
			VALUES (@login, @password);

			SELECT CAST(1 AS BIT);
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Users', RESEED);
		DBCC CHECKIDENT ('Clients', RESEED);

		SELECT CAST(0 AS BIT);

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
		SELECT *
		FROM Users
		WHERE User_id = @id;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllUsers AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Users;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
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
		UPDATE Users
		SET Login = @login, Password = @password
		WHERE User_id = @id;

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		SELECT CAST(0 AS BIT);
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

			SELECT CAST(1 AS BIT);
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Users', RESEED);
		DBCC CHECKIDENT ('Clients', RESEED);

		SELECT CAST(0 AS BIT);

		ROLLBACK TRANSACTION;
	END CATCH
END
GO

-- CLIENTS

CREATE PROCEDURE dbo.GetClientById
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Clients
		WHERE User_id = @id;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 2, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllClients AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Clients;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 2, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.UpdateClient 
	@id INT,
	@second_name NVARCHAR(30),
	@first_name NVARCHAR(30),
	@last_name NVARCHAR(30),
	@phone_number NVARCHAR(15),
	@email NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE Clients
		SET Second_name = @second_name, First_name = @first_name, Last_name = @last_name, Phone_number = @phone_number, Email = @email
		WHERE User_id = @id;

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 2, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

--WORK_RECORDS

CREATE PROCEDURE dbo.CreateWorkRecord
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		INSERT Work_records(User_id, Individual_pension_coefficient, Year)
		VALUES (@user_id, @individual_pension_coefficient, @year);

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Work_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO 

CREATE PROCEDURE dbo.GetWorkRecordById
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Work_records
		WHERE Work_exp_id = @id;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllWorkRecords AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Work_records;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.UpdateWorkRecord
	@id INT,
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE Work_records
		SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year
		WHERE Work_exp_id = @id;

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.DeleteWorkRecord
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
			DELETE FROM Work_records
			WHERE Work_exp_id = @id;

			SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Work_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

--INSURANCE_RECORDS

CREATE PROCEDURE dbo.CreateInsuranceRecord
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		INSERT Insurance_records(User_id, Individual_pension_coefficient, Year)
		VALUES (@user_id, @individual_pension_coefficient, @year);

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Insurance_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetInsuranceRecordById
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Insurance_records
		WHERE Insurance_exp_id = @id;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllInsuranceRecords AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Insurance_records;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.UpdateInsuranceRecord
	@id INT,
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE Insurance_records
		SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year
		WHERE Insurance_exp_id = @id;

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.DeleteInsuranceRecord
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
			DELETE FROM Insurance_records
			WHERE Insurance_exp_id = @id;

			SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Work_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

-- MILITARY_RECORDS

CREATE PROCEDURE dbo.CreateMilitaryRecord
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		INSERT Military_records(User_id, Individual_pension_coefficient, Year)
		VALUES (@user_id, @individual_pension_coefficient, @year);

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Military_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO 

CREATE PROCEDURE dbo.GetMilitaryRecordById
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Military_records
		WHERE Military_exp_id = @id;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllMilitaryRecords AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Military_records;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.UpdateMilitaryRecord
	@id INT,
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE Military_records
		SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year
		WHERE Military_exp_id = @id;

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.DeleteMilitaryRecord
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
			DELETE FROM Military_records
			WHERE Military_exp_id = @id;

			SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Military_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

-- INDIVIDUAL_PENCION_COEFFICIENT_ACCUMULATION

CREATE PROCEDURE dbo.CreateIndividualPencionCoefficientAccumulation
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT,
	@record_id INT,
	@table_id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		INSERT Individual_pencion_coefficient_accumulation(User_id, Individual_pension_coefficient, Year, Record_id, Table_id)
		VALUES (@user_id, @individual_pension_coefficient, @year, @record_id, @table_id);

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Individual_pencion_coefficient_accumulation', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO 

CREATE PROCEDURE dbo.GetIndividualPencionCoefficientAccumulationById
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Individual_pencion_coefficient_accumulation
		WHERE Accumulation_exp_id = @id;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllIndividualPencionCoefficientAccumulations AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Individual_pencion_coefficient_accumulation;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.UpdateIndividualPencionCoefficientAccumulation
	@id INT,
	@user_id INT,
	@individual_pension_coefficient DECIMAL(6, 2),
	@year INT,
	@record_id INT,
	@table_id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE Individual_pencion_coefficient_accumulation
		SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year, Record_id = @record_id, Table_id = @table_id
		WHERE Accumulation_exp_id = @id;

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.DeleteIndividualPencionCoefficientAccumulation
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
			DELETE FROM Individual_pencion_coefficient_accumulation
			WHERE Accumulation_exp_id = @id;

			SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Individual_pencion_coefficient_accumulation', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

-- REF_COEFFICIENTS_COST_BY_YEAR

CREATE PROCEDURE dbo.CreateRefCoefficientCostByYear 
	@year INT,
	@cost DECIMAL(6,2)
AS
BEGIN
	SET NOCOUNT ON;
	SET IDENTITY_INSERT dbo.Ref_coefficients_cost_by_year ON;
	BEGIN TRY
		INSERT Ref_coefficients_cost_by_year(Year, Cost)
		VALUES (@year, @cost);

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 7, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Ref_coefficients_cost_by_year', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
	SET IDENTITY_INSERT dbo.Ref_coefficients_cost_by_year OFF;
END
GO 

CREATE PROCEDURE dbo.GetRefCoefficientCostByYear
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Ref_coefficients_cost_by_year
		WHERE Year = @year;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 7, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetAllRefCoefficientsCostByYear AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Ref_coefficients_cost_by_year;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 7, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.UpdateRefCoefficientCostByYear
	@year INT,
	@cost DECIMAL(6,2)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE Ref_coefficients_cost_by_year
		SET Cost = @cost 
		WHERE Year = @year;

		SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 7, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.DeleteRefCoefficientCostByYear
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
			DELETE FROM Ref_coefficients_cost_by_year
			WHERE Year = @year;

			SELECT CAST(1 AS BIT);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 7, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Ref_coefficients_cost_by_year', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

EXEC CreateUser @login = 'admin', @password = 'admin';