insert into library(bookno,bookname,author) values/*(1,'sas','ddda'),(2,'sasa','fsrt'),*/(3,'fggr','hjgjg')
select * from library
update library set bookname='fhfhf',author ='hghg' where bookno=1
delete from library where bookno =3
select count(*) from library
select * from library where bookname like 'f%'
select bookname as Book from library
create table login(username varchar(20),password varchar(20))
select * from login
insert into login(loginid,username,password) values(1,'admin','1234'),(2,'sasa','fsrt'),(3,'fggr','hjgjg')
select library.bookname ,login.username from library full join login on library.bookname = login.username
select bookname from library union select username from login
create table login_db(loginid int,username varchar(20),password varchar(20))
insert into login_db(username,password) select username,password from login
select * from login
drop table login
exec sp_rename 'login_db','login'
insert into login(loginid) values (1),(2),(3)
alter table library add constraint keyy foreign key (bookno) references login(loginid)
delete from login where loginid IS NULL
alter table login alter column loginid int not null
alter table login add primary key(loginid)
create view [customer id >1] as select bookno,bookname,author from library where bookno>1
select * from [customer id >1]

