
'Enter��Tab
'һ�������������KeyPreview  = TRUE
'����
Private Sub frmCreateAcc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
	If e.KeyCode = Keys.Enter Then
		My.Computer.Keyboard.SendKeys("{TAB}")
	End If
End Sub