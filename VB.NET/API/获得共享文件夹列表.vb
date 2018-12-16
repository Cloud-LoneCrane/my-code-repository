 Public Const MAX_PREFERRED_LENGTH As Int32 = &HFFFFFFFF
    Public Const NERR_Success As Int32 = 0

    Public Enum NetShareLevel As Integer
        Names = 0
        Resources = 1
        ResourecesEx = 502
        ResourecesSingleScope = 503
        ResourcesAllScopes
    End Enum

    Public Structure _SHARE_INFO_0
        <MarshalAs(UnmanagedType.LPWStr)> Public shi0_netname As String
    End Structure
    Public Declare Unicode Function NetShareEnum Lib "netapi32.dll" ( _
    <MarshalAs(UnmanagedType.LPWStr)> ByVal servername As String, _
        ByVal level As NetShareLevel, _
        ByRef bufptr As IntPtr, _
        ByVal prefmaxlen As Integer, _
        ByRef entriesread As Integer, _
        ByRef totalentries As Integer, _
        ByRef resume_handle As Integer _
    ) As Integer
   
    ''' <summary>
    ''' 获得共享文件夹列表
    ''' </summary>
    ''' <param name="server">服务器eg.PC_001,server</param>
    ''' <returns>共享文件夹列表数组</returns>
    ''' <remarks></remarks>
    Public Function GetSharedFolders(ByVal server As String) As String()
        Dim dataptr As IntPtr
        Dim num = 0, total = 0, hresume As Int32 = 0 ' As integer
        Try
            If NetShareEnum("\\" & server, NetShareLevel.Names, dataptr, MAX_PREFERRED_LENGTH, num, total, hresume) <> NERR_Success Then
                Return Nothing
            End If

            Dim ret(num - 1) As String
            Dim i As Int32 = 0
            For i = 0 To num - 1
                Dim Item As _SHARE_INFO_0 = Marshal.PtrToStructure(CType(dataptr.ToInt32 + Marshal.SizeOf(GetType(_SHARE_INFO_0)) * i, IntPtr), GetType(_SHARE_INFO_0))
                ret(i) = Item.shi0_netname
            Next

            Dim arTmp As New ArrayList
            For i = 0 To ret.Length - 1
                If InStr(ret(i), "$") = 0 Then
                    arTmp.Add(ret(i))
                End If
            Next

            Dim arStr() As String = CType(arTmp.ToArray(GetType(String)), String())

            Return arStr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
