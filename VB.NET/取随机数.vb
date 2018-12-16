''' <summary>
''' 获得随机数
''' </summary>
''' <returns>Int32</returns>
''' <remarks></remarks>
Public Function GetRandNum(ByVal Low As Int32, ByVal High As Int32) As Int32
	GetRandNum = 0

	Dim intTmp As Int32 = 0
	Dim RandGenerater As New Random '随机数生成器

	intTmp = RandGenerater.Next(Low, High)
	GetRandNum = intTmp

End Function
