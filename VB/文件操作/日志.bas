'=====================================================
'���ܣ� д����־
'=====================================================
Sub WriteLog(ByRef strTmp As String)
On Error Resume Next

	Dim intFileNum As Integer
	intFileNum = 511
		Open App.Path & "\��־.log" For Append As #intFileNum
		Print #intFileNum, Now
		Print #intFileNum, strTmp
	Close #511
End Sub