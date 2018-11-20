-- Deleting the database.
--DROP DATABASE novenco;

-- Step 1 - Creating the database.
CREATE DATABASE novenco;
-- Step 2 - Use Created database.
USE novenco;
-- Step 3 - Creating the tables.
CREATE TABLE Company (
	Company_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255),
    [Description] NVARCHAR(255),
	Email NVARCHAR(255),
	Phonenumber NVARCHAR(8),
	Cvr_number NVARCHAR(8)
);

CREATE TABLE Service_agreement_package (
	Service_agreement_package_id INT IDENTITY(1,1) PRIMARY KEY,
	Sap_Name NVARCHAR(255),
	Sap_Description NVARCHAR(255),
	Sap_Celcius NVARCHAR(255),
	Sap_Hertz NVARCHAR(255),
	Sap_kWh NVARCHAR(255),
	Sap_Amps NVARCHAR(255)
);

CREATE TABLE Ventilator (
	Ventilator_id INT IDENTITY(1,1) PRIMARY KEY,
	[Address] NVARCHAR(255),
	FK_Company_id INT FOREIGN KEY REFERENCES Company(Company_id),
	FK_Service_agreement_package_id INT FOREIGN KEY REFERENCES Service_agreement_package(Service_agreement_package_id)
);

CREATE TABLE Employee (
	Employee_id INT IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(255),
	Phonenumber NVARCHAR(8),
	Email NVARCHAR(255),
	FK_Company_id INT FOREIGN KEY REFERENCES Company(Company_id)
);

CREATE TABLE Ventilator_status (
	Ventilator_status_id INT IDENTITY(1,1) PRIMARY KEY,
	[Datetime] DATETIME,
	Celcius NVARCHAR(255),
	Hertz NVARCHAR(255),
	kWh NVARCHAR(255),
	Amps NVARCHAR(255),
	Validated NVARCHAR(255),
	FK_Ventilator_id INT FOREIGN KEY REFERENCES Ventilator(Ventilator_id)
);

CREATE TABLE Ventilator_error (
	Ventilator_error_id INT IDENTITY(1,1) PRIMARY KEY,
	[Type] NVARCHAR(255),
	FK_Ventilator_status_id INT FOREIGN KEY REFERENCES Ventilator_status(Ventilator_status_id)
);

CREATE TABLE Error_correction_report (
	Error_correction_report_id INT IDENTITY(1,1) PRIMARY KEY,
	[Description] NVARCHAR(255),
	FK_Employee_id INT FOREIGN KEY REFERENCES Employee(Employee_id),
	FK_Ventilator_status_id INT FOREIGN KEY REFERENCES Ventilator_status(Ventilator_status_id)
);
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Step 4 - Creating mock data
INSERT INTO Company ([Name], [Description], Email, Phonenumber, Cvr_number) VALUES ('Novenco', 'Ventilation fremstilling', 'novenco@novenco.dk', '55512345', '55598765');
INSERT INTO Company ([Name], [Description], Email, Phonenumber, Cvr_number) VALUES ('Kjeldbjerg Carpark', 'Parkeringshus', 'kbcp@kbcp.dk', '55577777', '55566666');

SELECT * FROM Company;

INSERT INTO Service_agreement_package (Sap_Name, Sap_Description, Sap_Celcius, Sap_Hertz, Sap_kWh, Sap_Amps) VALUES ('Guld', 'Guld pakke - Høj prioritet', '60', '5', '5', '3');
INSERT INTO Service_agreement_package (Sap_Name, Sap_Description, Sap_Celcius, Sap_Hertz, Sap_kWh, Sap_Amps) VALUES ('Sølv', 'Sølv pakke - Mellem prioritet', '80', '20', '6', '4');
INSERT INTO Service_agreement_package (Sap_Name, Sap_Description, Sap_Celcius, Sap_Hertz, Sap_kWh, Sap_Amps) VALUES ('Kobber', 'Kobber pakke - Lav prioritet', '100', '55', '7', '5');

SELECT * FROM Service_agreement_package;

INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 1);
INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 2);
INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 3);

