create table users(UserId int primary key identity,Username NVARCHAR(50) NOT NULL,Password NVARCHAR(256) NOT NULL);
select * from users;
insert into users(Username,Password)values('testuser','testpassword');
delete from users

create table Books(BookId int primary key identity,Title nvarchar(50)not null,Author nvarchar(50)not null);
select * from Books;
ALTER TABLE Books ADD Category varchar(50)
DELETE FROM Books;
Drop table Books;

create table Issue(IssueID int Primary Key,Ititle Varchar(20),Iauthor Varchar(20))
Insert into Issue(IssueID,Ititle,Iauthor,IssuePeriod,Due) values(1,'testtitle','testauthor','2 Months',1)
select * from Issue
ALTER TABLE Issue ADD IssuePeriod varchar(20), Due BIT
