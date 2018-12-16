
''' <summary>
''' 数据库连接对象
''' </summary>
''' <remarks></remarks>
Public g_Conn As DBCommand

''' <summary>
''' 连接数据库
''' </summary>
''' <remarks></remarks>
Public Sub ConnMySql()
  '连接对象
        g_Conn = New DBConfig.DBCommand

        '打开数据库的连接
        Dim strServer, strUserID, strPwd, strDBName As String

        'strServer = "localhost"
        'strUserID = "root"
        'strPwd = "jift"
        'strDBName = "yqsnew"

        strServer = "192.168.100.222"
        strUserID = "yqsnew"
        strPwd = "yqs"
        strDBName = "yqsnew"
        
 'MySql .net驱动
 g_DataAccessType = enmDataAccessType.DB_MYSQL
        g_Conn.ConnStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", _
        strServer, strUserID, strPwd, strDBName)
        
 'ODBC      
        'g_DataAccessType = enmDataAccessType.DB_ODBC
        'g_Conn.ConnStr = "Driver={MySQL ODBC 5.1 Driver};Server=192.168.100.222;Database=yqsnew;User=yqsnew; Password=yqs;Option=3;"

        If g_Conn.Open() = False Then '打开数据连接
            MessageBox.Show("数据库连接失败", "测试程序", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        
End Sub        