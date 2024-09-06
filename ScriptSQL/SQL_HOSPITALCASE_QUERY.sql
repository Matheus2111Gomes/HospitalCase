CREATE DATABASE HOSPITAL_CASE;
USE HOSPITAL_CASE;

CREATE TABLE Patients(
	Id UNIQUEIDENTIFIER PRIMARY KEY,
	Name varchar(255),
	Address varchar(255),
	Telephone varchar(255),
	Document varchar(255),
	DateBirth Date,
)
INSERT INTO Patients(
	Id,
	Name,
	Address,
	Telephone,
	Document,
	DateBirth
)
VALUES(
	'eb6a1de8-95b0-49cc-ba34-09a4c0c43c34',
	'Matheus',
	'Address Teste',
	'Telephone Teste',
	'123123123',
	'11/11/1996'
);
--Select * from Patients;

CREATE TABLE Specialtys(
	Id int Identity PRIMARY KEY,
	Name varchar(255)
);
INSERT INTO Specialtys(
	Name
)
VALUES(
	'cardiologist'
);
--SELECT * FROM Specialtys

CREATE TABLE Doctors (
	Id UNIQUEIDENTIFIER PRIMARY KEY,
	CRM varchar(255),
	Name varchar(255),
	SpecialtyId int FOREIGN KEY (SpecialtyId) REFERENCES Specialtys(Id),
);
INSERT INTO Doctors(
	Id,
	CRM,
	Name,
	SpecialtyId
)
VALUES(
	'04baf0d4-ed56-487f-85e2-231db8c9f6b0',
	'000000000',
	'Matheus só que Doutor',
	1
);
SELECT * FROM Doctors;

CREATE TABLE Appointments(
	Id UNIQUEIDENTIFIER PRIMARY KEY,
	AppointmentDate Date,
	PatientId UNIQUEIDENTIFIER FOREIGN KEY (PatientId) REFERENCES Patients(Id),
	DoctorId UNIQUEIDENTIFIER FOREIGN KEY (DoctorId) REFERENCES Doctors(Id),
	AppointmentInfos NVARCHAR(MAX)
)
--SELECT * FROM Appointments;

--LogsPatient
CREATE TABLE PatientAudit (
    AuditId UNIQUEIDENTIFIER PRIMARY KEY,
    PatientId UNIQUEIDENTIFIER,
    OperationType VARCHAR(50),
    ChangeDate DATETIME DEFAULT GETDATE(),
    OldName VARCHAR(255),
    NewName VARCHAR(255),
    OldAddress VARCHAR(255),
    NewAddress VARCHAR(255),
	OldTelephone VARCHAR(255),
    NewTelephone VARCHAR(255)
);

--Tabelas de junção
CREATE TABLE DoctorPatient (
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    PatientId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (DoctorId, PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id),
    FOREIGN KEY (PatientId) REFERENCES Patients(Id)
);


--select * from PatientAudit


CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Username VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL, -- 'Patient', 'Doctor', 'Admin'
    ForeignId UNIQUEIDENTIFIER -- Referencia - PatientId/DoctorId
);

--Trigger Para Loggar alterações em paciente
CREATE TRIGGER trg_AuditPatientUpdate
ON Patients
AFTER UPDATE
AS
BEGIN
    INSERT INTO PatientAudit (AuditId, PatientId, OperationType, OldName, NewName, OldAddress, NewAddress, OldTelephone, NewTelephone)
    SELECT NEWID(), i.Id, 'UPDATE', d.Name, i.Name, d.Address, i.Address, d.TelePhone, i.Telephone
    FROM inserted i
    JOIN deleted d ON i.Id = d.Id;
END;


--Trigger para checkagem de disponibilidade de consulta
CREATE TRIGGER trg_CheckAppointmentConflict
ON Appointments
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 
        FROM Appointments a
        JOIN inserted i ON a.DoctorId = i.DoctorId
        WHERE a.AppointmentDate = i.AppointmentDate
          AND a.Id != i.Id 
    )
    BEGIN
        RAISERROR('Doctor is already booked for this time', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;