select * from Patient
select * from Doctor
select * from Appointment
select * from OfficeStaff

truncate table Appointment
truncate table Patient
insert into OfficeStaff values('Asoke','Tim','Asoke','As@123')

 

create Table Patient (
PatientID int primary key not null identity(1,2),
PatientFirstName varchar(50) not null,
PatientLastName varchar(50) not null,
Sex varchar(20) not null ,
Age int not null,
DataOfBirth varchar(50) not null
)



create Table Doctor (
DoctorID int primary key identity(1,3) not null,
DoctorFirstName varchar(50) not null,
DoctorLastName varchar(50) not null,
Sex varchar(50) not null,
DoctorSpec varchar(50) not null ,
VisitingHrs int not null
)




create table Appointment(
AppID int not null identity(1,3),
DocID int FOREIGN KEY REFERENCES Doctor(DoctorID),
PatientID int FOREIGN KEY REFERENCES Patient(PatientID),
DataOfApp varchar(50) not null,
Slot int not null
)

 

create table OfficeStaff (
OS int identity(1,3),
FirstName varchar(50),
LastName varchar(50),
UserName varchar(50),
Password varchar(50),
)


select * from Patient
select * from Appointment
select * from Doctor

insert into Doctor values('Asoke','Sain','M','Ophthalmology',6)
insert into Appointment values(7,31,'8/6/2022',4)


