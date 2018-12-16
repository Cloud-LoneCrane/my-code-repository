' Created by VS2005.
' User: jifle
' Date: 2010-4-9
' Time: 15:51
' �޸�ʱ��:2010-05-31 17:30
' �޸����ݣ�
'       ��������DBConnection��DBCommand�࣬����modTypesģ���modFuncsģ��
'-------------------------------------------------------------------------------
'���������ռ�
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.Data.OleDb
'-------------------------------------------------------------------------------
''' <summary>
''' ���ú���ģ��
''' </summary>
''' <remarks></remarks>
Public Module modFuncs

    Public Function ReaderToTable(ByVal Reader As SqlClient.SqlDataReader) As DataTable

        Dim newTable As New DataTable()
        Dim col As DataColumn
        Dim row As DataRow
        Dim i As Integer

        For i = 0 To Reader.FieldCount - 1

            col = New DataColumn()
            col.ColumnName = Reader.GetName(i)
            col.DataType = Reader.GetFieldType(i)

            newTable.Columns.Add(col)
        Next

        While Reader.Read

            row = newTable.NewRow()
            For i = 0 To Reader.FieldCount - 1
                row(i) = Reader.Item(i)
            Next

            newTable.Rows.Add(row)
        End While

        Return newTable
    End Function

    Public Function ReaderToTable(ByVal Reader As MySqlDataReader) As DataTable

        Dim newTable As New DataTable()
        Dim col As DataColumn
        Dim row As DataRow
        Dim i As Integer

        For i = 0 To Reader.FieldCount - 1

            col = New DataColumn()
            col.ColumnName = Reader.GetName(i)
            col.DataType = Reader.GetFieldType(i)

            newTable.Columns.Add(col)
        Next

        While Reader.Read

            row = newTable.NewRow()
            For i = 0 To Reader.FieldCount - 1
                row(i) = Reader.Item(i)
            Next

            newTable.Rows.Add(row)
        End While

        Return newTable
    End Function

    Public Function ReaderToTable(ByVal Reader As OdbcDataReader) As DataTable
        Dim newTable As New DataTable()
        Dim col As DataColumn
        Dim row As DataRow
        Dim i As Integer

        For i = 0 To Reader.FieldCount - 1

            col = New DataColumn()
            col.ColumnName = Reader.GetName(i)
            col.DataType = Reader.GetFieldType(i)

            newTable.Columns.Add(col)
        Next

        While Reader.Read

            row = newTable.NewRow()
            For i = 0 To Reader.FieldCount - 1
                row(i) = Reader.Item(i)
            Next

            newTable.Rows.Add(row)
        End While

        Return newTable
    End Function

    Public Function ReaderToTable(ByVal Reader As OleDbDataReader) As DataTable
        Dim newTable As New DataTable()
        Dim col As DataColumn
        Dim row As DataRow
        Dim i As Integer

        For i = 0 To Reader.FieldCount - 1

            col = New DataColumn()
            col.ColumnName = Reader.GetName(i)
            col.DataType = Reader.GetFieldType(i)

            newTable.Columns.Add(col)
        Next

        While Reader.Read

            row = newTable.NewRow()
            For i = 0 To Reader.FieldCount - 1
                row(i) = Reader.Item(i)
            Next

            newTable.Rows.Add(row)
        End While

        Return newTable
    End Function

End Module
