
''' <summary>
''' ������Ͽ��items����
''' </summary>
''' <param name="arStr">������items���ݵ��ַ�����</param>
''' <remarks></remarks>
Public Sub Cbo_ItemsSet(ByVal cboTmp As ComboBox, ByVal arStr() As String)

	Dim i As Integer = 0
	For i = 0 To arStr.Length - 1
		cboTmp.Items.Add(arStr(i))
	Next

End Sub