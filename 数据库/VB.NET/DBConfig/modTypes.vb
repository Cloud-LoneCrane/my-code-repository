
' Created by VS2005.
' User: jifle
' Date: 2010-4-9
' Time: 15:51
' �޸�ʱ��:2010-05-31 17:30
' �޸����ݣ�
'       ��������DBConnection��DBCommand�࣬����modTypesģ���modFuncsģ��
Namespace DBConfig
    ''' <summary>
    ''' ���ݿ�����������Ͷ���ģ��
    ''' </summary>
    ''' <remarks></remarks>
    Public Module modTypes

        ''' <summary>
        ''' ���ݷ��ʽӿ�����
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
        ''' ��ѯԭ��
        ''' </summary>
        ''' <remarks></remarks>
        Public Structure strtSqlItem
            ''' <summary>
            ''' ����
            ''' </summary>
            ''' <remarks></remarks>
            Public strTableName As String
            ''' <summary>
            ''' ��Ӧ��SQL���
            ''' </summary>
            ''' <remarks></remarks>
            Public strSQL As String

        End Structure

    End Module
End Namespace
