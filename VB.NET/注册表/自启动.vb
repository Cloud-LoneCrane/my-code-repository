'��Windows�������еĳ��� 
Dim Reg As Microsoft.Win32.RegistryKey
Reg = CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
Reg.SetValue("LIXU", "C:\lixu.exe") 'д��������
Reg.DeleteValue("LIXU") 'ɾ��������

Dim str As String = ""
str = Reg.GetValue("LIXU")

Dim strMsg = ""

If str = "" Then
	strMsg = "��ע������ڻ�ֵΪ��"
Else
	strMsg = "��ע�����ֵΪ: " & str
End If