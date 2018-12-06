--Novenco Database DDL Script. (Dato. 03-12-2018)

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


CREATE TABLE Error_type(
	Error_type_id INT IDENTITY(1,1) PRIMARY KEY,
	[Type_name] NVARCHAR(255)
);


CREATE TABLE Ventilator_error (
	Ventilator_error_id INT IDENTITY(1,1) PRIMARY KEY,
	FK_Error_type_id INT FOREIGN KEY REFERENCES Error_type(Error_type_id),
	FK_Ventilator_status_id INT FOREIGN KEY REFERENCES Ventilator_status(Ventilator_status_id)
);


CREATE TABLE Spare_part(
	Spare_part_id INT IDENTITY(1,1) PRIMARY KEY,
	Spare_part_name NVARCHAR(255)
);

CREATE TABLE Spare_part_list(
	Spare_part_list_id INT IDENTITY(1,1) PRIMARY KEY,
	List_id INT,
	FK_Spare_part_id INT FOREIGN KEY REFERENCES Spare_part(Spare_part_id)
);

CREATE TABLE Error_correction_report (
	Error_correction_report_id INT IDENTITY(1,1) PRIMARY KEY,
	Error_description NVARCHAR(4000),
	Error_correction_description NVARCHAR(4000),
	Correction_date DATETIME,
	Sap_celcius NVARCHAR(255),
	Sap_hertz NVARCHAR(255),
	Sap_kwh NVARCHAR(255),
	Sap_amps NVARCHAR(255),
	FK_Spare_part_list_id INT FOREIGN KEY REFERENCES Spare_part_list(Spare_part_list_id),
	FK_Employee_id INT FOREIGN KEY REFERENCES Employee(Employee_id),
	FK_Ventilator_error_id INT FOREIGN KEY REFERENCES Ventilator_error(Ventilator_error_id)
);

-- Step 4 - Creating mock data.
INSERT INTO Company ([Name], [Description], Email, Phonenumber, Cvr_number) VALUES ('Novenco', 'Ventilation fremstilling', 'novenco@novenco.dk', '55512345', '55598765');
INSERT INTO Company ([Name], [Description], Email, Phonenumber, Cvr_number) VALUES ('Kjeldbjerg Carpark', 'Parkeringshus', 'kbcp@kbcp.dk', '55577777', '55566666');

INSERT INTO Service_agreement_package (Sap_Name, Sap_Description, Sap_Celcius, Sap_Hertz, Sap_kWh, Sap_Amps) VALUES ('Guld', 'Guld pakke - Høj prioritet', '60', '5', '5', '3');
INSERT INTO Service_agreement_package (Sap_Name, Sap_Description, Sap_Celcius, Sap_Hertz, Sap_kWh, Sap_Amps) VALUES ('Sølv', 'Sølv pakke - Mellem prioritet', '80', '20', '6', '4');
INSERT INTO Service_agreement_package (Sap_Name, Sap_Description, Sap_Celcius, Sap_Hertz, Sap_kWh, Sap_Amps) VALUES ('Kobber', 'Kobber pakke - Lav prioritet', '100', '55', '7', '5');

INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 1);
INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 2);
INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 3);

INSERT INTO Employee ([Name], Phonenumber, Email, FK_Company_id) VALUES ('bent mortensen', 22845214, 'bent_mortensen4@hotmail.com', 1);

INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, 'valid', 1);
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, NULL, 1);
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 111, 70, 10 , 10, NULL, 1); 
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 111, 70, 10 , 10, NULL, 2); 

INSERT INTO Error_type ([Type_name]) VALUES ('Rystelser - Hertz');
INSERT INTO Error_type ([Type_name]) VALUES ('Temperatur - Celcius');
INSERT INTO Error_type ([Type_name]) VALUES ('Ampere - Amps');
INSERT INTO Error_type ([Type_name]) VALUES ('Kilowatt-timer - kWh');
INSERT INTO Error_type ([Type_name]) VALUES ('Andet - Other');

INSERT INTO Ventilator_error (FK_Error_type_id, FK_Ventilator_status_id) VALUES (1, 1);
INSERT INTO Ventilator_error (FK_Error_type_id, FK_Ventilator_status_id) VALUES (2, 2);

INSERT INTO Spare_part (Spare_part_name) VALUES ('Andet');
INSERT INTO Spare_part (Spare_part_name) VALUES ('føler');
INSERT INTO Spare_part (Spare_part_name) VALUES ('lejer');
INSERT INTO Spare_part (Spare_part_name) VALUES ('motor');
INSERT INTO Spare_part (Spare_part_name) VALUES ('ravpluks');
INSERT INTO Spare_part (Spare_part_name) VALUES ('varmeskjold');

