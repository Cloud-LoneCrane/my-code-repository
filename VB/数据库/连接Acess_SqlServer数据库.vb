'���幫�������ݿ����Ӷ���
Public conn As New ADODB.Connection
Public g_dbType As Boolean  '�Ƿ���sql���ݿ�
Dim IniFile As New ClsIniFile

'���幫�������Ӻ���
Public Function SqlConn() As Boolean
On Error GoTo myerr

    SqlConn = False
Dim ip As String, uid As String, pwd As String   'sql���ݿ����Ӳ���


    
  If g_dbType = True Then
    ip = "127.0.0.1"
    uid = ""
    pwd = ""
    conn.ConnectionString = "Driver={SQL Server};server=" & ip & ";uid=" & uid & ";pwd=" & pwd & ";database=wbgl"
  Else
    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & App.Path & "\���ݿ�\wbgl.mdb" & ";Jet OLEDB:DataBase password=jfmoonsun"
  End If
    conn.Open
      conn.CursorLocation = adUseClient
      
     '�ж��Ƿ���������
     If conn.State = adStateClosed Then
            MsgBox "��û�������������ݿ⡣", 0 + 16, "���棡", vbExclamation, App.Title
     End If
     
        SqlConn = True
     Exit Function
 
myerr:
  Call ShowError("ModSqlConn", "SqlConn()", Err.Number, Err.Description)
 
End Function