'定义公共的数据库连接对象
Public conn As New ADODB.Connection
Public g_dbType As Boolean  '是否是sql数据库
Dim IniFile As New ClsIniFile

'定义公共的连接函数
Public Function SqlConn() As Boolean
On Error GoTo myerr

    SqlConn = False
Dim ip As String, uid As String, pwd As String   'sql数据库连接参数


    
  If g_dbType = True Then
    ip = "127.0.0.1"
    uid = ""
    pwd = ""
    conn.ConnectionString = "Driver={SQL Server};server=" & ip & ";uid=" & uid & ";pwd=" & pwd & ";database=wbgl"
  Else
    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & App.Path & "\数据库\wbgl.mdb" & ";Jet OLEDB:DataBase password=jfmoonsun"
  End If
    conn.Open
      conn.CursorLocation = adUseClient
      
     '判断是否正常连接
     If conn.State = adStateClosed Then
            MsgBox "你没有正常连接数据库。", 0 + 16, "警告！", vbExclamation, App.Title
     End If
     
        SqlConn = True
     Exit Function
 
myerr:
  Call ShowError("ModSqlConn", "SqlConn()", Err.Number, Err.Description)
 
End Function