INSERT INTO Spare_part_list (List_id,FK_Spare_part_id) VALUES (1, 1);
INSERT INTO Spare_part_list (List_id,FK_Spare_part_id) VALUES (1, 2);

INSERT INTO Error_correction_report (Error_description, Error_correction_description, Correction_date, Sap_celcius, Sap_amps, Sap_hertz, Sap_kwh, FK_Ventilator_error_id, FK_Employee_id, FK_Spare_part_list_id)  VALUES ('beskrivelse af fejlen', 'beskrivelse af tiltag for at rette fejlen', CURRENT_TIMESTAMP, 0, 0, 0, 0, 1, 1, 1);

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT * FROM Company;
SELECT * FROM Service_agreement_package;
SELECT * FROM Employee;
SELECT * FROM Ventilator;
SELECT * FROM Ventilator_status;
SELECT * FROM Ventilator_error;
SELECT * FROM Error_type;
SELECT * FROM Spare_part;
SELECT * FROM Spare_part_list;
SELECT * FROM Error_correction_report;
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Step 4 - Creating mock data.
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
--DELETE FROM Employee WHERE Name='Test';


INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, 'valid', 1);
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, NULL, 1);
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 111, 70, 10 , 10, NULL, 1); 
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 111, 70, 10 , 10, NULL, 2); 

SELECT * FROM Ventilator_status;
-- DELETE FROM Ventilator_status WHERE Hertz=70;

INSERT INTO Error_type ([Type_name]) VALUES ('Rystelser - Hertz');
INSERT INTO Error_type ([Type_name]) VALUES ('Temperatur - Celcius');
INSERT INTO Error_type ([Type_name]) VALUES ('Ampere - Amps');
INSERT INTO Error_type ([Type_name]) VALUES ('Kilowatt-timer - kWh');
INSERT INTO Error_type ([Type_name]) VALUES ('Andet - Other');

SELECT * FROM Error_type;

INSERT INTO Ventilator_error (FK_Error_type_id, FK_Ventilator_status_id) VALUES (1, 1);

-- scope identity 
INSERT INTO Ventilator_error(FK_Error_type_id, FK_Ventilator_status_id) VALUES(2, 2); SELECT SCOPE_IDENTITY();
SELECT * FROM Ventilator_error;

-- clean up
-- DELETE FROM Ventilator_error WHERE Ventilator_error_id BETWEEN 13 AND 183;


INSERT INTO Spare_part (Spare_part_name) VALUES ('Andet');
INSERT INTO Spare_part (Spare_part_name) VALUES ('føler');
INSERT INTO Spare_part (Spare_part_name) VALUES ('lejer');
INSERT INTO Spare_part (Spare_part_name) VALUES ('motor');
INSERT INTO Spare_part (Spare_part_name) VALUES ('ravpluks');
INSERT INTO Spare_part (Spare_part_name) VALUES ('varmeskjold');

SELECT * FROM Spare_part;

INSERT INTO Spare_part_list (List_id,FK_Spare_part_id) VALUES (1, 1);
INSERT INTO Spare_part_list (List_id,FK_Spare_part_id) VALUES (1, 2);

SELECT * FROM Spare_part_list WHERE List_id = 1;

INSERT INTO Error_correction_report (Error_description, Error_correction_description, Correction_date, Sap_celcius,
 Sap_amps, Sap_hertz, Sap_kwh, FK_Ventilator_error_id, FK_Employee_id, FK_Spare_part_list_id) 
 VALUES ('beskrivelse af fejlen', 'beskrivelse af tiltag for at rette fejlen', CURRENT_TIMESTAMP, 0, 0, 0, 0, 1, 1, 1);

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
-- DELETE FROM Ventilator_status WHERE Ventilator_status_id BETWEEN 4 AND 19;

SELECT * FROM Ventilator_error;
-- DELETE FROM Ventilator_error WHERE FK_Error_type_id = 1

-- get single ventilator status
SELECT Ventilator_status.*, Company.*, Ventilator.*, Service_agreement_package.* 
FROM Ventilator_status 
INNER JOIN Ventilator ON Ventilator.Ventilator_id = Ventilator_status.FK_Ventilator_id 
INNER JOIN Company ON Company.Company_id = Ventilator.FK_Company_id 
INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id 
WHERE Ventilator_status.Ventilator_status_id = 3;

