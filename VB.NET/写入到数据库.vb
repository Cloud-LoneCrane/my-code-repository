Dim strSQL As String = ""
'���͸��������
If False = SendData(strSql) Then
	MessageBox.Show("", frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
	exit sub 
End If

	MessageBox.Show("", frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)