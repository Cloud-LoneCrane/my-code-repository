'
' Created by SharpDevelop.
' User: jiftle
' Date: 2010-4-9
' Time: 15:51

'导入操作数据库需要的命名空间
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

'自定义命名空间DBConfig
Namespace DBConfig
    Public Class DBConnection
        '声明一个受到保护变量存储连接数据库的信息
        Public ConnStr As String

        '声明用于数据库连接的公共成员
        Public conn '默认是Object类型


        ''' <summary>
        ''' 连接数据库
        ''' </summary>
        Public Function Open() As Boolean

            Dim strErr As String = vbNullString

            '判断连接字符串是否为空
            If ConnStr Is Nothing Or ConnStr = "" Then
                strErr = "Connection String can't be Empty!"
                ErrorHandler(strErr)
                Return False
            End If

            '实例化Connection类
            If g_DataAccessType = enmDataAccessType.DB_OLEDB Then
                conn = New OleDbConnection(ConnStr)
            ElseIf g_DataAccessType = enmDataAccessType.DB_ODBC Then
                conn = New OdbcConnection(ConnStr)
            ElseIf g_DataAccessType = enmDataAccessType.DB_SQL Then
                conn = New SqlConnection(ConnStr)
            ElseIf g_DataAccessType = enmDataAccessType.DB_MYSQL Then
                conn = New MySqlConnection(ConnStr)
            Else
                conn = New MySqlConnection(ConnStr)
            End If



            Try
                '打开数据库
                conn.Open()
            Catch ex As OleDb.OleDbException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return False
            Catch ex As Odbc.OdbcException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return False
            Catch ex As SqlException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return False
            Catch ex As MySqlException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return False
            Catch ex As SystemException
                strErr = Chr(32) & ex.Message
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

            If Not conn Is Nothing Then
                '关闭连接
                conn.Close()
                conn = Nothing
            End If
        End Sub

        Public Sub New()

        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace

