''' <summary>
''' 按位运算的值
''' </summary>
''' <param name="arVar">字节数组 每个元素表示一位 0 为假 1 为真</param>
''' <returns>按位运算的值</returns>
Public Function MakeBitCombo(ByVal arVar() As Byte) As Integer
	Dim intVar As Integer

	intVar = 0
	If Not arVar Is Nothing Then
		Dim shtStart As Short = LBound(arVar)
		Dim shtEnd As Short = UBound(arVar)
		Dim i As Short = 0

		For i = 0 To shtEnd - shtStart
			intVar += arVar(i) * System.Math.Pow(2, i)
		Next

	End If

	Return intVar
End Function


''' <summary>
''' 使用按位组合的整型值设置一组CheckBox的Checked属性
''' </summary>
''' <param name="shtVar">按位组合的整型值</param>
''' <param name="arChk">CheckBox 数组</param>
''' <remarks>调用格式   eg. Dim arChk() As CheckBox = {CheckBox1, CheckBox2, CheckBox3, CheckBox4}
'''                     SetChkChecked(1, arChk)
''' </remarks>
Public Sub SetChkChecked(ByVal shtVar As Short, ByRef arChk() As CheckBox)
	Dim arChrVar() As Char
	Dim str As String = ""
	Dim i As Integer = 0
	Dim intUbound As Integer = 0

	IntToBit(shtVar, str)   '得到包含有按位组合信息的字符串
	arChrVar = str.ToCharArray

	intUbound = UBound(arChrVar)
	For i = 0 To UBound(arChk)
		If i > intUbound Then
			arChk(i).Checked = False
		Else
			arChk(i).Checked = IIf(arChrVar(i) = "0", False, True)
		End If
	Next

End Sub

''' <summary>
''' 将整型转换为按位组合的字符串
''' </summary>
''' <param name="shtVar">包含按位组合的值</param>
''' <param name="strBit">用来返回按位组合的字符串</param>
''' <remarks></remarks>
Public Sub IntToBit(ByVal shtVar As Short, ByRef strBit As String)
  
	If shtVar = 0 Then  '表示已经到了尽头
		Exit Sub
	End If

	If (shtVar Mod 2) <> 0 Then '有余数
		strBit += "1"
	Else '没有余数
		strBit += "0"
	End If

	IntToBit(shtVar \ 2, strBit) '递归调用

End Sub
