' Created by SharpDevelop
' User: jifle
' Date: 2010-4-9
' Time: 15:51
' �޸�ʱ��:2010-05-31 17:30
' �޸����ݣ�
'       ��������DBConnection��DBCommand�࣬����modTypesģ���modFuncsģ��
' -------------------------------------------------------------------
'����������ݿ���Ҫ�������ռ�
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
' -------------------------------------------------------------------

' �Զ��������ռ�DBConfig
Namespace DBConfig
    ''' <summary>
    ''' ͨ��Commad�࣬��������sql����ִ��
    ''' </summary>
    ''' <remarks></remarks>
    Public Class clsDBCommand
        Inherits clsDBConnection

        ''' <summary>
        ''' �洢��һ�ε�DataReader�������û����ǹر�DataReader�������쳣
        ''' </summary>
        ''' <remarks></remarks>
        Private m_DataReader As Object

        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <param name="strSQL"></param>
        ''' <returns>��Ӱ������� ���� -1 ��ʾִ��ʧ��</returns>
        ''' <remarks></remarks>
        Public Function ExecuteSQL(ByVal strSQL As String) As Integer
            Dim strErr As String
            Dim cmd As Object

            Try
                '����SqlCommandʵ��            
                Select Case DataAccessType
                    Case enmDataAccessType.DB_OLEDB
                        cmd = New OleDbCommand(strSQL, Conn)
                    Case enmDataAccessType.DB_ODBC
                        cmd = New OdbcCommand(strSQL, Conn)
                    Case enmDataAccessType.DB_SQL
                        cmd = New SqlCommand(strSQL, Conn)
                    Case enmDataAccessType.DB_MYSQL
                        cmd = New MySqlCommand(strSQL, Conn)
                    Case Else
                        cmd = New MySqlCommand(strSQL, Conn)
                End Select
            Catch ex As Exception
                ErrorHandler(ex.ToString)
                Return -1
            End Try
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
        ''' <returns>����DataTable���ͱ��� ,�����Ҫ�����ݿ������ݽ��иı䣬��ʹ��DataAdapterUpdate���и���</returns>
        Public Function CreateDataTable(ByVal strSQL As String) As DataTable

            Dim daTmp As Object
            Dim dtblTmp As DataTable = New DataTable

            Try

                Select Case DataAccessType
                    Case enmDataAccessType.DB_OLEDB
                        Dim connTmp As OleDbConnection
                        connTmp = New OleDbConnection
                        connTmp = Conn
                        daTmp = New OleDbDataAdapter(strSQL, connTmp)

                        daTmp.Fill(dtblTmp)
                    Case enmDataAccessType.DB_ODBC
                        Dim connTmp As OdbcConnection
                        connTmp = New OdbcConnection
                        connTmp = Conn
                        daTmp = New OdbcDataAdapter(strSQL, connTmp)

                        daTmp.Fill(dtblTmp)
                    Case enmDataAccessType.DB_SQL
                        Dim connTmp As SqlConnection
                        connTmp = New SqlConnection
                        connTmp = Conn
                        daTmp = New SqlDataAdapter(strSQL, connTmp)

                        daTmp.Fill(dtblTmp)
                    Case enmDataAccessType.DB_MYSQL
                        Dim connTmp As MySqlConnection
                        connTmp = New MySqlConnection
                        connTmp = Conn
                        daTmp = New MySqlDataAdapter(strSQL, connTmp)

                        Try
                            daTmp.Fill(dtblTmp)
                        Catch ex As MySqlException
                            '�п�������Ϊ�ϴ�����û�йر�
                            Try
                                CType(m_DataReader, MySqlDataReader).Close()
                                daTmp.Fill(dtblTmp)
                                Return dtblTmp
                            Catch exx As SystemException
                                ErrorHandler(ex.ToString)
                                Return Nothing
                            End Try
                        Catch ex As Exception
                            ErrorHandler(ex.ToString)
                            Return Nothing
                        End Try
                    Case Else
                            Dim connTmp As MySqlConnection
                            connTmp = New MySqlConnection
                            connTmp = Conn
                            daTmp = New MySqlDataAdapter(strSQL, connTmp)

                            daTmp.Fill(dtblTmp)
                End Select

            Catch ex As OleDb.OleDbException
                Dim strErr As String = ""
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return Nothing
            Catch ex As Odbc.OdbcException
                Dim strErr As String = ""
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return Nothing
            Catch ex As SqlException
                Dim strErr As String = ""
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return Nothing
            Catch ex As MySql.Data.Types.MySqlConversionException
                Dim strErr As String = ""
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return Nothing
            Catch ex As System.StackOverflowException
                Dim strErr As String = ""
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return Nothing
            Catch ex As SystemException
                Dim strErr As String = ""
                strErr = ex.ToString
                ErrorHandler(strErr)
                Return Nothing
            End Try

            '����һ��DataTable
            Return dtblTmp

        End Function

        ''' <summary>
        ''' ����MySqlDataReader
        ''' </summary>
        ''' <param name="strSQL">��ѯ���</param>
        ''' <returns></returns>
        ''' <remarks>ʹ����Ϻ��������ر�DataReader MySqlDataReader.Close</remarks>
        Public Function CreateMySqlDataReader(ByVal strSQL As String) As MySqlDataReader
            Try
                Select Case DataAccessType
                    Case enmDataAccessType.DB_MYSQL
                        Dim cmd As MySqlCommand
                        Dim Reader As MySqlDataReader

                        cmd = New MySqlCommand(strSQL, CType(Conn, MySqlConnection))
                        Reader = cmd.ExecuteReader()

                        m_DataReader = Reader
                        Return Reader
                End Select
            Catch ex As Exception
                ErrorHandler(ex.ToString)
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' ����ODBCDataReader
        ''' </summary>
        ''' <param name="strSQL">��ѯ���</param>
        ''' <returns></returns>
        ''' <remarks>MySqlҲ������</remarks>
        Public Function CreateODBCDataReader(ByVal strSQL As String) As OdbcDataReader
            Try
                Select Case DataAccessType
                    Case enmDataAccessType.DB_ODBC
                        Dim cmd As OdbcCommand
                        Dim Reader As OdbcDataReader

                        cmd = New OdbcCommand(strSQL, CType(Conn, OdbcConnection))
                        Reader = cmd.ExecuteReader()
                        Return Reader
                End Select
            Catch ex As Exception
                ErrorHandler(ex.ToString)
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' ����DataSet
        ''' </summary>
        ''' <param name="strSQL"></param>
        ''' <returns></returns>
        ''' <remarks>����һ�����DataSet</remarks>
        Public Function CreateDataSet(ByVal strSQL As String) As DataSet
            Try
                Dim dsTmp As New DataSet
                Dim Adapter As New MySqlDataAdapter()
                Adapter.SelectCommand = New MySqlCommand(strSQL, CType(Conn, MySqlConnection))
                Adapter.Fill(dsTmp)
                Return dsTmp
            Catch ex As Exception
                ErrorHandler(ex.ToString)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' ����DataSet
        ''' </summary>
        ''' <param name="strtSqlItemArray">��ѯԭ�ӵĽṹ������</param>
        ''' <returns></returns>
        ''' <remarks>����������DataSet</remarks>
        Public Function CreateDataSet(ByVal strtSqlItemArray() As strtSqlItem) As DataSet
            Try
                Dim dsTmp As New DataSet

                For Each tmp As strtSqlItem In strtSqlItemArray
                    Dim Adapter As New MySqlDataAdapter()
                    Adapter.SelectCommand = New MySqlCommand(tmp.strSQL, CType(Conn, MySqlConnection))
                    Adapter.Fill(dsTmp, tmp.strTableName)
                Next

                Return dsTmp
            Catch ex As Exception
                ErrorHandler(ex.ToString)
                Return Nothing
            End Try
        End Function


    End Class
End Namespace
