�Ա�Ļ�������
һ����ѯ
select * from zhxx where zh > '00001' and xm like '%��%' order by id desc
order by position,money asc,top desc 
asc ���� desc ����

�������
insert into zhxx(zh,xm) values('A0002','����')

����ɾ��
delete from zhxx where xm = '�û�����'

�ġ��޸�
update czyqx
set czydh = 'A001' ,xm = '����' where qx = 1

�塢������
create table zhxx(zh char(20) not NUll,xm char(15),kl integer)

����ɾ����
drop table zhxx

�ߡ��޸ı�
����ֶ�
alter table zhxx
add yxx bit   
add zh varchar(20) null

ɾ���ֶ�
alter table zhxx
drop column zh 

�������
alter table zhxx
add primary key (zh)

�޸�ĳһ�е���������
alter table zhxx
modify kl nvarchar(20)

�����ݿ�Ļ�������
һ������
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

�����ָ����ݿ�
RESTORE DATABASE  wbglsql from disk = '\\127.0.0.1\��ѯ����\wbglsql.bak'
 With Move 'wbglsql_data' to 'c:\wbglsql_data.mdf',move 'wbglsql_log' to 'c:\wbglsql_log.ldf'

�����������ݿ�
Sp_detach_db wbglsql

�ġ��������ݿ�
sp_attach_db 'wbglsql','C:\wbglsql_data.mdf','C:\wbglsql_log.ldf'
ֻ�������ļ�û����־�ļ�
sp_attach_single_file_db 'wbglsql','C:\wbglsql_data.mdf'

�塢�������ݿ�
backup database wbglsql 
to disk ='c:\wbglsql.bak'













