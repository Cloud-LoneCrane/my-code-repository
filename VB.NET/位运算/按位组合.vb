''' <summary>
''' ��λ�����ֵ
''' </summary>
''' <param name="arVar">�ֽ����� ÿ��Ԫ�ر�ʾһλ 0 Ϊ�� 1 Ϊ��</param>
''' <returns>��λ�����ֵ</returns>
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
''' ʹ�ð�λ��ϵ�����ֵ����һ��CheckBox��Checked����
''' </summary>
''' <param name="shtVar">��λ��ϵ�����ֵ</param>
''' <param name="arChk">CheckBox ����</param>
''' <remarks>���ø�ʽ   eg. Dim arChk() As CheckBox = {CheckBox1, CheckBox2, CheckBox3, CheckBox4}
'''                     SetChkChecked(1, arChk)
''' </remarks>
Public Sub SetChkChecked(ByVal shtVar As Short, ByRef arChk() As CheckBox)
	Dim arChrVar() As Char
	Dim str As String = ""
	Dim i As Integer = 0
	Dim intUbound As Integer = 0

	IntToBit(shtVar, str)   '�õ������а�λ�����Ϣ���ַ���
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
''' ������ת��Ϊ��λ��ϵ��ַ���
''' </summary>
''' <param name="shtVar">������λ��ϵ�ֵ</param>
''' <param name="strBit">�������ذ�λ��ϵ��ַ���</param>
''' <remarks></remarks>
Public Sub IntToBit(ByVal shtVar As Short, ByRef strBit As String)
  
	If shtVar = 0 Then  '��ʾ�Ѿ����˾�ͷ
		Exit Sub
	End If

	If (shtVar Mod 2) <> 0 Then '������
		strBit += "1"
	Else 'û������
		strBit += "0"
	End If

	IntToBit(shtVar \ 2, strBit) '�ݹ����

End Sub