SELECT * FROM Ventilator;

INSERT INTO Employee ([Name], Phonenumber, Email, FK_Company_id) VALUES ('bent mortensen', 22845214, 'bent_mortensen4@hotmail.com', 1);

SELECT * FROM Employee;
DELETE FROM Employee WHERE Name='Test';


INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, 'valid', 1);
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, NULL, 1);
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 111, 70, 10 , 10, NULL, 1); 

SELECT * FROM Ventilator_status;
-- DELETE FROM Ventilator_status WHERE Hertz=70;

INSERT INTO Ventilator_error ([Type], FK_Ventilator_status_id) VALUES ('Type of Error Celcius, Hertz, kWh, Amps - maybe a small description of error types it have', 1);

SELECT * FROM Ventilator_error;
-- clean up
-- DELETE FROM Ventilator_error WHERE Ventilator_error_id BETWEEN 13 AND 183;


INSERT INTO Error_correction_report ([Description], FK_Employee_id, FK_Ventilator_status_id) VALUES ('En beskrivelse af fejlen, samt en beskrivelse af tiltag lavet for at rette fejlen!', 1, 1);

SELECT *FROM Error_correction_report;



--Get lastest Status not validated
SELECT * FROM Ventilator_status WHERE Ventilator_status.Validated IS NULL;

--Get specific Ventilator
SELECT * FROM Ventilator WHERE Ventilator.Ventilator_id = 1;

--Get Values from SAP
SELECT Service_agreement_package.Sap_Celcius, Service_agreement_package.Sap_Hertz, Service_agreement_package.Sap_kWh, Service_agreement_package.Sap_Amps, Ventilator.Ventilator_id
From Service_agreement_package
INNER JOIN Ventilator ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id;

-- get SAP values, set for ventilator 1.
SELECT Ventilator.Ventilator_id, Service_agreement_package.Sap_Celcius, Service_agreement_package.Sap_Hertz, Service_agreement_package.Sap_kWh, Service_agreement_package.Sap_Amps
FROM Ventilator 
RIGHT JOIN Service_agreement_package ON Service_agreement_package.Service_agreement_package_id = Ventilator.FK_Service_agreement_package_id
WHERE Ventilator.Ventilator_id = 1;


--Get status and company and ventilator
SELECT Ventilator_status.*, Company.*, Ventilator.*, Service_agreement_package.*
FROM Ventilator_status
INNER JOIN Ventilator ON Ventilator.Ventilator_id = Ventilator_status.FK_Ventilator_id
INNER JOIN Company ON Company.Company_id = Ventilator.FK_Company_id
INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id
WHERE Ventilator_status.Validated IS NULL;


--select BaseProduct.*, Company.*, PostalCode.*, SubCategory.*
--from BaseProduct
--inner join Company on Company.Name = BaseProduct.FK_CompanyName and BaseProduct.FK_CompanyEmail = Company.Email
--inner join PostalCode on BaseProduct.FK_PostalCode = PostalCode.Postal
--inner join SubCategory on SubCategory.Name = BaseProduct.FK_SubCategory
--where BaseProduct.Active = 1 and Company.Active = 1;


-- UPDATE Ventilator status to valid
UPDATE Ventilator_status SET Validated = 'valid' WHERE Ventilator_status_id = 2 AND Validated IS NULL;
-- UDATE Ventilator status to NULL 
UPDATE Ventilator_status SET Validated = NULL WHERE Ventilator_status_id = 2 AND Validated ='valid';

SELECT * FROM Ventilator_status;
--DELETE FROM Ventilator_status WHERE Ventilator_status_id BETWEEN 4 AND 19;

SELECT * FROM Ventilator_error;

-- get single ventilator status
SELECT Ventilator_status.*, Company.*, Ventilator.*, Service_agreement_package.* 
FROM Ventilator_status 
INNER JOIN Ventilator ON Ventilator.Ventilator_id = Ventilator_status.FK_Ventilator_id 
INNER JOIN Company ON Company.Company_id = Ventilator.FK_Company_id 
INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id 
WHERE Ventilator_status.Ventilator_status_id = 3;