SELECT * FROM Service_agreement_package WHERE Service_agreement_package_id = 1;

-- update sap med nye værdier
UPDATE Service_agreement_package SET Sap_Celcius = 12, Sap_Hertz = 12, Sap_kWh = 12, Sap_Amps = 12 WHERE Service_agreement_package_id = 1;
UPDATE Service_agreement_package SET Sap_Celcius = 60, Sap_Hertz = 5, Sap_kWh = 5, Sap_Amps = 3 WHERE Service_agreement_package_id = 1;

SELECT * FROM Employee;
SELECT * FROM Company;

SELECT * FROM Error_type

SELECT MAX(list_id) FROM Spare_part_list

SELECT MAX(Ventilator_error_id) FROM Ventilator_error WHERE FK_Error_type_id = 1 AND FK_Ventilator_status_id = 1;


--
UPDATE Ventilator_status SET Validated = 'valid' WHERE FK_Ventilator_id = 1 AND Validated IS NULL;
SELECT * FROM Ventilator_status;


SELECT DISTINCT ON FK_Ventilator_id FROM Ventilator_status WHERE Validated IS NULL

SELECT * FROM Ventilator_status WHERE Validated IS NULL
UNION
SELECT DISTINCT FK_Ventilator_id FROM Ventilator_status

SELECT  Ventilator.*, Ventilator_status.Validated, Ventilator_status.Ventilator_status_id, Ventilator_status.FK_Ventilator_id, Ventilator_status.[Datetime], Error_type.Error_type_id, Error_type.[Type_name]
FROM Ventilator_status
INNER JOIN Ventilator ON Ventilator.Ventilator_id = Ventilator_status.FK_Ventilator_id
INNER JOIN Ventilator_error ON Ventilator_error.FK_Ventilator_status_id = Ventilator_status.Ventilator_status_id
INNER JOIN Error_type ON Ventilator_error.FK_Error_type_id = Error_type.Error_type_id


SELECT MIN([Datetime]) FROM (SELECT * FROM Ventilator_status WHERE Validated IS NULL AND FK_Ventilator_id = 1)

SELECT * FROM table WHERE email in (SELECT email FROM table GROUP BY email HAVING COUNT(email)=1);

SELECT * FROM Ventilator_status WHERE Validated IS NULL AND DISTINCT FK_Ventilator_id  IN (SELECT DISTINCT FK_Ventilator_id FROM Ventilator_status WHERE Validated IS NULL)

SELECT MIN([Datetime]) FROM Ventilator_status WHERE Validated IS NULL


SELECT DISTINCT FK_Ventilator_id FROM Ventilator_status IN (SELECT * FROM Ventilator_status WHERE Validated IS NULL)


SELECT DISTINCT FK_Ventilator_id, [Datetime]  FROM Ventilator_status WHERE Ventilator_status.Validated IS NULL

SELECT  Ventilator_status_id, MIN([Datetime]) AS [Datetime]
FROM Ventilator_status
GROUP BY Ventilator_status_id


SELECT DISTINCT FK_Ventilator_id, MIN([Datetime]) AS [Datetime] FROM Ventilator_status WHERE Validated IS NULL GROUP BY FK_Ventilator_id
union
SELECT Ventilator_status_id, MIN([Datetime]) AS [Datetime] FROM Ventilator_status WHERE Validated IS NULL GROUP BY Ventilator_status_id


SELECT MIN([Datetime]) FROM Ventilator_status WHERE Validated IS NULL;

SELECT Ventilator_status.*, Company.*, Ventilator.*, Service_agreement_package.*
FROM Ventilator_status
INNER JOIN Ventilator ON Ventilator.Ventilator_id = Ventilator_status.FK_Ventilator_id
INNER JOIN Company ON Company.Company_id = Ventilator.FK_Company_id
INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id
WHERE Ventilator_status.Validated IS NULL;


SELECT Ventilator.*, Service_agreement_package.*
FROM Ventilator
INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id;


INSERT INTO Ventilator_status([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES(CURRENT_TIMESTAMP, 0, 0, 0, 0, 'valid', 1); SELECT SCOPE_IDENTITY();

INSERT INTO Employee ([Name], Phonenumber, Email, FK_Company_id) VALUES ('Test', 'Test', 'Test@Test.Test', 1);








DELETE Employee WHERE [Name] = 'Test';
SELECT * FROM Employee