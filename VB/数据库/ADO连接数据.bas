 
 
 '=================	 sql		========================
  conn.ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;Initial Catalog=wbglsql;Data Source=.;Integrated Security=SSPI"
'    MsgBox ip
'    conn.ConnectionString = "Driver={SQL Server};server=" & ip & ";uid=" & uid & ";pwd=" & pwd & ";database=wbglsql"


 '=================	 Access		========================
    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & App.Path & "\数据库\student.mdb" & ";Jet OLEDB:DataBase password=001"

'=================	 MySQL		========================   
 'CREATE CONNECTION OBJECT AND ASSIGN CONNECTION STRING
Dim conn As ADODB.Connection
Set conn = New ADODB.Connection

conn.ConnectionString = "DRIVER={MySQL ODBC 5.1 Driver};" _
            & "SERVER=192.168.100.222;" _
            & "DATABASE=test;" _
            & "UID=yqsnew;" _
            & "PWD=yqs;" _
            & "port = 3306"
           ' & "OPTION=" & 1 + 2 + 8 + 32 + 2048 + 16384 & _

conn.CursorLocation = adUseClient
conn.Open

'conn.Execute "create table zhxx(zh char(20) not NUll,xm char(15),kl integer)"
conn.Execute "insert into zhxx(zh,xm) values('A0002','王二')"

Dim rs As New ADODB.Recordset

rs.Open "select * from zhxx", conn, adOpenKeyset, adLockOptimistic