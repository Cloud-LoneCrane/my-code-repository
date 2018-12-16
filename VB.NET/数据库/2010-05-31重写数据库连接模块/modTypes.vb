
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

    End Module
End Namespace
