USE master
GO

-- Добавить проверку

CREATE DATABASE PensionAccumulationCalculator
GO

USE PensionAccumulationCalculator
GO

CREATE TABLE Ref_tables (
	table_id INT PRIMARY KEY IDENTITY(1,1),
	table_name NVARCHAR(50) NOT NULL
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
	Login NVARCHAR(50) NOT NULL UNIQUE CLUSTERED,
	Password NVARCHAR(32) NOT NULL,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id)
);

CREATE TABLE Ref_coefficients_cost_by_year (
	Year INT PRIMARY KEY IDENTITY(1,1),
	Cost DECIMAL(6,2) NOT NULL
);

CREATE TABLE Work_records (
	Work_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT NOT NULL,
	Individual_pension_coefficient DECIMAL(6,2) NOT NULL,
	Year INT NOT NULL,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
);

CREATE TABLE Insurance_records (
	Insurance_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT NOT NULL,
	Individual_pension_coefficient DECIMAL(6,2) NOT NULL,
	Year INT NOT NULL,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
);

CREATE TABLE Military_records (
	Military_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT NOT NULL,
	Individual_pension_coefficient DECIMAL(6,2) NOT NULL,
	Year INT NOT NULL,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
);

CREATE TABLE Individual_pencion_coefficient_accumulation (
	Accumulation_exp_id INT PRIMARY KEY IDENTITY(1,1),
	User_id INT NOT NULL,
	Individual_pension_coefficient DECIMAL(6,2) NOT NULL,
	Year INT NOT NULL,
	FOREIGN KEY (User_id) REFERENCES Clients(User_id),
	FOREIGN KEY (Year) REFERENCES Ref_coefficients_cost_by_year(Year)
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
		ROLLBACK TRANSACTION;

		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Users', RESEED);
		DBCC CHECKIDENT ('Clients', RESEED);

		SELECT CAST(0 AS BIT);
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

CREATE PROCEDURE dbo.GetUserXsd AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Users
		WHERE User_id = 0
		FOR XML RAW('User'), TYPE, XMLSCHEMA, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetUserXml 
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Users
		WHERE User_id = @id
		FOR XML RAW('User'), TYPE, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetUsersXml AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Users
		ORDER BY User_id
		FOR XML RAW('User'), TYPE, ELEMENTS, ROOT('Users');
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 1, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
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

CREATE PROCEDURE dbo.GetClientXsd AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Clients
		WHERE User_id = 0
		FOR XML RAW('Client'), TYPE, XMLSCHEMA, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 2, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetClientXml 
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Clients
		WHERE User_id = @id
		FOR XML RAW('Client'), TYPE, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 2, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetClientsXml AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Clients
		ORDER BY User_id
		FOR XML RAW('Client'), TYPE, ELEMENTS, ROOT('Clients');
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 2, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
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
		BEGIN TRANSACTION
			INSERT Work_records(User_id, Individual_pension_coefficient, Year)
			VALUES (@user_id, @individual_pension_coefficient, @year);

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @user_id, @year = @year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc + @individual_pension_coefficient)
				EXEC UpdateIPCAccumulation @user_id = @user_id, @new_ipc_count = @new_IPC, @year = @year
			END
			ELSE 
			BEGIN
				EXEC CreateIPCAccumulation @user_id = @user_id, @ipc_count = @individual_pension_coefficient, @year = @year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

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
		BEGIN TRANSACTION
			DECLARE @old_work_record_IPC DECIMAL(6, 2) = (SELECT wr.Individual_pension_coefficient FROM Work_records as wr WHERE wr.Work_exp_id = @id)

			UPDATE Work_records
			SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year
			WHERE Work_exp_id = @id;

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @user_id, @year = @year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc - @old_work_record_IPC + @individual_pension_coefficient)
				EXEC UpdateIPCAccumulation @user_id = @user_id, @new_ipc_count = @new_IPC, @year = @year
			END
			ELSE 
			BEGIN
				EXEC CreateIPCAccumulation @user_id = @user_id, @ipc_count = @individual_pension_coefficient, @year = @year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

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
		BEGIN TRANSACTION
			--CREATE TABLE #WorkRecordData (work_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			SELECT * INTO #WorkRecordData FROM Work_records as wr WHERE wr.Work_exp_id = @id
			DECLARE @old_work_record_IPC DECIMAL(6, 2) = (SELECT d.IPC_count FROM #IPCData as d)
			DECLARE @my_user_id DECIMAL(6, 2) = (SELECT d.user_id FROM #IPCData as d)
			DECLARE @my_year DECIMAL(6, 2) = (SELECT d.year FROM #IPCData as d)

			DELETE FROM Work_records
			WHERE Work_exp_id = @id;

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @my_user_id, @year = @my_year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc - @old_work_record_IPC)
				EXEC UpdateIPCAccumulation @user_id = @my_user_id, @new_ipc_count = @new_IPC, @year = @my_year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Work_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetWorkRecordXsd AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Work_records
		WHERE Work_exp_id = 0
		FOR XML RAW('WorkRecord'), TYPE, XMLSCHEMA, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetWorkRecordXml 
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Work_records
		WHERE Work_exp_id = @id
		FOR XML RAW('WorkRecord'), TYPE, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetWorkRecordsXml AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Work_records
		ORDER BY Work_exp_id
		FOR XML RAW('WorkRecord'), TYPE, ELEMENTS, ROOT('WorkRecords');
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 3, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
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
		BEGIN TRANSACTION
			INSERT Insurance_records(User_id, Individual_pension_coefficient, Year)
			VALUES (@user_id, @individual_pension_coefficient, @year);

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @user_id, @year = @year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc + @individual_pension_coefficient)
				EXEC UpdateIPCAccumulation @user_id = @user_id, @new_ipc_count = @new_IPC, @year = @year
			END
			ELSE 
			BEGIN
				EXEC CreateIPCAccumulation @user_id = @user_id, @ipc_count = @individual_pension_coefficient, @year = @year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

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
		BEGIN TRANSACTION
			DECLARE @old_insurance_record_IPC DECIMAL(6, 2) = (SELECT ir.Individual_pension_coefficient FROM Insurance_records as ir WHERE ir.Insurance_exp_id = @id)

			UPDATE Insurance_records
			SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year
			WHERE Insurance_exp_id = @id;

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @user_id, @year = @year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc - @old_insurance_record_IPC + @individual_pension_coefficient)
				EXEC UpdateIPCAccumulation @user_id = @user_id, @new_ipc_count = @new_IPC, @year = @year
			END
			ELSE 
			BEGIN
				EXEC CreateIPCAccumulation @user_id = @user_id, @ipc_count = @individual_pension_coefficient, @year = @year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

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
		BEGIN TRANSACTION
			--CREATE TABLE #WorkRecordData (work_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			SELECT * INTO #InsuranceRecordData FROM Insurance_records as ir WHERE ir.Insurance_exp_id = @id
			DECLARE @old_insurance_record_IPC DECIMAL(6, 2) = (SELECT d.IPC_count FROM #IPCData as d)
			DECLARE @my_user_id DECIMAL(6, 2) = (SELECT d.user_id FROM #IPCData as d)
			DECLARE @my_year DECIMAL(6, 2) = (SELECT d.year FROM #IPCData as d)

			DELETE FROM Insurance_records
			WHERE Insurance_exp_id = @id;

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @my_user_id, @year = @my_year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc - @old_insurance_record_IPC)
				EXEC UpdateIPCAccumulation @user_id = @my_user_id, @new_ipc_count = @new_IPC, @year = @my_year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Work_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetInsuranceRecordXsd AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Insurance_records
		WHERE Insurance_exp_id = 0
		FOR XML RAW('InsuranceRecord'), TYPE, XMLSCHEMA, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetInsuranceRecordXml 
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Insurance_records
		WHERE Insurance_exp_id = @id
		FOR XML RAW('InsuranceRecord'), TYPE, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetInsuranceRecordsXml AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Insurance_records
		ORDER BY Insurance_exp_id
		FOR XML RAW('InsuranceRecord'), TYPE, ELEMENTS, ROOT('InsuranceRecords');
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 4, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
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
		BEGIN TRANSACTION
			INSERT Military_records(User_id, Individual_pension_coefficient, Year)
			VALUES (@user_id, @individual_pension_coefficient, @year);

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @user_id, @year = @year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc + @individual_pension_coefficient)
				EXEC UpdateIPCAccumulation @user_id = @user_id, @new_ipc_count = @new_IPC, @year = @year
			END
			ELSE 
			BEGIN
				EXEC CreateIPCAccumulation @user_id = @user_id, @ipc_count = @individual_pension_coefficient, @year = @year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

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
		BEGIN TRANSACTION
			DECLARE @old_military_record_IPC DECIMAL(6, 2) = (SELECT mr.Individual_pension_coefficient FROM Military_records as mr WHERE mr.Military_exp_id = @id)

			UPDATE Military_records
			SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year
			WHERE Military_exp_id = @id;

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @user_id, @year = @year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc - @old_military_record_IPC + @individual_pension_coefficient)
				EXEC UpdateIPCAccumulation @user_id = @user_id, @new_ipc_count = @new_IPC, @year = @year
			END
			ELSE 
			BEGIN
				EXEC CreateIPCAccumulation @user_id = @user_id, @ipc_count = @individual_pension_coefficient, @year = @year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

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
		BEGIN TRANSACTION
			--CREATE TABLE #WorkRecordData (work_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			SELECT * INTO #MilitaryRecordData FROM Military_records as mr WHERE mr.Military_exp_id= @id
			DECLARE @old_military_record_IPC DECIMAL(6, 2) = (SELECT d.IPC_count FROM #IPCData as d)
			DECLARE @my_user_id DECIMAL(6, 2) = (SELECT d.user_id FROM #IPCData as d)
			DECLARE @my_year DECIMAL(6, 2) = (SELECT d.year FROM #IPCData as d)

			DELETE FROM Military_records
			WHERE Military_exp_id = @id;

			CREATE TABLE #IPCData (acc_exp_id INT, user_id INT, IPC_count DECIMAL(6, 2), year INT)
			INSERT INTO #IPCData EXEC dbo.GetIpcAccumulationData @user_id = @my_user_id, @year = @my_year;
			DECLARE @cur_ipc DECIMAL(6, 2)
			SET @cur_ipc = (SELECT IPC_count FROM #IPCData)
			IF (@cur_ipc) IS NOT NULL
			BEGIN
				DECLARE @new_IPC DECIMAL(6, 2)
				SET @new_IPC = (@cur_ipc - @old_military_record_IPC)
				EXEC UpdateIPCAccumulation @user_id = @my_user_id, @new_ipc_count = @new_IPC, @year = @my_year
			END
				
			DROP TABLE IF EXISTS #IPCData;

			SELECT CAST(1 AS BIT);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));

		DBCC CHECKIDENT ('Military_records', RESEED);

		SELECT CAST(0 AS BIT);
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetMilitaryRecordXsd AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Military_records
		WHERE Military_exp_id = 0
		FOR XML RAW('MilitaryRecord'), TYPE, XMLSCHEMA, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetMilitaryRecordXml 
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Military_records
		WHERE Military_exp_id = @id
		FOR XML RAW('MilitaryRecord'), TYPE, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetMilitaryRecordsXml AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Military_records
		ORDER BY Military_exp_id
		FOR XML RAW('MilitaryRecord'), TYPE, ELEMENTS, ROOT('MilitaryRecords');
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 5, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
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
		INSERT Individual_pencion_coefficient_accumulation(User_id, Individual_pension_coefficient, Year)
		VALUES (@user_id, @individual_pension_coefficient, @year);

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
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE Individual_pencion_coefficient_accumulation
		SET User_id = @user_id, Individual_pension_coefficient = @individual_pension_coefficient, Year = @year
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

CREATE PROCEDURE dbo.GetIndividualPencionCoefficientAccumulationXsd AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Individual_pencion_coefficient_accumulation
		WHERE Accumulation_exp_id = 0
		FOR XML RAW('IndividualPencionCoefficientAccumulation'), TYPE, XMLSCHEMA, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE GetIndividualPencionCoefficientAccumulationXml 
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Individual_pencion_coefficient_accumulation
		WHERE Accumulation_exp_id = @id
		FOR XML RAW('IndividualPencionCoefficientAccumulation'), TYPE, ELEMENTS;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE IndividualPencionCoefficientAccumulation AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT *
		FROM Individual_pencion_coefficient_accumulation
		ORDER BY Accumulation_exp_id
		FOR XML RAW('IndividualPencionCoefficientAccumulation'), TYPE, ELEMENTS, ROOT('IndividualPencionCoefficientAccumulations');
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
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

-- Individual_pension_coefficient_accumulation

CREATE PROCEDURE dbo.CreateIPCAccumulation
	@user_id INT,
	@ipc_count DECIMAL(6,2),
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		INSERT dbo.Individual_pencion_coefficient_accumulation(User_id, Individual_pension_coefficient, Year)
		VALUES (@user_id, @ipc_count, @year);
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO 

--EXEC CreateIPCAccumulation @user_id = 1, @ipc_count = 52.3, @year = 2020
--EXEC CreateIPCAccumulation @user_id = 2, @ipc_count = 22.6, @year = 2020
--EXEC CreateIPCAccumulation @user_id = 3, @ipc_count = 102.1, @year = 2020
--EXEC CreateIPCAccumulation @user_id = 3, @ipc_count = 102.1, @year = 2019
--EXEC CreateIPCAccumulation @user_id = 3, @ipc_count = 102.1, @year = 2021
--EXEC CreateIPCAccumulation @user_id = 3, @ipc_count = 102.1, @year = 2018
--EXEC CreateIPCAccumulation @user_id = 4, @ipc_count = 66.9, @year = 2020

CREATE PROCEDURE dbo.UpdateIPCAccumulation
	@user_id INT,
	@year INT,
	@new_ipc_count DECIMAL(6,2)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		DECLARE @cur_ipc DECIMAL(6,2)

		SET @cur_ipc =
		(	
			SELECT TOP 1 ipc_ac.Individual_pension_coefficient 
			FROM Individual_pencion_coefficient_accumulation as ipc_ac
			WHERE ipc_ac.User_id = @user_id and ipc_ac.Year = @year
		);

		UPDATE Individual_pencion_coefficient_accumulation
		SET Individual_pension_coefficient = @new_ipc_count 
		WHERE User_id = @user_id and Year = @year;
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO 

--EXEC UpdateIPCAccumulation @user_id = 3, @new_ipc_count = 211.2, @year = 2020

CREATE PROCEDURE dbo.DeleteIPCAccumulation
	@user_id INT,
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		DELETE FROM Individual_pencion_coefficient_accumulation
		WHERE User_id = @user_id and Year = @year
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO 

--EXEC DeleteIPCAccumulation @user_id = 4, @year = 2020

CREATE PROCEDURE dbo.GetLastIPCAccumulationData
	@user_id INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT TOP 1 * FROM Individual_pencion_coefficient_accumulation
		WHERE User_id = @user_id
		ORDER BY Year desc
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

--EXEC GetLastIPCAccumulationData @user_id = 3

CREATE PROCEDURE dbo.GetIPCAccumulationData
	@user_id INT,
	@year INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT TOP 1 * FROM Individual_pencion_coefficient_accumulation
		WHERE User_id = @user_id and Year = @year
		ORDER BY Year desc
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO

CREATE PROCEDURE dbo.GetDataForPensionCalculator
	@user_id INT,
	@year INT
AS 
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		SELECT
			c.First_name,
			c.Last_name,
			ipca.Individual_pension_coefficient,
			rccby.Cost
		FROM dbo.Users as u
		LEFT JOIN Individual_pencion_coefficient_accumulation AS ipca
			ON u.User_id = ipca.User_id
		LEFT JOIN dbo.Clients as c
			ON c.User_id = u.User_id
		LEFT JOIN dbo.Ref_coefficients_cost_by_year as rccby
			ON rccby.Year = ipca.Year
		WHERE ipca.Year = @year AND u.User_id = @user_id
	END TRY
	BEGIN CATCH
		INSERT Error_logs(Error_datetime, Source_table_id, Details)
		VALUES (GETDATE(), 6, CONCAT('ERROR ', ERROR_NUMBER(), ': ', ERROR_MESSAGE()));
	END CATCH
END
GO 


--EXEC  @user_id = 1, @record_id = 2, @table_id = 6, @year = 2015;
--SELECT * FROM Individual_pencion_coefficient_accumulation
--SELECT * FROM Error_logs
--SELECT * FROM Ref_coefficients_cost_by_year
--SELECT * FROM Users
--SELECT * FROM Ref_tables


EXEC CreateUser @login = 'admin', @password = 'admin';
EXEC CreateUser @login = 'Georgiy', @password = 'admin';
EXEC CreateUser @login = 'Mikhail', @password = 'admin';
EXEC CreateUser @login = 'Administrator', @password = 'admin';



