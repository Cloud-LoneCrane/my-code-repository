''' <summary>
''' ��������
''' </summary>
''' <returns>Int32</returns>
''' <remarks></remarks>
Public Function GetRandNum(ByVal Low As Int32, ByVal High As Int32) As Int32
	GetRandNum = 0

	Dim intTmp As Int32 = 0
	Dim RandGenerater As New Random '�����������

	intTmp = RandGenerater.Next(Low, High)
	GetRandNum = intTmp

End Function
