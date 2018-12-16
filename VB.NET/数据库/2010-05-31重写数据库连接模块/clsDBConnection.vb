' Created by SharpDevelop.
' User: jiftle
' Date: 2010-4-9
' Time: 15:51
' 修改时间:2010-05-31 17:30
'----------------------------------------------------------------
'引用的命名空间
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
'----------------------------------------------------------------
Namespace DBConfig
    ''' <summary>
    ''' 数据库连接类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class clsDBConnection

        ''' <summary>
        ''' 连接字符串
        ''' </summary>
        ''' <remarks></remarks>
        Private strConnectionString As String = ""

        ''' <summary>
        ''' 通用连接对象 
        ''' </summary>
        ''' <remarks></remarks>
        Protected Conn As Object

        ''' <summary>
        ''' 数据库访问接口类型
        ''' </summary>
        ''' <remarks></remarks>
        Protected DataAccessType As enmDataAccessType

        ''' <summary>
        ''' 日志文件路径
        ''' </summary>
        ''' <remarks></remarks>
        Protected strLogFilePath As String = "C:\DBConfig.log"

        ''' <summary>
        ''' 数据库是否已经连接
        ''' </summary>
        ''' <remarks></remarks>
        Protected m_blnConnected As Boolean = False

        ''' <summary>
        ''' 连接字串
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
        ''' 连接状态
        ''' </summary>
        ''' <value></value>
        ''' <returns>True 已连接 False 没有连接</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ConnectedState() As Boolean
            Get
                Return m_blnConnected
            End Get
        End Property

        ''' <summary>
        ''' 数据库访问接口类型
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
        ''' 连接数据库
        ''' </summary>
        ''' <returns>成功，返回True；否则，返回 False</returns>
        ''' <remarks></remarks>
        Public Function Open() As Boolean

            Dim strErr As String = ""

            If DataAccessType = Nothing Then
                strErr = "Wrong DataBaseDriverType"
                ErrorHandler(strErr)
                Return False
            End If

            Try

                '实例化Connection类
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
                '打开数据库
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
        ''' 关闭数据库连接
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Close()
            If Not Conn Is Nothing Then
                '关闭连接
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
        ''' 写入日志
        ''' </summary>
        ''' <remarks>LOGFILE 是自定义的全局常量</remarks>
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
                '不处理

            End Try
        End Sub

        ''' <summary>
        ''' 构造函数
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' 构造函数
        ''' </summary>
        ''' <param name="strConnStr">连接字串</param>
        ''' <remarks>使用本构造函数后，可以不对ConnectionString属性赋值</remarks>
        Public Sub New(ByVal strConnStr As String)
            strConnectionString = strConnStr
        End Sub


    End Class
End Namespace