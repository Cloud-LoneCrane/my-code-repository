''' <summary>
''' �ļ����Ƿ�Ϸ�
''' </summary>
''' <returns>�Ϸ�,return True;�Ƿ�,return Flase</returns>
Private Function IsFileNameValid(ByVal name As String) As Boolean
	Dim isFilename As Boolean = True
	Dim errorStr As String() = New String() {"/", "\", ":", ",", "*", "?", _
	 """", "<", ">", "|"}

	If String.IsNullOrEmpty(name) Then
		'�ļ���Ϊ��
		isFilename = False
	Else
		For i As Integer = 0 To errorStr.Length - 1
			If name.Contains(errorStr(i)) Then
				'�ַ������Ƿ�����������
				isFilename = False
				Exit For
			End If
		Next
	End If
	Return isFilename
End Function