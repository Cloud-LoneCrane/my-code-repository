' Created by SharpDevelop.
' User: jiftle
' Date: 2010-4-9
' Time: 15:51
' �޸�ʱ��:2010-05-31 17:30
'----------------------------------------------------------------
'���õ������ռ�
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
'----------------------------------------------------------------
Namespace DBConfig
    ''' <summary>
    ''' ���ݿ�������
    ''' </summary>
    ''' <remarks></remarks>
    Public Class clsDBConnection

        ''' <summary>
        ''' �����ַ���
        ''' </summary>
        ''' <remarks></remarks>
        Private strConnectionString As String = ""

        ''' <summary>
        ''' ͨ�����Ӷ��� 
        ''' </summary>
        ''' <remarks></remarks>
        Protected Conn As Object

        ''' <summary>
        ''' ���ݿ���ʽӿ�����
        ''' </summary>
        ''' <remarks></remarks>
        Protected DataAccessType As enmDataAccessType

        ''' <summary>
        ''' ��־�ļ�·��
        ''' </summary>
        ''' <remarks></remarks>
        Protected strLogFilePath As String = "C:\DBConfig.log"

        ''' <summary>
        ''' ���ݿ��Ƿ��Ѿ�����
        ''' </summary>
        ''' <remarks></remarks>
        Protected m_blnConnected As Boolean = False

        ''' <summary>
        ''' �����ִ�
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ConnectionString() As String
            Get
                Return strConnectionString
            End Get
            Set(ByVal value As String)
                If value = "" Then
                    Return
                End If
                strConnectionString = value
            End Set
        End Property

        ''' <summary>
        ''' ����״̬
        ''' </summary>
        ''' <value></value>
        ''' <returns>True ������ False û������</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ConnectedState() As Boolean
            Get
                Return m_blnConnected
            End Get
        End Property

        ''' <summary>
        ''' ���ݿ���ʽӿ�����
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DataBaseDriverType() As enmDataAccessType
            Get
                Return DataAccessType
            End Get
            Set(ByVal Value As enmDataAccessType)
                If System.Enum.GetName(Value.GetType, Value) Is Nothing Then
                    DataAccessType = Nothing
                    Return
                End If
                DataAccessType = Value
            End Set
        End Property

        ''' <summary>
        ''' �������ݿ�
        ''' </summary>
        ''' <returns>�ɹ�������True�����򣬷��� False</returns>
        ''' <remarks></remarks>
        Public Function Open() As Boolean

            Dim strErr As String = ""

            If DataAccessType = Nothing Then
                strErr = "Wrong DataBaseDriverType"
                ErrorHandler(strErr)
                Return False
            End If

            Try

                'ʵ����Connection��
                If DataAccessType = enmDataAccessType.DB_OLEDB Then
                    Conn = New OleDbConnection(strConnectionString)
                ElseIf DataAccessType = enmDataAccessType.DB_ODBC Then
                    Conn = New OdbcConnection(strConnectionString)
                ElseIf DataAccessType = enmDataAccessType.DB_SQL Then
                    Conn = New SqlConnection(strConnectionString)
                ElseIf DataAccessType = enmDataAccessType.DB_MYSQL Then
                    Conn = New MySqlConnection(strConnectionString)
                Else
                    Conn = New MySqlConnection(strConnectionString)
                End If
            Catch ex As Exception
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return False
            End Try

            Try
                '�����ݿ�
                Conn.Open()
                m_blnConnected = True

            Catch ex As OleDb.OleDbException
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return False
            Catch ex As Odbc.OdbcException
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return False
            Catch ex As SqlException
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return False
            Catch ex As MySqlException
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return False
            Catch ex As SystemException
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' �ر����ݿ�����
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Close()
            If Not Conn Is Nothing Then
                '�ر�����
                Select Case Conn.GetType.Name
                    Case "OleDbConnection", "OdbcConnection", "SqlConnection", "MySqlConnection"
                        Conn.Close()
                    Case Else
                        Conn = Nothing
                End Select
                m_blnConnected = True
            End If
            m_blnConnected = True
        End Sub


        ''' <summary>
        ''' д����־
        ''' </summary>
        ''' <remarks>LOGFILE ���Զ����ȫ�ֳ���</remarks>
        Protected Sub ErrorHandler(ByVal strErrdescribe As String)
            Try
#If DEBUG Then
                MessageBox.Show(strErrdescribe, Me.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
#Else
            Dim swTmp As System.IO.StreamWriter = System.IO.File.AppendText(strLogFilePath)
            swTmp.WriteLine(Now & vbTab & strErrdescribe)
            swTmp.Close()
            swTmp = Nothing
#End If
            Catch ex As Exception
                '������

            End Try
        End Sub

        ''' <summary>
        ''' ���캯��
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' ���캯��
        ''' </summary>
        ''' <param name="strConnStr">�����ִ�</param>
        ''' <remarks>ʹ�ñ����캯���󣬿��Բ���ConnectionString���Ը�ֵ</remarks>
        Public Sub New(ByVal strConnStr As String)
            strConnectionString = strConnStr
        End Sub


    End Class
End Namespace