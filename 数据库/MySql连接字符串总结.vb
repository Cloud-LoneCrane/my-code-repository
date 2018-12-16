MySql连接字符串总结
2009-10-12 08:08MySQL Connector/ODBC 2.50 (MyODBC 2.50)连接方式
本地数据库连接以下是语法格式：
Driver={mySQL};Server=localhost;Option=16834;Database=myDataBase;

远程数据连接 以下是语法格式：
Driver={mySQL};Server=myServerAddress;Option=131072;Stmt=;Database=myDataBase; User=myUsername;Password=myPassword;

特殊的TCP/IP端口连接以下是语法格式：
Driver={mySQL};Server=myServerAddress;Port=3306;Option=131072;Stmt=;Database=myDataBase; User=myUsername;Password=myPassword;

     说明：此Driver的默认端口是3306。如果没有在连接字符串中特别指出就是连接Mysql的3306端口。
MySQL Connector/ODBC 3.51 (MyODBC 3.51)连接方式
本地数据库连接以下是语法格式：
Driver={MySQL ODBC 3.51 Driver};Server=localhost;Database=myDataBase; User=myUsername;Password=myPassword;Option=3;

远程数据连接 以下是语法格式：
Driver={MySQL ODBC 3.51 Driver};Server=data.domain.com;Database=myDataBase;User=myUsername; Password=myPassword;Option=3;

特殊的TCP/IP端口连接以下是语法格式：
Driver={MySQL ODBC 3.51 Driver};Server=data.domain.com;Port=3306;Database=myDataBase;User=myUsername; Password=myPassword;Option=3;

     说明：此Driver的默认端口是3306。如果没有在连接字符串中特别指出就是连接Mysql的3306端口。
特殊字符集的连接以下是语法格式：
Driver={MySQL ODBC 3.51 Driver};Server=data.domain.com;charset=UTF8;Database=myDataBase;User=myUsername; Password=myPassword;Option=3;

OLE DB, OleDbConnection (.NET)连接方式
标准连接以下是语法格式：
Provider=MySQLProv;Data Source=mydb;User Id=myUsername;Password=myPassword;

MySQL Connector/Net (.NET)连接方式
标准连接以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

    默认端口是3306.
特殊的TCP/IP端口连接以下是语法格式：
Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

命名管道以下是语法格式：
Server=myServerAddress;Port=-1;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

     说明：端口值为-1，说明用命名管道方式连接。此方式只在Windows下有效，在UNIX下用会被忽略。
多服务器连接
   用此种方式连接到数据库中，不必担心该使用哪个数据库。以下是语法格式：
Server=serverAddress1 & serverAddress2 & etc..;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

加密选项
    这条活动的SSL连接加密所有客户端和服务器商的数据传输。而且服务器要有一个证书。以下是语法格式：
Server=myServerAddress;Port=-1;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

     这个选项从 Connector/NET5.0.3版开始出现，以前的版本中则没有此功能。
修改默认的命令超时时间
     使用这条修改连接的默认命令超时时间。注意：此条不会影响你在单独命令对象上设置的超时时间。 以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;default command timeout=20;

   此条只对Connector/NET 5.1.4 及以上的版本有效.
修改连接偿试时间
   使用这条修改在终止重试和接收错误的等待时间（以秒为单位） 以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Connection Timeout=5;

Inactivating prepared statements    
    Use this one to instruct the provider to ignore any command prepare statements and prevent corruption issues with server side prepared statements. 以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Ignore Prepare=true;

    此选项被加入到Connector/NET的5.0.3版和1.0.9版。
特殊的TCP/IP端口连接
   这条语句修改连接的端口。以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Port=3306;

   默认端口是3306。此参数会被Unix忽略。
特殊网络协议
     这条语句修改用哪种协议进行连接。 以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Protocol=socket;

   如果没有特别说明，"socket"是默认的值。"tcp"是与"socket"相同意义的。"pipe"是使用命名管道连接，"unix"是使用unix socket连接，"memory"是使用mySql的共享内存。
特殊字符集的连接
    这个语句指出以使种字符串编码发送到服务器上的查询语句。以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;CharSet=UTF8;

   注意：查询结果仍然是以反回数据的格式传送。
修改共享内存名
    此语句用来修改用来通信的共享内存名称。 以下是语法格式：
Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Shared Memory Name=MYSQL;

   说明：    此语句只有当连接协议设置为"memory"时才有效。
MySqlConnection (.NET)连接方式
   eInfoDesigns.dbProvider 以下是语法格式：
Data Source=myServerAddress;Database=myDataBase;User ID=myUsername;Password=myPassword;Command Logging=false;

SevenObjects MySqlClient (.NET)连接方式
   标准连接以下是语法格式：
Host=myServerAddress;UserName=myUsername;Password=myPassword;Database=myDataBase;

Core Labs MySQLDirect (.NET)连接方式
   标准连接 以下是语法格式：
User ID=root;Password=myPassword;Host=localhost;Port=3306;Database=myDataBase; Direct=true;Protocol=TCP;Compress=false;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;

MySQLDriverCS (.NET)连接方式
标准连接 以下是语法格式：
Location=myServerAddress;Data Source=myDataBase;User ID=myUsername;Password=myPassword;Port=3306;Extended Properties="""";