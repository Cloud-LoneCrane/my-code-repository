Public Structure strtTmp1
	Public intAge As Int32
	<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strHome As String
	Public shtLevel As Int16
	<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strName As String
End Structure

Sub Main()
   Dim t1 As strtTmp1
	Dim t2 As strtTmp1
	Dim bytBufTmp() As Byte
	t1.strName = "123"
	t1.strHome = "北京，郑州-----------------------------------------------------"
	t1.intAge = 24
	t1.shtLevel = 2

	bytBufTmp = RawSerialize(t1)
	t2 = RawDeserialize(bytBufTmp, GetType(strtTmp1))
	Console.ReadKey()
End Sub
''' <summary>
''' 结构体转换为字节数组
''' </summary>
''' <param name="anything"></param>
''' <returns></returns>
''' <remarks></remarks>
Public Function RawSerialize(ByVal anything As Object) As Byte()

	Dim rawsize As Integer = Marshal.SizeOf(anything)
	Dim buffer As IntPtr = Marshal.AllocHGlobal(rawsize)

	Marshal.StructureToPtr(anything, buffer, False)

	Dim rawdatas(rawsize - 1) As Byte

	Marshal.Copy(buffer, rawdatas, 0, rawsize)
	Marshal.FreeHGlobal(buffer)

	Return rawdatas

End Function

''' <summary>
'''  字节数组转换为结构体(参数2: eg. GetType(strtTmp))
''' </summary>
''' <param name="rawdatas"></param>
''' <param name="anytype"></param>
''' <returns></returns>
''' <remarks></remarks>
Public Function RawDeserialize(ByVal rawdatas As Byte(), ByVal anytype As Type) As Object

	Dim rawsize As Integer = Marshal.SizeOf(anytype)

	If (rawsize > rawdatas.Length) Then
		Return Nothing
	End If

	Dim buffer As IntPtr = Marshal.AllocHGlobal(rawsize)

	Marshal.Copy(rawdatas, 0, buffer, rawsize)

	Dim retobj As Object = Marshal.PtrToStructure(buffer, anytype)

	Marshal.FreeHGlobal(buffer)

	Return retobj

End Function