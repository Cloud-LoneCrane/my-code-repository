''' <summary>
''' 判断电子邮件格式是否正确.
''' </summary>
''' <param name="strIn"></param>
''' <returns>true 格式正确 false 格式错误 </returns>
''' <remarks></remarks>
Function IsValidEmail(ByVal strIn As String) As Boolean
	' Return true if strIn is in valid e-mail format.
	Return Regex.IsMatch(strIn, "^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
End Function

''' <summary>
''' 用空字符串替换无效字符
''' </summary>
''' <param name="strIn"></param>
''' <returns></returns>
''' <remarks></remarks>
Function CleanInput(ByVal strIn As String) As String
	' Replace invalid characters with empty strings.
	Return Regex.Replace(strIn, "[^\w\.@-]", "")    '\w 与任何单词字符匹配()

End Function
