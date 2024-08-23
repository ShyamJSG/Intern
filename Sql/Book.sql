select * from Books
create table Users(UserID int primary key identity,Username nvarchar(20),Password nvarchar(20))
DBCC CHECKIDENT ('[Books]', RESEED, 3);
delete from Books where BookID in (5);

select * from Users
insert into Users (Username,Password) values('admin',1234)
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users';
