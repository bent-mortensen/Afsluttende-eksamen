<a name="top"></a>
# Database

DDL og lidt mock data til databasen.

* [Data Definition Language](#ddl)
* [Mock data](#mock)

[Back](https://github.com/bent-mortensen/Afsluttende-projekt#top)

<a name="ddl"></a>
Data Definition Language

```
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
	[Name] NVARCHAR(255),
	[Description] NVARCHAR(255),
	Celcius NVARCHAR(255),
	Hertz NVARCHAR(255),
	kWh NVARCHAR(255),
	Amps NVARCHAR(255)
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
```

[To the top](#top)


<a name="mock"></a>
# Mock data



```
-- Step 4 - Creating mock data
INSERT INTO Company ([Name], [Description], Email, Phonenumber, Cvr_number) VALUES ('Novenco', 'Ventilation fremstilling', 'novenco@novenco.dk', '55512345', '55598765');
INSERT INTO Company ([Name], [Description], Email, Phonenumber, Cvr_number) VALUES ('Kjeldbjerg Carpark', 'Parkeringshus', 'kbcp@kbcp.dk', '55577777', '55566666');

SELECT * FROM Company;

INSERT INTO Service_agreement_package ([Name], [Description], Celcius, Hertz, kWh, Amps) VALUES ('Guld', 'Guld pakke - Høj prioritet', '60', '5', '5', '3');
INSERT INTO Service_agreement_package ([Name], [Description], Celcius, Hertz, kWh, Amps) VALUES ('Sølv', 'Sølv pakke - Mellem prioritet', '80', '20', '6', '4');
INSERT INTO Service_agreement_package ([Name], [Description], Celcius, Hertz, kWh, Amps) VALUES ('Kobber', 'Kobber pakke - Lav prioritet', '100', '55', '7', '5');

SELECT * FROM Service_agreement_package;

INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 1);
INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 2);
INSERT INTO Ventilator ([Address], FK_Company_id, FK_Service_agreement_package_id) VALUES ('Iglsøvej 104, 7800 Skive', 2, 3);

SELECT * FROM Ventilator;

INSERT INTO Employee ([Name], Phonenumber, Email, FK_Company_id) VALUES ('bent mortensen', 22845214, 'bent_mortensen4@hotmail.com', 1);

SELECT * FROM Employee;

-- CURRENT_TIMESTAMP

-- USE novenco;
-- SELECT * FROM Company;
-- SELECT * FROM Service_agreement_package;
-- SELECT * FROM Ventilator;
-- SELECT * FROM Employee;

-- Randomly generated values
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, 'valid', 1);
INSERT INTO Ventilator_status ([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES (CURRENT_TIMESTAMP, 45, 4, 4 , 2, NULL, 1);

SELECT * FROM Ventilator_status;

INSERT INTO Ventilator_error ([Type], FK_Ventilator_status_id) VALUES ('Type of Error Celcius, Hertz, kWh, Amps - maybe a small description of error types it have', 1);

SELECT * FROM Ventilator_error;

INSERT INTO Error_correction_report ([Description], FK_Employee_id, FK_Ventilator_status_id) VALUES ('En beskrivelse af fejlen, samt en beskrivelse af tiltag lavet for at rette fejlen!', 1, 1);

SELECT *FROM Error_correction_report;
```

[To the top](#top)
