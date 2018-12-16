' Created by VS2005.
' User: jifle
' Date: 2010-4-9
' Time: 15:51
' 修改时间:2010-05-31 17:30
' 修改内容：
'       重新整理DBConnection和DBCommand类，增加modTypes模块和modFuncs模块
'-------------------------------------------------------------------------------
'引用命名空间
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.Data.OleDb
'-------------------------------------------------------------------------------
''' <summary>
''' 公用函数模块
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
