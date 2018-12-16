Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

	Dim i = 0, j As Integer = 0
	Dim strTmp As String = ""
	Dim str1 = "", str2 As String = ""

	Do
		str1 = Format(i, "00")

		str2 = Format(j, "00")
		strTmp = """" & str1 & ":" & str2 & """"
		Debug.Print(strTmp)

		Debug.Print(",")
		str2 = Format(j, "30")
		strTmp = """" & str1 & ":" & str2 & """"
		Debug.Print(strTmp)
		Debug.Print(",")

		i += 1
	Loop While i <= 24
End Sub