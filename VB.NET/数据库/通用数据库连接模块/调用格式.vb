
''' <summary>
''' ���ݿ����Ӷ���
''' </summary>
''' <remarks></remarks>
Public g_Conn As DBCommand

''' <summary>
''' �������ݿ�
''' </summary>
''' <remarks></remarks>
Public Sub ConnMySql()
  '���Ӷ���
        g_Conn = New DBConfig.DBCommand

        '�����ݿ������
        Dim strServer, strUserID, strPwd, strDBName As String

        'strServer = "localhost"
        'strUserID = "root"
        'strPwd = "jift"
        'strDBName = "yqsnew"

        strServer = "192.168.100.222"
        strUserID = "yqsnew"
        strPwd = "yqs"
        strDBName = "yqsnew"
        
 'MySql .net����
 g_DataAccessType = enmDataAccessType.DB_MYSQL
        g_Conn.ConnStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", _
        strServer, strUserID, strPwd, strDBName)
        
 'ODBC      
        'g_DataAccessType = enmDataAccessType.DB_ODBC
        'g_Conn.ConnStr = "Driver={MySQL ODBC 5.1 Driver};Server=192.168.100.222;Database=yqsnew;User=yqsnew; Password=yqs;Option=3;"

        If g_Conn.Open() = False Then '����������
            MessageBox.Show("���ݿ�����ʧ��", "���Գ���", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        
End Sub        