''' <summary>
'''����һ���ṹ�����鵽һ��Object����
''' </summary>
''' <param name="arStruct"></param>
''' <param name="arObj"></param>
''' <remarks></remarks>
Public Sub CopyStructArrayToObjectArray(Of T)(ByVal arStruct() As T, ByRef arObj() As Object)

	Dim StructUbound As Int32 = 0

	StructUbound = arStruct.Length - 1
	ReDim arObj(StructUbound)

	Dim i As Int32 = 0
	For i = 0 To arStruct.Length - 1
		arObj(i) = arStruct(i)
	Next

End Sub