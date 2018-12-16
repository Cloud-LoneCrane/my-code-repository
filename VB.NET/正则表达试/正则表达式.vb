''' <summary>
''' �жϵ����ʼ���ʽ�Ƿ���ȷ.
''' </summary>
''' <param name="strIn"></param>
''' <returns>true ��ʽ��ȷ false ��ʽ���� </returns>
''' <remarks></remarks>
Function IsValidEmail(ByVal strIn As String) As Boolean
	' Return true if strIn is in valid e-mail format.
	Return Regex.IsMatch(strIn, "^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
End Function

''' <summary>
''' �ÿ��ַ����滻��Ч�ַ�
''' </summary>
''' <param name="strIn"></param>
''' <returns></returns>
''' <remarks></remarks>
Function CleanInput(ByVal strIn As String) As String
	' Replace invalid characters with empty strings.
	Return Regex.Replace(strIn, "[^\w\.@-]", "")    '\w ���κε����ַ�ƥ��()

End Function
