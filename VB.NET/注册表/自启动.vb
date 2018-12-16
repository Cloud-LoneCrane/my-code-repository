'随Windows启动运行的程序 
Dim Reg As Microsoft.Win32.RegistryKey
Reg = CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
Reg.SetValue("LIXU", "C:\lixu.exe") '写入自启动
Reg.DeleteValue("LIXU") '删除自启动

Dim str As String = ""
str = Reg.GetValue("LIXU")

Dim strMsg = ""

If str = "" Then
	strMsg = "该注册项不存在或值为空"
Else
	strMsg = "该注册项的值为: " & str
End If