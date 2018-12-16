'
' Created by SharpDevelop.
' User: jiftle
' Date: 2010-4-9
' Time: 15:51

'����������ݿ���Ҫ�������ռ�
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

'�Զ��������ռ�DBConfig
Namespace DBConfig
    Public Class DBConnection
        '����һ���ܵ����������洢�������ݿ����Ϣ
        Public ConnStr As String

        '�����������ݿ����ӵĹ�����Ա
        Public conn 'Ĭ����Object����


        ''' <summary>
        ''' �������ݿ�
        ''' </summary>
        Public Function Open() As Boolean

            Dim strErr As String = vbNullString

            '�ж������ַ����Ƿ�Ϊ��
            If ConnStr Is Nothing Or ConnStr = "" Then
                strErr = "Connection String can't be Empty!"
                ErrorHandler(strErr)
                Return False
            End If

            'ʵ����Connection��
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
                '�����ݿ�
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
        ''' �ر����ݿ�����
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Close()

            If Not conn Is Nothing Then
                '�ر�����
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

