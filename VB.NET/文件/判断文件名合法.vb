''' <summary>
''' 文件名是否合法
''' </summary>
''' <returns>合法,return True;非法,return Flase</returns>
Private Function IsFileNameValid(ByVal name As String) As Boolean
	Dim isFilename As Boolean = True
	Dim errorStr As String() = New String() {"/", "\", ":", ",", "*", "?", _
	 """", "<", ">", "|"}

	If String.IsNullOrEmpty(name) Then
		'文件名为空
		isFilename = False
	Else
		For i As Integer = 0 To errorStr.Length - 1
			If name.Contains(errorStr(i)) Then
				'字符串中是否包含特殊符号
				isFilename = False
				Exit For
			End If
		Next
	End If
	Return isFilename
End Function