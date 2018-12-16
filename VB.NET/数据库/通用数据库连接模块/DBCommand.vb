'
' Created by SharpDevelop.
' User: jiftle
' Date: 2010-4-9
' Time: 15:51
' 
'����������ݿ���Ҫ�������ռ�
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

'�Զ��������ռ�DBConfig
Namespace DBConfig
    Public Class DBCommand
        Inherits DBConnection

        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <param name="strSQL"></param>
        ''' <returns>��Ӱ������� ���� -1 ��ʾִ��ʧ��</returns>
        ''' <remarks></remarks>
        Public Function ExecuteSQL(ByVal strSQL As String) As Integer
            Dim strErr As String
            Dim cmd

            '����SqlCommandʵ��            
            Select Case g_DataAccessType
                Case enmDataAccessType.DB_OLEDB
                    cmd = New OleDbCommand(strSQL, conn)
                Case enmDataAccessType.DB_ODBC
                    cmd = New OdbcCommand(strSQL, conn)
                Case enmDataAccessType.DB_SQL
                    cmd = New SqlCommand(strSQL, conn)
                Case enmDataAccessType.DB_MYSQL
                    cmd = New MySqlCommand(strSQL, conn)
                Case Else
                    cmd = New MySqlCommand(strSQL, conn)
            End Select

            'count ��ʾ��Ӱ�����������ʼ��Ϊ0 
            Dim count As Integer = 0

            Try
                'ִ��SQL����
                count = cmd.ExecuteNonQuery()

            Catch ex As OleDb.OleDbException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As Odbc.OdbcException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As SqlException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As MySqlException
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As SystemException
                strErr = Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            End Try

            Return count
        End Function


		''' <summary>
		''' ���ݲ�����sql��䣬����һ��DataTable
		''' </summary>
		''' <param name="strSQL">sql���eg."select * from zhTmp"</param>
		''' <returns>����DataTable���ͱ��� </returns>
        Public Function CreateDataTable(ByVal strSQL As String) As DataTable

            Dim daTmp
            Dim dtblTmp As DataTable = New DataTable

            Try

                Select Case g_DataAccessType
                    Case enmDataAccessType.DB_OLEDB
                        Dim connTmp As OleDbConnection
                        connTmp = New OleDbConnection
                        connTmp = conn
                        daTmp = New OleDbDataAdapter(strSQL, connTmp)

                    Case enmDataAccessType.DB_ODBC
                        Dim connTmp As OdbcConnection
                        connTmp = New OdbcConnection
                        connTmp = conn
                        daTmp = New OdbcDataAdapter(strSQL, connTmp)

                    Case enmDataAccessType.DB_SQL
                        Dim connTmp As SqlConnection
                        connTmp = New SqlConnection
                        connTmp = conn
                        daTmp = New SqlDataAdapter(strSQL, connTmp)

                    Case enmDataAccessType.DB_MYSQL
                        Dim connTmp As MySqlConnection
                        connTmp = New MySqlConnection
                        connTmp = conn
                        daTmp = New MySqlDataAdapter(strSQL, connTmp)

                    Case Else
                        Dim connTmp As MySqlConnection
                        connTmp = New MySqlConnection
                        connTmp = conn
                        daTmp = New MySqlDataAdapter(strSQL, connTmp)

                End Select


                Try
                    daTmp.Fill(dtblTmp)

                Catch ex As OleDb.OleDbException
                    Dim strErr As String = ""
                    strErr = ex.ErrorCode & Chr(32) & ex.Message
                    ErrorHandler(strErr)
                    Return Nothing
                Catch ex As Odbc.OdbcException
                    Dim strErr As String = ""
                    strErr = ex.ErrorCode & Chr(32) & ex.Message
                    ErrorHandler(strErr)
                    Return Nothing
                Catch ex As SqlException
                    Dim strErr As String = ""
                    strErr = ex.ErrorCode & Chr(32) & ex.Message
                    ErrorHandler(strErr)
                    Return Nothing
                Catch ex As MySqlException
                    Dim strErr As String = ""
                    strErr = ex.ErrorCode & Chr(32) & ex.Message
                    ErrorHandler(strErr)
                    Return Nothing
                Catch ex As MySql.Data.Types.MySqlConversionException
                    Dim strErr As String = ""
                    strErr = Chr(32) & ex.Message
                    ErrorHandler(strErr)
                    Return Nothing
                Catch ex As System.StackOverflowException
                    Dim strErr As String = ""
                    strErr = Chr(32) & ex.Message
                    ErrorHandler(strErr)
                    Return Nothing
                Catch ex As SystemException
                    Dim strErr As String = ""
                    strErr = Chr(32) & ex.Message
                    ErrorHandler(strErr)
                    Return Nothing
                End Try

                '����һ��DataTable(���Ա༭��)
                Return dtblTmp


            Catch ex As Exception
                Dim strErr As String = ""
                strErr = Chr(32) & ex.Message
                ErrorHandler(strErr)

                Return Nothing
            End Try


            Return dtblTmp
        End Function

    End Class
End Namespace
