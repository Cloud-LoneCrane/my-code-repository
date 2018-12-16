对表的基本操作
一、查询
select * from zhxx where zh > '00001' and xm like '%用%' order by id desc
order by position,money asc,top desc 
asc 升序 desc 降序

二、添加
insert into zhxx(zh,xm) values('A0002','王二')

三、删除
delete from zhxx where xm = '用户姓名'

四、修改
update czyqx
set czydh = 'A001' ,xm = '测试' where qx = 1

五、创建表
create table zhxx(zh char(20) not NUll,xm char(15),kl integer)

六、删除表
drop table zhxx

七、修改表
添加字段
alter table zhxx
add yxx bit   
add zh varchar(20) null

删除字段
alter table zhxx
drop column zh 

添加主码
alter table zhxx
add primary key (zh)

修改某一列的数据类型
alter table zhxx
modify kl nvarchar(20)

对数据库的基本操作
一、创建
create database mytest
on 
(name = wbgl,
filename = 'C:\wbgl.mdf',
size = 10,
maxsize = 30,
filegrowth = 5)
log on 
(name = wbgl_log,
filename = 'c:\wbgl_log.ldf',
size = 3,
maxsize = 10,
filegrowth = 2) 

二、恢复数据库
RESTORE DATABASE  wbglsql from disk = '\\127.0.0.1\查询密码\wbglsql.bak'
 With Move 'wbglsql_data' to 'c:\wbglsql_data.mdf',move 'wbglsql_log' to 'c:\wbglsql_log.ldf'

三、分离数据库
Sp_detach_db wbglsql

四、附加数据库
sp_attach_db 'wbglsql','C:\wbglsql_data.mdf','C:\wbglsql_log.ldf'
只有数据文件没有日志文件
sp_attach_single_file_db 'wbglsql','C:\wbglsql_data.mdf'

五、备份数据库
backup database wbglsql 
to disk ='c:\wbglsql.bak'













