-- Novenco Database DDL Script. (Dato. 03-12-2018)
-- Perform each step in order.

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

INSERT INTO Spare_part (Spare_part_name) VALUES ('Andet');
INSERT INTO Spare_part (Spare_part_name) VALUES ('føler');
INSERT INTO Spare_part (Spare_part_name) VALUES ('lejer');
INSERT INTO Spare_part (Spare_part_name) VALUES ('motor');
INSERT INTO Spare_part (Spare_part_name) VALUES ('ravpluks');
INSERT INTO Spare_part (Spare_part_name) VALUES ('varmeskjold');
