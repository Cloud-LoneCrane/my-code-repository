'=====================================================
'功能： 写入日志
'=====================================================
Sub WriteLog(ByRef strTmp As String)
On Error Resume Next

	Dim intFileNum As Integer
	intFileNum = 511
		Open App.Path & "\日志.log" For Append As #intFileNum
		Print #intFileNum, Now
		Print #intFileNum, strTmp
	Close #511
End Sub