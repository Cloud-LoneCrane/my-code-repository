MySql�����ַ����ܽ�
2009-10-12 08:08MySQL Connector/ODBC 2.50 (MyODBC 2.50)���ӷ�ʽ
�������ݿ������������﷨��ʽ��
Driver={mySQL};Server=localhost;Option=16834;Database=myDataBase;

Զ���������� �������﷨��ʽ��
Driver={mySQL};Server=myServerAddress;Option=131072;Stmt=;Database=myDataBase; User=myUsername;Password=myPassword;

�����TCP/IP�˿������������﷨��ʽ��
Driver={mySQL};Server=myServerAddress;Port=3306;Option=131072;Stmt=;Database=myDataBase; User=myUsername;Password=myPassword;

     ˵������Driver��Ĭ�϶˿���3306�����û���������ַ������ر�ָ����������Mysql��3306�˿ڡ�
MySQL Connector/ODBC 3.51 (MyODBC 3.51)���ӷ�ʽ
�������ݿ������������﷨��ʽ��
Driver={MySQL ODBC 3.51 Driver};Server=localhost;Database=myDataBase; User=myUsername;Password=myPassword;Option=3;

Զ���������� �������﷨��ʽ��
Driver={MySQL ODBC 3.51 Driver};Server=data.domain.com;Database=myDataBase;User=myUsername; Password=myPassword;Option=3;

�����TCP/IP�˿������������﷨��ʽ��
Driver={MySQL ODBC 3.51 Driver};Server=data.domain.com;Port=3306;Database=myDataBase;User=myUsername; Password=myPassword;Option=3;

     ˵������Driver��Ĭ�϶˿���3306�����û���������ַ������ر�ָ����������Mysql��3306�˿ڡ�
�����ַ����������������﷨��ʽ��
Driver={MySQL ODBC 3.51 Driver};Server=data.domain.com;charset=UTF8;Database=myDataBase;User=myUsername; Password=myPassword;Option=3;

OLE DB, OleDbConnection (.NET)���ӷ�ʽ
��׼�����������﷨��ʽ��
Provider=MySQLProv;Data Source=mydb;User Id=myUsername;Password=myPassword;

MySQL Connector/Net (.NET)���ӷ�ʽ
��׼�����������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

    Ĭ�϶˿���3306.
�����TCP/IP�˿������������﷨��ʽ��
Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

�����ܵ��������﷨��ʽ��
Server=myServerAddress;Port=-1;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

     ˵�����˿�ֵΪ-1��˵���������ܵ���ʽ���ӡ��˷�ʽֻ��Windows����Ч����UNIX���ûᱻ���ԡ�
�����������
   �ô��ַ�ʽ���ӵ����ݿ��У����ص��ĸ�ʹ���ĸ����ݿ⡣�������﷨��ʽ��
Server=serverAddress1 & serverAddress2 & etc..;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

����ѡ��
    �������SSL���Ӽ������пͻ��˺ͷ������̵����ݴ��䡣���ҷ�����Ҫ��һ��֤�顣�������﷨��ʽ��
Server=myServerAddress;Port=-1;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

     ���ѡ��� Connector/NET5.0.3�濪ʼ���֣���ǰ�İ汾����û�д˹��ܡ�
�޸�Ĭ�ϵ����ʱʱ��
     ʹ�������޸����ӵ�Ĭ�����ʱʱ�䡣ע�⣺��������Ӱ�����ڵ���������������õĳ�ʱʱ�䡣 �������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;default command timeout=20;

   ����ֻ��Connector/NET 5.1.4 �����ϵİ汾��Ч.
�޸����ӳ���ʱ��
   ʹ�������޸�����ֹ���Ժͽ��մ���ĵȴ�ʱ�䣨����Ϊ��λ�� �������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Connection Timeout=5;

Inactivating prepared statements    
    Use this one to instruct the provider to ignore any command prepare statements and prevent corruption issues with server side prepared statements. �������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Ignore Prepare=true;

    ��ѡ����뵽Connector/NET��5.0.3���1.0.9�档
�����TCP/IP�˿�����
   ��������޸����ӵĶ˿ڡ��������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Port=3306;

   Ĭ�϶˿���3306���˲����ᱻUnix���ԡ�
��������Э��
     ��������޸�������Э��������ӡ� �������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Protocol=socket;

   ���û���ر�˵����"socket"��Ĭ�ϵ�ֵ��"tcp"����"socket"��ͬ����ġ�"pipe"��ʹ�������ܵ����ӣ�"unix"��ʹ��unix socket���ӣ�"memory"��ʹ��mySql�Ĺ����ڴ档
�����ַ���������
    ������ָ����ʹ���ַ������뷢�͵��������ϵĲ�ѯ��䡣�������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;CharSet=UTF8;

   ע�⣺��ѯ�����Ȼ���Է������ݵĸ�ʽ���͡�
�޸Ĺ����ڴ���
    ����������޸�����ͨ�ŵĹ����ڴ����ơ� �������﷨��ʽ��
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Shared Memory Name=MYSQL;

   ˵����    �����ֻ�е�����Э������Ϊ"memory"ʱ����Ч��
MySqlConnection (.NET)���ӷ�ʽ
   eInfoDesigns.dbProvider �������﷨��ʽ��
Data Source=myServerAddress;Database=myDataBase;User ID=myUsername;Password=myPassword;Command Logging=false;

SevenObjects MySqlClient (.NET)���ӷ�ʽ
   ��׼�����������﷨��ʽ��
Host=myServerAddress;UserName=myUsername;Password=myPassword;Database=myDataBase;

Core Labs MySQLDirect (.NET)���ӷ�ʽ
   ��׼���� �������﷨��ʽ��
User ID=root;Password=myPassword;Host=localhost;Port=3306;Database=myDataBase; Direct=true;Protocol=TCP;Compress=false;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;

MySQLDriverCS (.NET)���ӷ�ʽ
��׼���� �������﷨��ʽ��
Location=myServerAddress;Data Source=myDataBase;User ID=myUsername;Password=myPassword;Port=3306;Extended Properties="""";