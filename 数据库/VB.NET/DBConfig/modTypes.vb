
' Created by VS2005.
' User: jifle
' Date: 2010-4-9
' Time: 15:51
' 修改时间:2010-05-31 17:30
' 修改内容：
'       重新整理DBConnection和DBCommand类，增加modTypes模块和modFuncs模块
Namespace DBConfig
    ''' <summary>
    ''' 数据库连接类的类型定义模块
    ''' </summary>
    ''' <remarks></remarks>
    Public Module modTypes

        ''' <summary>
        ''' 数据访问接口类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum enmDataAccessType As Short
            ''' <summary>
            ''' OLEDB
            ''' </summary>
            ''' <remarks></remarks>
            DB_OLEDB = 0 '
            ''' <summary>
            ''' ODBC
            ''' </summary>
            ''' <remarks></remarks>
            DB_ODBC
            ''' <summary>
            ''' MYSQL
            ''' </summary>
            ''' <remarks></remarks>
            DB_MYSQL
            ''' <summary>
            ''' SQL Server
            ''' </summary>
            ''' <remarks></remarks>
            DB_SQL
        End Enum

        ''' <summary>
        ''' 查询原子
        ''' </summary>
        ''' <remarks></remarks>
        Public Structure strtSqlItem
            ''' <summary>
            ''' 表名
            ''' </summary>
            ''' <remarks></remarks>
            Public strTableName As String
            ''' <summary>
            ''' 对应的SQL语句
            ''' </summary>
            ''' <remarks></remarks>
            Public strSQL As String

        End Structure

    End Module
End Namespace
