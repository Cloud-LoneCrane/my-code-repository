''' <summary>
''' 打印注册项的子项
''' </summary>
''' <param name="rkey">键名 eg.   Dim rk As Microsoft.Win32.RegistryKey = CurrentUser</param>
''' <remarks>  Dim rk As Microsoft.Win32.RegistryKey = CurrentUser PrintKeys(rk)</remarks>
Private Sub PrintKeys(ByVal rkey As Microsoft.Win32.RegistryKey)

	' Retrieve all the subkeys for the specified key.
	Dim names As String() = rkey.GetSubKeyNames()

	Dim icount As Integer = 0

	Console.WriteLine("Subkeys of " & rkey.Name)
	Console.WriteLine("-----------------------------------------------")

	' Print the contents of the array to the console.
	Dim s As String
	For Each s In names
		Console.WriteLine(s)

		' The following code puts a limit on the number
		' of keys displayed.  Comment it out to print the
		' complete list.
		icount += 1
		If icount >= 10 Then
			Exit For
		End If
	Next s
End Sub