
''' <summary>
''' 设置组合框的items集合
''' </summary>
''' <param name="arStr">包含有items内容的字符数组</param>
''' <remarks></remarks>
Public Sub Cbo_ItemsSet(ByVal cboTmp As ComboBox, ByVal arStr() As String)

	Dim i As Integer = 0
	For i = 0 To arStr.Length - 1
		cboTmp.Items.Add(arStr(i))
	Next

End Sub