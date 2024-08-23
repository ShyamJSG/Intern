Create table Employees(EmpId int,EmpName varchar(20),Password varchar(20))
Select * from Employees
Insert into Employees (EmpId,EmpName,Password) values
(1,'Shyam','12345'),(2,'Ram','123456'),(3,'Ganesh','1234567')


Create table Register(RegID int identity(1,1) primary key,UserName varchar(20),Password varchar(20),Email varchar(20),IsActive int)
Select * from Register

Create table Crud(Id int identity(1,1) primary key, Name varchar(20),Email varchar(20),IsActive int,CreatedOn DateTime)
Select * from Crud
