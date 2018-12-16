Dim strSQL As String = ""
'发送给处理程序
If False = SendData(strSql) Then
	MessageBox.Show("", frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
	exit sub 
End If

	MessageBox.Show("", frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)